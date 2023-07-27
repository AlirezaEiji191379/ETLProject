using ETLProject.Common.Database;
using ETLProject.Common.Table;
using ETLProject.Contract.DbConnectionContracts;
using ETLProject.DataSource.Abstractions;
using ETLProject.DataSource.DbConnectionMetaDataBusiness.Abstractions;
using SqlKata;
using SqlKata.DbTypes.DbColumn;
using SqlKata.DbTypes.Enums;
using SqlKata.Execution;

namespace ETLProject.DataSource.DbConnectionMetaDataBusiness;

internal class SqlServerDbConnectionMetaDataBusiness : IDbConnectionMetaDataBusiness
{
    private readonly IQueryFactoryProvider _queryFactoryProvider;
    private readonly IEtlColumnTypeMapper _etlColumnTypeMapper;
    private readonly IDbConnectionProvider _dbConnectionProvider;

    public SqlServerDbConnectionMetaDataBusiness(IQueryFactoryProvider queryFactoryProvider,
        IEtlColumnTypeMapper etlColumnTypeMapper, IDbConnectionProvider dbConnectionProvider)
    {
        _queryFactoryProvider = queryFactoryProvider;
        _etlColumnTypeMapper = etlColumnTypeMapper;
        _dbConnectionProvider = dbConnectionProvider;
    }

    public DataSourceType DataSourceType { get; } = DataSourceType.SQLServer;

    public async Task<List<string>> GetDatabases(ConnectionDto connectionDto)
    {
        var queryFactory = GetQueryFactory(connectionDto);
        var query = new Query("sys.databases").Select("name");
        var dapperRowsResult = await queryFactory.GetAsync(query);
        return (from IDictionary<string, object> dapperRow in dapperRowsResult select dapperRow["name"].ToString())
            .ToList();
    }

    public async Task<List<DbTableAttributes>> GetDatabaseTables(ConnectionDto connectionDto)
    {
        var queryFactory = GetQueryFactory(connectionDto);
        var query = new Query().FromRaw($"[{connectionDto.DatabaseName}].[INFORMATION_SCHEMA].[TABLES]").Select("TABLE_NAME", "TABLE_SCHEMA")
            .Where("TABLE_TYPE", "=", "BASE TABLE");
        var dapperRowsResult = await queryFactory.GetAsync(query);
        return (from IDictionary<string, object> dapperRow in dapperRowsResult
            select new DbTableAttributes()
            {
                TableName = dapperRow["TABLE_NAME"].ToString()!,
                TableSchema = dapperRow["TABLE_SCHEMA"].ToString()!,
                DatabaseName = connectionDto.DatabaseName
            }).ToList();
    }

    public async Task<List<DbTableColumnInformation>> GetTableColumns(ConnectionDto connectionDto,
        string tableName)
    {
        var queryFactory = GetQueryFactory(connectionDto);
        var query = new Query("INFORMATION_SCHEMA.COLUMNS").Select("COLUMN_NAME",
                "DATA_TYPE",
                "CHARACTER_MAXIMUM_LENGTH",
                "NUMERIC_SCALE",
                "COLLATION_NAME")
            .Where("TABLE_NAME", "=", tableName);
        var dapperRowsResult = await queryFactory.GetAsync(query);
        return (from IDictionary<string, object> dapperRow in dapperRowsResult
            let lengthString = dapperRow["CHARACTER_MAXIMUM_LENGTH"] 
            let scaleString = dapperRow["NUMERIC_SCALE"]
            let collation = dapperRow["COLLATION_NAME"]
            let dbDataType = dapperRow["DATA_TYPE"].ToString()
            let sqlDbColumn = new SqlServerDBColumn() { SqlServerDbType = Enum.Parse<SqlServerDbType>(dbDataType,true) }
            select new DbTableColumnInformation()
            {
                Collation = collation?.ToString(),
                Length = lengthString == null ? null : int.Parse(lengthString.ToString()),
                Precision = scaleString == null ? null : int.Parse(scaleString.ToString()),
                ColumnName = dapperRow["COLUMN_NAME"].ToString()!,
                DatabaseType = dbDataType,
                SystemType = _etlColumnTypeMapper.AdaptSqlServerType(sqlDbColumn).Type.ToString()
            }).ToList();
    }

    public void TestConnection(ConnectionDto connectionDto)
    {
        var connectionParameter = new DatabaseConnectionParameters()
        {
            DataSourceType = this.DataSourceType,
            Host = connectionDto.Host,
            Password = connectionDto.Password,
            Username = connectionDto.Username,
            Port = connectionDto.Port,
        };
        using var connection = _dbConnectionProvider.GetConnection(connectionParameter);
        connection.Open();
        connection.Close();
    }

    private QueryFactory GetQueryFactory(ConnectionDto connectionDto)
    {
        var connectionParameter = new DatabaseConnectionParameters()
        {
            DataSourceType = this.DataSourceType,
            Host = connectionDto.Host,
            Password = connectionDto.Password,
            Username = connectionDto.Username,
            Port = connectionDto.Port,
            DatabaseName = connectionDto.DatabaseName
        };
        return _queryFactoryProvider.GetQueryFactoryByConnection(connectionParameter);
    }
}