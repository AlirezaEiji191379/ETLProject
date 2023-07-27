
using ETLProject.Common.Common.DIManager;
using ETLProject.Common.Database;
using ETLProject.Common.Table;
using ETLProject.Contract.DbWriter;
using ETLProject.Contract.DbWriter.Enums;
using ETLProject.DataSource.Common.DIManager;
using ETLProject.DataSource.QueryBusiness.DbAddBusiness.Abstractions;
using ETLProject.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();

serviceCollection.AddCommonServices();
serviceCollection.AddDataSourceQueryServices();
serviceCollection.AddInfrastructureServices();

var provider = serviceCollection.BuildServiceProvider();

var dbWriter = provider.GetService<IDbAddBusiness>();

var inputTable = new ETLTable()
{
    TableName = "Users",
    TableSchema = "public",
    TableType = TableType.Permanent,
    DataSourceType = DataSourceType.Postgresql,
    AliasName = "t",
    DatabaseConnection = new DatabaseConnectionParameters()
    {
        DataSourceType = DataSourceType.Postgresql,
        Host = "localhost",
        Port = "5432",
        Username = "postgres",
        Password = "92?VH2WMrx",
        DatabaseName = "TestDB",
        Id = Guid.NewGuid()
    },
    Columns = new List<ETLColumn>()
    {
        new()
        {
            Name = "Id",
            ETLColumnType = new ETLColumnType()
            {
                Type = ColumnType.Int32Type
            }
        },
        new()
        {
            Name = "FullName",
            ETLColumnType = new ETLColumnType()
            {
                Type = ColumnType.StringType,
                Length = 200
            }
        }
    }
};

var dbWriterParameter = new DbWriterParameter()
{
    DestinationTableSchema = "testdb",
    DbTransferAction = DbTransferAction.Insert,
    UseInputConnection = false,
    DestinationConnectionId = Guid.Parse("d8d9bd70-6184-4150-a2a8-5c873602735e"),
    DestinationTableName = "postgresuser",
    BulkConfiguration = new BulkConfiguration()
    {
        BatchSize = 100
    }
};

await dbWriter.WriteToTable(inputTable,dbWriterParameter);

