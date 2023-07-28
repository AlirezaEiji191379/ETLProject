using ETLProject.Common.Table;
using ETLProject.Contract.Where.Conditions;

namespace ETLProject.DataSource.QueryBusiness.WhereQueryBusiness.Abstractions;

public interface IWhereQueryBusiness
{
    ETLTable AddWhereCondition(ETLTable inputTable, Condition condition);
}