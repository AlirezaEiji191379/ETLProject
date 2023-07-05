using ETLProject.Common.Table;
using SqlKata.Execution;

namespace ETLProject.DataSource.Abstractions
{
    internal interface IQueryFactoryProvider
    {
        QueryFactory GetQueryFactory(ETLTable etlTable);
    }
}
