using ETLProject.Contract.Aggregate.Enums;

namespace ETLProject.Contract.Aggregate;

public class AggregateColumns
{
    public string? AliasName { get; set; }
    public string ColumnName { get; set; }
    public AggregateType AggregateType { get; set; }
}