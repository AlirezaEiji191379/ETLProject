using ETLProject.Common.Table;
using ETLProject.Contract.Where.Conditions;
using ETLProject.DataSource.QueryBusiness.WhereQueryBusiness.Abstractions;

namespace ETLProject.DataSource.QueryBusiness.WhereQueryBusiness;

internal class WhereQueryBusiness : IWhereQueryBusiness
{
    private readonly IConditionBuilder _conditionBuilder;

    public WhereQueryBusiness(IConditionBuilder conditionBuilder)
    {
        _conditionBuilder = conditionBuilder;
    }

    public ETLTable AddWhereCondition(ETLTable inputTable, Condition condition)
    {
        var whereClause = _conditionBuilder.BuildCondition(condition,inputTable.AliasName);
        inputTable.Query.Where(whereClause);
        return inputTable;
    }
}