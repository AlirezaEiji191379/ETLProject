using ETLProject.Common.Table;
using ETLProject.Contract.Limit;

namespace ETLProject.DataSource.QueryBusiness.Limit.Abstractions;

public interface ILimitQueryMaker
{
    void AddLimitQuery(ETLTable etlTable,LimitContract limitContract);
}