using ETLProject.Contract.Where.Conditions;
using Microsoft.EntityFrameworkCore;
using SqlKata;

namespace ETLProject.DataSource.QueryBusiness.WhereQueryBusiness.Abstractions;

internal interface IConditionBuilder
{
    Func<Query,Query> BuildCondition(Condition condition, string tableAliasName);
}