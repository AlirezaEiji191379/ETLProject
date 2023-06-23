using ETLProject.Common.Table;
using ETLProject.DataSource.Query.Abstractions;
using System.Text;

namespace ETLProject.DataSource.Query.Common.Utilities
{
    internal class TableNameProvider : ITableNameProvider
    {
        public string GetTableName(ETLTable etlTable)
        {
            if (etlTable.DatabaseConnection.Schema == null)
                return etlTable.TableName;

            return new StringBuilder(etlTable.DatabaseConnection.Schema).Append("." + etlTable.TableName).ToString();
        }
    }
}
