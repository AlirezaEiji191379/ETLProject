using ETLProject.Common.Database;
using ETLProject.Contract.DbConnectionContracts;
using ETLProject.DataSource.Abstractions;
using ETLProject.DataSource.ColumnMapping;
using ETLProject.DataSource.DbConnectionMetaDataBusiness.Abstractions;
using SqlKata;
using SqlKata.DbTypes.DbColumn;
using SqlKata.DbTypes.Enums;
using SqlKata.Execution;

namespace ETLProject.DataSource.DbConnectionMetaDataBusiness;

internal class PostgresqlConnectionMetaDataBusiness : IDbConnectionMetaDataBusiness
{
    private readonly IQueryFactoryProvider _queryFactoryProvider;
    private readonly IEtlColumnTypeMapper _etlColumnTypeMapper;
    private readonly IDbConnectionProvider _dbConnectionProvider;

    public PostgresqlConnectionMetaDataBusiness(IQueryFactoryProvider queryFactoryProvider,
        IEtlColumnTypeMapper etlColumnTypeMapper, IDbConnectionProvider dbConnectionProvider)
    {
        _queryFactoryProvider = queryFactoryProvider;
        _etlColumnTypeMapper = etlColumnTypeMapper;
        _dbConnectionProvider = dbConnectionProvider;
    }

    public DataSourceType DataSourceType { get; } = DataSourceType.Postgresql;

    public async Task<List<string>> GetDatabases(ConnectionDto connectionDto)
    {
        var queryFactory = GetQueryFactory(connectionDto, null);
        var query = new Query("pg_database").Select("datname");
        var dapperRowsResult = await queryFactory.GetAsync(query);
        return (from IDictionary<string, object> dapperRow in dapperRowsResult select dapperRow["datname"].ToString())
            .ToList();
    }

    public async Task<List<DbTableAttributes>> GetDatabaseTables(ConnectionDto connectionDto, string databaseName)
    {
        var queryFactory = GetQueryFactory(connectionDto, databaseName);
        var query = new Query($"information_schema.tables").Select("table_name", "table_schema")
            .Where("table_type", "=", "BASE TABLE").Where("table_catalog", "=", databaseName);
        var dapperRowsResult = await queryFactory.GetAsync(query);
        return (from IDictionary<string, object> dapperRow in dapperRowsResult
            select new DbTableAttributes()
            {
                TableName = dapperRow["table_name"].ToString()!,
                TableSchema = dapperRow["table_schema"].ToString()!,
                DatabaseName = databaseName
            }).ToList();
    }

    public async Task<List<DbTableColumnInformation>> GetTableColumns(ConnectionDto connectionDto, string databaseName,
        string tableName)
    {
        var queryFactory = GetQueryFactory(connectionDto, databaseName);
        var query = new Query("information_schema.columns").Select("column_name",
                "data_type",
                "character_maximum_length",
                "numeric_scale",
                "collation_name")
            .Where("table_name", "=", tableName);
        var dapperRowsResult = await queryFactory.GetAsync(query);
        return (from IDictionary<string, object> dapperRow in dapperRowsResult
            let lengthString = dapperRow["character_maximum_length"]
            let scaleString = dapperRow["numeric_scale"]
            let collation = dapperRow["collation_name"]
            let dbDataType = dapperRow["data_type"]
            let postgresqlDbColumn = new PostgresqlDBColumn()
                { PostgresqlDbType = PostgresqlEnumTypeMapper.PostgreSqlDataTypes[dbDataType.ToString()] }
            select new DbTableColumnInformation()
            {
                Collation = collation?.ToString(),
                Length = lengthString == null ? null : int.Parse(lengthString.ToString()),
                Precision = scaleString == null ? null : int.Parse(scaleString.ToString()),
                ColumnName = dapperRow["column_name"].ToString()!,
                DatabaseType = dbDataType.ToString(),
                SystemType = _etlColumnTypeMapper.AdaptPostgresqlType(postgresqlDbColumn).Type.ToString()
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