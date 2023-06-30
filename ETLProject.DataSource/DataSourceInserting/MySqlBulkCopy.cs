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
            ConvertDataTableToCsv(dataTable,filePath);
            var columnSet = string.Format($"({string.Join(",", etlTable.Columns.Select(x => x.Name))})");
            //TODO: refator Query generation
            var queryString = $"LOAD DATA INFILE '{filePath}' INTO TABLE {etlTable.TableName} "+
                        "FIELDS TERMINATED BY ',' " +
                        "ENCLOSED BY '\"' " +
                        "LINES TERMINATED BY '\n' " +
                        $"{columnSet}";
            var queryFactory = _queryFactoryProvider.GetQueryFactory(etlTable);
            await queryFactory.StatementAsync(queryString);
        }


        private void ConvertDataTableToCsv(DataTable dataTable, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // Write the column headers
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    writer.Write(dataTable.Columns[i].ColumnName);
                    if (i < dataTable.Columns.Count - 1)
                    {
                        writer.Write(",");
                    }
                }
                writer.WriteLine();

                // Write the data rows
                foreach (DataRow row in dataTable.Rows)
                {
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        writer.Write(row[i].ToString());
                        if (i < dataTable.Columns.Count - 1)
                        {
                            writer.Write(",");
                        }
                    }
                    writer.WriteLine();
                }
            }
        }
    }
}
