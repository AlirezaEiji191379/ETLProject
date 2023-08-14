using ETLProject.Common.PipeLine.Abstractions;

namespace ETLProject.Contract.Where.Conditions;

public class Condition : IPluginConfig
{
    public FieldCondition? FieldCondition { get; set; }
    public LogicalCondition? LogicalCondition { get; set; }
    public bool IsFieldCondition { get; set; }
}