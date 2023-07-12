using ETLProject.Common.Common.DIManager;
using ETLProject.Common.Database;
using ETLProject.Common.Table;
using ETLProject.DataSource.Common.DIManager;
using ETLProject.DataSource.QueryBusiness.DistinctBusiness.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using SqlKata.Compilers.Abstractions;

namespace ETLProject.Sample.ETLSamples;

public static class DistinctSample
{
    public static void Sample()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddCommonServices();
        serviceCollection.AddDataSourceQueryServices();

        var provider = serviceCollection.BuildServiceProvider();
        var etlTable = new ETLTable()
        {
            AliasName = "t",
            Columns = new List<ETLColumn>()
            {
                new()
                {
                    Name= "Id",
                    ETLColumnType = new ETLColumnType()
                    {
                        Type = ColumnType.Int32Type,
                    }
                },
                new()
                {
                    Name= "FullName",
                    ETLColumnType = new ETLColumnType()
                    {
                        Type = ColumnType.StringType,
                        Length= 100,
                    }
                }
            },
            DataSourceType = DataSourceType.SQLServer,
            DatabaseConnection = new DatabaseConnectionParameters()
            {
                ConnectionName = "x",
                DataSourceType = DataSourceType.SQLServer,
                DatabaseName = "testdb",
                Host = "localhost",
                Id = Guid.NewGuid(),
                Password = "92?VH2WMrx",
                Port = "1433",
                Schema = "testdb",
                Username = "alirezaeiji151379"
            },
            TableType = TableType.Permanent,
            TableName = "Users"
        };

        var distinctBusiness = provider.GetRequiredService < IDistinctQueryBusiness>();
        var compiler = provider.GetRequiredService<ICompilerProvider>().CreateCompiler(SqlKata.Compilers.Enums.DataSource.SqlServer);
        Console.WriteLine(compiler.Compile(distinctBusiness.DistinctTable(etlTable).Query));

    }
}