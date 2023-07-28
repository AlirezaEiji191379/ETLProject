using ETLProject.Common.Table;
using ETLProject.Contract.Where.Conditions;
using ETLProject.DataSource.QueryBusiness.WhereQueryBusiness.Abstractions;

namespace ETLProject.DataSource.QueryBusiness.WhereQueryBusiness;

internal class WhereQueryBusiness : IWhereQueryBusiness
{
    public ETLTable AddWhereCondition(ETLTable inputTable, Condition condition)
    {
        return null;
    }
}