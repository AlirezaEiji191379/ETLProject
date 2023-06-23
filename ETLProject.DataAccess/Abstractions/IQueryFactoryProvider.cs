using ETLProject.Common.Table;
using SqlKata.Execution;

namespace ETLProject.DataSource.Query.Abstractions
{
    internal interface IQueryFactoryProvider
    {
        QueryFactory GetQueryFactory(ETLTable etlTable);
    }
}
