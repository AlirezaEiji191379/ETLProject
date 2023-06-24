using ETLProject.Common.Common.DIManager;
using ETLProject.Common.Database;
using ETLProject.Common.Table;
using ETLProject.DataSource.Abstractions;
using ETLProject.DataSource.Common;
using ETLProject.DataSource.Common.DIManager;
using Microsoft.Extensions.DependencyInjection;


var serviceCollection = new ServiceCollection();

serviceCollection.AddCommonServices();
serviceCollection.AddDataSourceQueryServices();

var provider = serviceCollection.BuildServiceProvider();

var etlTable = new ETLTable()
{
    Columns = new List<Column>()
    {
        new()
        {
            Name= "Id",
            Type = ColumnType.Int32Type
        },
        new()
        {
            Name= "FullName",
            Type = ColumnType.StringType
        }
    },
    DataSourceType = DataSourceType.Postgresql,
    DatabaseConnection = new DatabaseConnectionParameters()
    {
        ConnectionName = "x",
        DataSourceType= DataSourceType.Postgresql,
        DatabaseName= "TestDB",
        Host= "localhost",
        Id = Guid.NewGuid(),
        Password= "92?VH2WMrx",
        Port = "5432",
        Schema = "public",
        Username = "postgres"
    },
    TableType = TableType.Permanent,
    TableName = "Users"
};


var bulkConfig = new BulkConfiguration()
{
    BatchSize = 2,
};

var dataBulkReader = provider.GetService<IDataBaseBulkReader>();

await foreach (var dt in dataBulkReader!.ReadDataInBulk(etlTable,bulkConfig))
{
    Console.WriteLine("hi");
}

