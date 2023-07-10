using ETLProject.Common.Common.DIManager;
using ETLProject.Common.Database;
using ETLProject.Common.Table;
using ETLProject.DataSource.Abstractions;
using ETLProject.DataSource.Common;
using ETLProject.DataSource.Common.DIManager;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

internal static class WriteSamplesHelpers
{
    internal static async Task WriteSamples()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddCommonServices();
        serviceCollection.AddDataSourceQueryServices();

        var provider = serviceCollection.BuildServiceProvider();
        var dbTableCreator = provider.GetRequiredService<IDbTableFactory>();
        var etlTable = new ETLTable()
        {
            DataSourceType = DataSourceType.SQLServer,
            TableType = TableType.Temp,
            TableName = "",
            Columns = new List<ETLColumn> {
                    new()
                    {
                        Name = "FullName",
                        ETLColumnType = new ETLColumnType()
                        {
                            Length= 20,
                            Type = ColumnType.StringType
                        }
                    },
                    new()
                    {
                        Name = "Id",
                        ETLColumnType = new ETLColumnType()
                        {
                            Type = ColumnType.Int32Type
                        }
                    }
                },
            DatabaseConnection = new DatabaseConnectionParameters()
            {
                ConnectionName = "x",
                DataSourceType = DataSourceType.SQLServer,
                DatabaseName = "TestDb",
                //Host = "localhost",
                Host = "LAPTOP-FN08220K\\ALIREZALOCAL",
                Id = Guid.NewGuid(),
                Password = "92?VH2WMrx",
                Port = "1433",
                Schema = "dbo",
                Username = "sa"
            },
        };
        await dbTableCreator.CreateTable(etlTable);
        var dataInserterProvider = provider.GetRequiredService<IDataBulkCopyProvider>();
        var dataInserter = dataInserterProvider.GetBulkInserter(DataSourceType.SQLServer);
        using var dataTable = new DataTable();

        dataTable.Columns.Add("FullName", typeof(string));
        dataTable.Columns.Add("Id", typeof(int));

        var dr = dataTable.NewRow();
        dr["Id"] = 1;
        dr["FullName"] = "reza";
        dataTable.Rows.Add(dr);

        var dr2 = dataTable.NewRow();
        dr2["Id"] = 2;
        dr2["FullName"] = "ali";
        dataTable.Rows.Add(dr2);

        await dataInserter.InsertBulk(dataTable, etlTable);
    }

    internal static async Task ExtractAndLoad()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddCommonServices();
        serviceCollection.AddDataSourceQueryServices();
        var provider = serviceCollection.BuildServiceProvider();

        var dataTransfer = provider.GetRequiredService<IDataTransfer>();

        var etlTable = new ETLTable()
        {
            TableType = TableType.Permanent,
            TableName = "Users",
            DataSourceType = DataSourceType.Postgresql,
            DatabaseConnection = new DatabaseConnectionParameters()
            {
                ConnectionName = "x",
                DataSourceType = DataSourceType.Postgresql,
                DatabaseName = "TestDB",
                Host = "localhost",
                //Host = "LAPTOP-FN08220K\\ALIREZALOCAL",
                Id = Guid.NewGuid(),
                Password = "92?VH2WMrx",
                Port = "5432",
                Schema = "public",
                Username = "postgres"
            },
            Columns = new List<ETLColumn>
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
                        Length= 100
                    }
                }
            }
        };

        var destEtlTable = new ETLTable()
        {
            TableType = TableType.Permanent,
            DatabaseConnection = new DatabaseConnectionParameters()
            {
                ConnectionName = "x",
                DataSourceType = DataSourceType.SQLServer,
                DatabaseName = "TestDB",
                //Host = "localhost",
                Host = "LAPTOP-FN08220K\\ALIREZALOCAL",
                Id = Guid.NewGuid(),
                Password = "92?VH2WMrx",
                Port = "1433",
                Schema = "dbo",
                Username = "sa"
            },
            DataSourceType = DataSourceType.SQLServer,
            TableName = "SampleUsers2",
        };

        await dataTransfer.TransferDataBetweenTwoDifferentConnections(etlTable,destEtlTable,new BulkConfiguration { BatchSize = 2});

    }

    internal static async Task ExtractAndLoadInConnection()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddCommonServices();
        serviceCollection.AddDataSourceQueryServices();
        var provider = serviceCollection.BuildServiceProvider();

        var dataTransfer = provider.GetRequiredService<IDataTransfer>();

        var etlTable = new ETLTable()
        {
            TableType = TableType.Permanent,
            TableName = "Users",
            DataSourceType = DataSourceType.Postgresql,
            DatabaseConnection = new DatabaseConnectionParameters()
            {
                ConnectionName = "x",
                DataSourceType = DataSourceType.Postgresql,
                DatabaseName = "TestDB",
                Host = "localhost",
                //Host = "LAPTOP-FN08220K\\ALIREZALOCAL",
                Id = Guid.NewGuid(),
                Password = "92?VH2WMrx",
                Port = "5432",
                Schema = "public",
                Username = "postgres"
            },
            Columns = new List<ETLColumn>
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
                        Length= 100
                    }
                }
            }
        };

        await dataTransfer.TransferDataInSingleConnection(etlTable,"userse27",TableType.Permanent); }
}