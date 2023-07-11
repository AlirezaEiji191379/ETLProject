using ETLProject.Common.Table;
using ETLProject.Contract.Limit;

namespace ETLProject.DataSource.QueryBusiness.LimitBusiness.Abstractions;

public interface ILimitQueryMaker
{
    ETLTable AddLimitQuery(ETLTable etlTable, LimitContract limitContract);
}