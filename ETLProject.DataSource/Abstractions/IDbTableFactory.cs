using ETLProject.Common.Table;
using System.Data;

namespace ETLProject.DataSource.Abstractions
{
    public interface IDbTableFactory
    {
        Task CreateTable(ETLTable etlTable);
        Task CreateTableAs(ETLTable etlTable, string newTableName, TableType newTableType);
        Task SelectInto(ETLTable etlTable, string newTableName);
    }
}
