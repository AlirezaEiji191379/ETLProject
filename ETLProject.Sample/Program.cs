
using ETLProject.Common.Common.DIManager;
using ETLProject.Common.Database;
using ETLProject.Common.Table;
using ETLProject.Contract.DBReader;
using ETLProject.Contract.Sort;
using ETLProject.DataSource.Common.DIManager;
using ETLProject.Infrastructure;
using ETLProject.Pipeline;
using ETLProject.Pipeline.Execution;
using ETLProject.Pipeline.Graph;
using ETLProject.Pipeline.Plugins;
using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();
serviceCollection.AddCommonServices();
serviceCollection.AddDataSourceQueryServices();
serviceCollection.AddInfrastructureServices();
var provider = serviceCollection.BuildServiceProvider();
var node1 = new DbReaderPlugin("Reader1",new DbReaderContract()
{
    Schema = "public",
    TableName = "users",
    DataSourceType = DataSourceType.Postgresql,
    SelectedColumns = new List<DbColumnDto>()
    {
        new DbColumnDto()
        {
            Name = "id",
            ColumnType = ColumnType.Int32Type
        },
        new DbColumnDto()
        {
            Name = "fullname",
            ColumnType = ColumnType.StringType
        }
    },
    DatabaseConnectionParameters = new DatabaseConnectionParameters()
    {
        DataSourceType = DataSourceType.Postgresql,
        Host = "localhost",
        Password = "92?VH2WMrx",
        Port = "5432",
        Username = "postgres",
        DatabaseName = "TestDB"
    }
});
var node2 = new OrderPlugin("Order1",new SortContract()
{
    Columns = new List<OrderColumnDto>()
    {
        new OrderColumnDto()
        {
            Name = "Id",
            SortType = SortType.Ascending
        }
    }
});
ServiceProviderContainer.ServiceProvider = provider;
var graph = new DataPipelineGraph();

graph.AddVertex(node1);
graph.AddVertex(node2);

graph.AddEdge(node1,node2);

var executor = new PipelineExecutor(graph);


await executor.RunGraph(node2.PluginId);
