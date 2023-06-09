﻿using ETLProject.Common.Common.DIManager;
using ETLProject.Common.Database;
using ETLProject.Common.Table;
using ETLProject.DataSource.Abstractions;
using ETLProject.DataSource.Common;
using ETLProject.DataSource.Common.DIManager;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace ETLProject.Sample.ETLSamples
{
    internal static class ReadSamples
    {
        public static async Task ReadSampleAsync()
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
                DataSourceType = DataSourceType.MySql,
                DatabaseConnection = new DatabaseConnectionParameters()
                {
                    ConnectionName = "x",
                    DataSourceType = DataSourceType.MySql,
                    DatabaseName = "testdb",
                    Host = "localhost",
                    Id = Guid.NewGuid(),
                    Password = "92?VH2WMrx",
                    Port = "1433",
                    Username = "alirezaeiji151379"
                },
                TableType = TableType.Permanent,
                TableName = "Users",
                TableSchema ="testdb",
            };


            var bulkConfig = new BulkConfiguration()
            {
                BatchSize = 3,
            };

            var dataBulkReader = provider.GetService<IDataBaseBulkReader>();

            await foreach (var dt in dataBulkReader!.ReadDataInBulk(etlTable, bulkConfig))
            {
                foreach (DataRow row in dt.Rows)
                {
                    Console.WriteLine(row["Id"] + "   " + row["FullName"]);
                }
                Console.WriteLine("-----------------------------");
            }
        }
    }
}
