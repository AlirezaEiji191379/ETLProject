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


    public SqlServerDbConnectionMetaDataBusiness(IQueryFactoryProvider queryFactoryProvider,
        IEtlColumnTypeMapper etlColumnTypeMapper)
    {
        _queryFactoryProvider = queryFactoryProvider;
        _etlColumnTypeMapper = etlColumnTypeMapper;
    }

    public DataSourceType DataSourceType { get; } = DataSourceType.SQLServer;

    public async Task<List<string>> GetDatabases(ConnectionDto connectionDto)
    {
        var queryFactory = GetQueryFactory(connectionDto, null);
        var query = new Query("sys.databases").Select("name");
        var dapperRowsResult = await queryFactory.GetAsync(query);
        return (from IDictionary<string, object> dapperRow in dapperRowsResult select dapperRow["name"].ToString())
            .ToList();
    }

    public async Task<List<DbTableAttributes>> GetDatabaseTables(ConnectionDto connectionDto, string databaseName)
    {
        var queryFactory = GetQueryFactory(connectionDto, databaseName);
        var query = new Query($"{databaseName}.INFORMATION_SCHEMA.TABLES").Select("TABLE_NAME", "TABLE_SCHEMA")
            .Where("TABLE_TYPE", "=", "BASE TABLE");
        var dapperRowsResult = await queryFactory.GetAsync(query);
        return (from IDictionary<string, object> dapperRow in dapperRowsResult
            select new DbTableAttributes()
            {
                TableName = dapperRow["TABLE_NAME"].ToString()!,
                TableSchema = dapperRow["TABLE_SCHEMA"].ToString()!,
                DatabaseName = databaseName
            }).ToList();
    }

    public async Task<List<DbTableColumnInformation>> GetTableColumns(ConnectionDto connectionDto, string databaseName,
        string tableName)
    {
        var queryFactory = GetQueryFactory(connectionDto, databaseName);
        var query = new Query("INFORMATION_SCHEMA.COLUMNS").Select("COLUMN_NAME",
                "DATA_TYPE",
                "CHARACTER_MAXIMUM_LENGTH",
                "NUMERIC_SCALE",
                "COLLATION_NAME")
            .Where("TABLE_NAME", "=", tableName);
        var dapperRowsResult = await queryFactory.GetAsync(query);
        return (from IDictionary<string, object> dapperRow in dapperRowsResult
            let lengthString = dapperRow["CHARACTER_MAXIMUM_LENGTH"].ToString()
            let scaleString = dapperRow["NUMERIC_SCALE"].ToString()
            let collation = dapperRow["COLLATION_NAME"].ToString()
            let dbDataType = dapperRow["DATA_TYPE"].ToString()
            let sqlDbColumn = new SqlServerDBColumn() { SqlServerDbType = Enum.Parse<SqlServerDbType>(dbDataType) }
            select new DbTableColumnInformation()
            {
                Collation = collation,
                Length = lengthString == null ? null : int.Parse(lengthString),
                Precision = scaleString == null ? null : int.Parse(scaleString),
                ColumnName = dapperRow["COLUMN_NAME"].ToString()!,
                DatabaseType = dbDataType,
                SystemType = _etlColumnTypeMapper.AdaptSqlServerType(sqlDbColumn).Type.ToString()
            }).ToList();
    }
    private QueryFactory GetQueryFactory(ConnectionDto connectionDto, string databaseName)
    {
        var connectionParameter = new DatabaseConnectionParameters()
        {
            DataSourceType = this.DataSourceType,
            Host = connectionDto.Host,
            Password = connectionDto.Password,
            Username = connectionDto.Username,
            Port = connectionDto.Port,
            DatabaseName = databaseName
        };
        return _queryFactoryProvider.GetQueryFactoryByConnection(connectionParameter);
    }
}