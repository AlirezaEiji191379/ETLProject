﻿
using ETLProject.Common.Common.DIManager;
using ETLProject.Common.Database;
using ETLProject.Common.Table;
using ETLProject.Contract.Aggregate;
using ETLProject.Contract.Aggregate.Enums;
using ETLProject.Contract.DBReader;
using ETLProject.Contract.DbWriter;
using ETLProject.Contract.DbWriter.Enums;
using ETLProject.Contract.Limit;
using ETLProject.Contract.Sort;
using ETLProject.Contract.Where.Conditions;
using ETLProject.Contract.Where.Enums;
using ETLProject.DataSource.Common.DIManager;
using ETLProject.Infrastructure;
using ETLProject.Pipeline;
using ETLProject.Pipeline.Execution;
using ETLProject.Pipeline.Graph;
using ETLProject.Pipeline.Plugins;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using SqlKata;
using SqlKata.Compilers;
using SqlKata.Execution;

/*using var connection = new NpgsqlConnection("Host=localhost;Port=5432;Database=TestDB;Username=postgres;Password=92?VH2WMrx");
await connection.OpenAsync();
var query = new Query("Users").OrderBy("FullName").Limit(3);
var compiler = new PostgresCompiler();
Console.WriteLine(compiler.Compile(query).ToString());
var table = await new QueryFactory(connection,compiler).FromQuery(query).GetAsync();
Console.WriteLine(compiler.Compile(query).ToString());*/

var serviceCollection = new ServiceCollection();
serviceCollection.AddCommonServices();
serviceCollection.AddDataSourceQueryServices();
serviceCollection.AddInfrastructureServices();
var provider = serviceCollection.BuildServiceProvider();
var node1 = new DbReaderPlugin("Reader1",new DbReaderContract()
{
    Schema = "public",
    TableName = "usertest",
    DataSourceType = DataSourceType.Postgresql,
    SelectedColumns = new List<DbColumnDto>()
    {
        new DbColumnDto()
        {
            Name = "Id",
            ColumnType = ColumnType.Int32Type
        },
        new DbColumnDto()
        {
            Name = "FullName",
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
/*var node2 = new OrderPlugin("Order1",new SortContract()
{
    Columns = new List<OrderColumnDto>()
    {
        new OrderColumnDto()
        {
            Name = "FullName",
            SortType = SortType.Ascending
        }
    }
});*/

/*var node2 = new AggPlugin("Agg1", new AggregationParameter()
{
    GroupByColumns = new List<string>()
    {
        "FullName"
    },
    AggregateColumns = new List<AggregateColumns>()
    {
        new ()
        {
            AggregateType = AggregateType.Max,
            ColumnName = "Id",
            AliasName = "id_sum"
        }
    }
});*/

var node2 = new DistinctPlugin("Distinct1")
{

};

var node3 = new WherePlugin("Where1",new FieldCondition()
{
    ColumnName = "Id",
    ConditionType = ConditionType.GreaterThan,
    Value = 2
});

var node4 = new DbAddPlugin("add1", new DbWriterParameter()
{
    //UseInputConnection = true,
    DestinationConnectionId = Guid.Parse("d8d9bd70-6184-4150-a2a8-5c873602735e"),
    DbTransferAction = DbTransferAction.CreateInsert,
    DestinationTableName = "UsersEtl",
    DestinationTableSchema = "public",
    BulkConfiguration = new BulkConfiguration()
    {
        BatchSize = 100
    }
});
ServiceProviderContainer.ServiceProvider = provider;
var graph = new DataPipelineGraph();

graph.AddVertex(node1);// Read 
graph.AddVertex(node2);// Sort
/*graph.AddVertex(node3);// Where*/
graph.AddVertex(node4);// add

graph.AddEdge(node1,node2);
/*graph.AddEdge(node2,node3);*/
graph.AddEdge(node2,node4);

var executor = new PipelineExecutor(graph);


await executor.RunGraph(node4.PluginId);
