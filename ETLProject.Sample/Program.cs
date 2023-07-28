using ETLProject.Common.Common.DIManager;
using ETLProject.Common.Database;
using ETLProject.Common.Table;
using ETLProject.Contract.Aggregate;
using ETLProject.Contract.Aggregate.Enums;
using ETLProject.Contract.Where.Conditions;
using ETLProject.Contract.Where.Enums;
using ETLProject.DataSource.Common.DIManager;
using ETLProject.DataSource.QueryBusiness.AggregateBusiness.Abstractions;
using ETLProject.DataSource.QueryBusiness.WhereQueryBusiness.Abstractions;
using ETLProject.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using SqlKata;
using SqlKata.Compilers;

var serviceCollection = new ServiceCollection();

serviceCollection.AddCommonServices();
serviceCollection.AddDataSourceQueryServices();
serviceCollection.AddInfrastructureServices();

var provider = serviceCollection.BuildServiceProvider();

var aggBusiness = provider.GetRequiredService<IAggregateQueryBusiness>();
var inputTable = new ETLTable()
{
    Columns = new List<ETLColumn>()
    {
        new()
        {
            Name = "id",
            ETLColumnType = new ETLColumnType()
            {
                Type = ColumnType.Int32Type
            }
        },
        new()
        {
            Name = "fullname",
            ETLColumnType = new ETLColumnType()
            {
                Type = ColumnType.StringType,
                Length = 200
            }
        }
    },
    TableName = "Users",
    TableSchema = "dbo",
    AliasName = "t",
    TableType = TableType.Permanent,
    DataSourceType = DataSourceType.SQLServer,
    DatabaseConnection = new DatabaseConnectionParameters()
    {
        DataSourceType = DataSourceType.SQLServer,
        Host = "localhost",
        Port = "1433",
        DatabaseName = "testDb",
        Username = "sa",
        Password = "92?VH2WMrx"
    }
};

var aggregateParam = new AggregationParameter()
{
    GroupByColumns = new List<string>()
    {
        "fullname"
    },
    AggregateColumns = new List<AggregateColumns>()
    {
        new()
        {
            AliasName = "x",
            AggregateType = AggregateType.Max,
            ColumnName = "id"
        }
    }
};

var result = aggBusiness.AddAggregation(inputTable, aggregateParam);

var compiler = new SqlServerCompiler();

Console.WriteLine(compiler.Compile(result.Query));