using ETLProject.Common.Table;
using ETLProject.Contract.Limit;
using ETLProject.DataSource.QueryBusiness.LimitBusiness.Abstractions;

namespace ETLProject.DataSource.QueryBusiness.LimitBusiness;

internal class LimitQueryMaker : ILimitQueryMaker
{
    public ETLTable AddLimitQuery(ETLTable etlTable, LimitContract limitContract)
    {
        var query = etlTable.Query;
        query.Limit(limitContract.Top);
        return etlTable;
    }
}