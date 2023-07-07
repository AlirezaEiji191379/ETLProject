using ETLProject.Common.Table;
using ETLProject.Contract.Limit;
using ETLProject.DataSource.QueryBusiness.Limit.Abstractions;

namespace ETLProject.DataSource.QueryBusiness.Limit;

public class LimitQueryMaker : ILimitQueryMaker
{
    public void AddLimitQuery(ETLTable etlTable, LimitContract limitContract)
    {
        var query = etlTable.Query;
        query.Limit(limitContract.Top);
    }
}