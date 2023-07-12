using ETLProject.Common.Common.DIManager;
using ETLProject.Common.Database;
using ETLProject.Common.Table;
using ETLProject.Contract.DBReader;
using ETLProject.DataSource.Common.DIManager;
using ETLProject.DataSource.QueryBusiness.DbReaderBusiness.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using SqlKata.Compilers.Abstractions;

namespace ETLProject.Sample.ETLSamples;

public static class DbTableReaderSample
{
    public static void Sample()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddCommonServices();
        serviceCollection.AddDataSourceQueryServices();

        var provider = serviceCollection.BuildServiceProvider();

        var dbTableReader = provider.GetRequiredService<IDbTableReader>();
        var contract = new DbReaderContract()
        {
            TableName = "Users",
            DataSourceType = DataSourceType.SQLServer,
            DatabaseConnectionParameters = new DatabaseConnectionParameters()
            {
            },
            SelectedColumns = new List<DbColumnDto>()
            {
                new ()
                {
                    Name = "Id",
                    ColumnType = ColumnType.Int32Type
                },
                new ()
                {
                    Name = "FullName",
                    ColumnType = ColumnType.StringType,
                    Length = 200
                },
                new()
                {
                    Name = "Age",
                    ColumnType = ColumnType.Int32Type,
                }
            }
        };
        var etlTable= dbTableReader.ReadTable(contract);
        var compiler = provider.GetRequiredService<ICompilerProvider>().CreateCompiler(SqlKata.Compilers.Enums.DataSource.SqlServer);
        Console.WriteLine(compiler.Compile(etlTable.Query).ToString());

    }
}