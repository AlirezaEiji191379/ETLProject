using ETLProject.Contract.Where.Enums;
using ETLProject.Contract.Where.Values;

namespace ETLProject.Contract.Where.Conditions;

public class FieldCondition : Condition
{
    public string ColumnName { get; set; }
    public ConditionType ConditionType { get; set; }
    public ConditionValue ConditionValue { get; set; }
}