using ETLProject.Common.Table;

namespace ETLProject.DataSource.Abstractions
{
    internal interface ITableNameProvider
    {
        string GetTableName(ETLTable etlTable);
    }
}
