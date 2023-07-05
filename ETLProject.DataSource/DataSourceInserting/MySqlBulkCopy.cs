using ETLProject.Common.Abstractions;
using ETLProject.Common.Database;
using ETLProject.Common.Table;
using ETLProject.DataSource.Abstractions;
using System.Data;

namespace ETLProject.DataSource.DataSourceInserting
{
    internal class MySqlBulkCopy : IDataBulkInserter
    {
        private readonly IRandomStringGenerator _randomStringGenerator;
        private readonly IQueryFactoryProvider _queryFactoryProvider;
        public DataSourceType DataSourceType => DataSourceType.MySql;

        public MySqlBulkCopy(IRandomStringGenerator randomStringGenerator, IQueryFactoryProvider queryFactoryProvider)
        {
            _randomStringGenerator = randomStringGenerator;
            _queryFactoryProvider = queryFactoryProvider;
        }

        public async Task InsertBulk(DataTable dataTable, ETLTable etlTable)
        {
            string filePath = "";
            while (true)
            {
                var fileName = _randomStringGenerator.GenerateRandomString() + ".csv";
                filePath = AppDomain.CurrentDomain.BaseDirectory + fileName;
                if (!File.Exists(filePath))
                    break;
            }
            await ConvertDataTableToCsvAsync(dataTable,filePath);
            var columnSet = string.Format($"({string.Join(",", etlTable.Columns.Select(x => x.Name))})");
            //TODO: refator Query generation
            var queryString = $"LOAD DATA INFILE '{filePath.Replace(@"\",@"\\")}' INTO TABLE {etlTable.TableName} "+
                        "FIELDS TERMINATED BY ',' " +
                        "ENCLOSED BY '\"' " +
                        "LINES TERMINATED BY '\n' " +
                        $"{columnSet}";
            var queryFactory = _queryFactoryProvider.GetQueryFactory(etlTable);
            await queryFactory.StatementAsync(queryString);
            File.Delete(filePath);
        }


        private async Task ConvertDataTableToCsvAsync(DataTable dataTable, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        await writer.WriteAsync(row[i].ToString());
                        if (i < dataTable.Columns.Count - 1)
                        {
                            await writer.WriteAsync(",");
                        }
                    }
                    await writer.WriteLineAsync();
                }
            }
        }
    }
}
