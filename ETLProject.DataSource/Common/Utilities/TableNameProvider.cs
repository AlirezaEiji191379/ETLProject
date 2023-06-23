using ETLProject.Common.Table;
using ETLProject.DataSource.Abstractions;
using System.Text;

namespace ETLProject.DataSource.Common.Utilities
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
