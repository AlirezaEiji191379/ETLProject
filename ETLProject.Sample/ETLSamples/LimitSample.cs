using ETLProject.Common.Common.DIManager;
using ETLProject.Common.Database;
using ETLProject.Common.Table;
using ETLProject.Contract.Limit;
using ETLProject.DataSource.Common.DIManager;
using ETLProject.DataSource.QueryBusiness.Limit.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using SqlKata;
using SqlKata.Compilers.Abstractions;

namespace ETLProject.Sample.ETLSamples;

public class LimitSample
{
    public static void CheckLimit()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddCommonServices();
        serviceCollection.AddDataSourceQueryServices();

        var provider = serviceCollection.BuildServiceProvider();
        var etlTable = new ETLTable()
        {
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

        /*var query = new Query("Users").Select("Id", "FullName");
        etlTable.Query = query;*/

        var limiter = provider.GetService<ILimitQueryMaker>();
        var compiler = provider.GetService<ICompilerProvider>().CreateCompiler(SqlKata.Compilers.Enums.DataSource.SqlServer);
        limiter.AddLimitQuery(etlTable,new LimitContract(){Top = 4});
        Console.WriteLine(compiler.Compile(etlTable.Query).ToString());


    }
}