using ETLProject.Common.Table;

namespace ETLProject.DataSource.Query.Abstractions
{
    internal interface ITableNameProvider
    {
        string GetTableName(ETLTable etlTable);
    }
}
