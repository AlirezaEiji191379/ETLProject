using ETLProject.Contract.Where.Enums;

namespace ETLProject.Contract.Where.Conditions;

public class LogicalCondition
{
    public LogicalOperator LogicalOperator { get; set; }
    public List<Condition> ChildConditions { get; set; }
}