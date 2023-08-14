using System.Text;
using ETLProject.Contract.Where.Conditions;
using ETLProject.Contract.Where.Enums;
using ETLProject.DataSource.QueryBusiness.WhereQueryBusiness.Abstractions;
using Microsoft.Extensions.Primitives;
using SqlKata;

namespace ETLProject.DataSource.QueryBusiness.WhereQueryBusiness;

internal class ConditionBuilder : IConditionBuilder
{
    public string TableAliasName { get; set; }

    public Func<Query,Query> BuildCondition(Condition condition, string tableAliasName)
    {
        if (!condition.IsFieldCondition)
        {
            return BuildLogicalCondition(condition.LogicalCondition, tableAliasName);
        }
        else if (condition.IsFieldCondition)
        {
            return BuildFieldCondition(condition.FieldCondition, tableAliasName);
        }

        return null;
    }

    private Func<Query, Query> BuildFieldCondition(
        FieldCondition fieldCondition,
        string tableAliasName)
    {
        var columnFullName = new StringBuilder(tableAliasName).Append(".").Append(fieldCondition.ColumnName).ToString();
        switch (fieldCondition.ConditionType)
        {
            case ConditionType.Equals:
                return query => query.Where(columnFullName, "=", fieldCondition.Value);
            case ConditionType.NotEquals:
                return query => query.Where(columnFullName, "<>", fieldCondition.Value);
            case ConditionType.GreaterThan:
                return query => query.Where(columnFullName, ">", fieldCondition.Value);
            case ConditionType.GreaterThanOrEqual:
                return query => query.Where(columnFullName, ">=", fieldCondition.Value);
            case ConditionType.LowerThan:
                return query => query.Where(columnFullName, "<", fieldCondition.Value);
            case ConditionType.LowerThanOrEqual:
                return query => query.Where(columnFullName, "<=", fieldCondition.Value);
            default:
                throw new NotSupportedException();
        }
    }

    private Func<Query, Query> BuildLogicalCondition(
        LogicalCondition logicalCondition,
        string tableAliasName)
    {
        var internalQueries = logicalCondition.ChildConditions.Select(x =>
        {
            if (!x.IsFieldCondition)
            {
                return BuildLogicalCondition(x.LogicalCondition, tableAliasName);
            }

            if (x.IsFieldCondition)
            {
                return BuildFieldCondition(x.FieldCondition, tableAliasName);
            }

            return null;
        });

        return query =>
        {
            foreach (var internalQuery in internalQueries)
            {
                if (logicalCondition.LogicalOperator == LogicalOperator.And)
                {
                    query.Where(internalQuery);
                }
                else
                {
                    query.OrWhere(internalQuery);
                }
            }

            return query;
        };
    }
}