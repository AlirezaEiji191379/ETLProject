using ETLProject.Common.Common.DIManager;
using ETLProject.Common.Database;
using ETLProject.Common.Table;
using ETLProject.Contract.DbWriter;
using ETLProject.DataSource.Common.DIManager;
using ETLProject.DataSource.QueryBusiness.DbAddBusiness.Abstractions;
using ETLProject.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace ETLProject.Sample.ETLSamples;

public static class DbAddSamples
{
    public static async Task AddSample()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddCommonServices();
        serviceCollection.AddDataSourceQueryServices();
        serviceCollection.AddInfrastructureServices();

        var provider = serviceCollection.BuildServiceProvider();
        var addBusiness = provider.GetRequiredService<IDbAddBusiness>();

        var inputTable = new ETLTable()
        {
            DatabaseConnection = new DatabaseConnectionParameters()
            {
                Host = "localhost",
                Port = "5432",
                Password = "92?VH2WMrx",
                Username = "postgres",
                DatabaseName = "TestDB",
                DataSourceType = DataSourceType.Postgresql
            },
            TableType = TableType.Permanent,
            DataSourceType = DataSourceType.Postgresql,
            TableName = "users",
            TableSchema = "public",
            AliasName = "t",
            Columns = new List<ETLColumn>()
            {
                new ()
                {
                    Name = "id",
                    ETLColumnType = new ETLColumnType()
                    {
                        Type = ColumnType.Int32Type,
                        Length = 200
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
        };
        
        await addBusiness.WriteToTable(inputTable,new DbWriterParameter()
        {
            BulkConfiguration = new BulkConfiguration(){BatchSize = 3},
            TableType = TableType.Permanent,
            NewTableName = "Sample",
            DestinationConnectionId = Guid.Parse("14eb2dc9-5060-4f81-8790-3a5cbff5dfd0")
        });

    }
}