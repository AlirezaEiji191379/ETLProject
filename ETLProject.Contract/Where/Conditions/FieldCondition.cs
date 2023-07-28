using ETLProject.Contract.Where.Enums;

namespace ETLProject.Contract.Where.Conditions;

public class FieldCondition : Condition
{
    public string ColumnName { get; set; }
    public ConditionType ConditionType { get; set; }
    public object Value { get; set; }
}