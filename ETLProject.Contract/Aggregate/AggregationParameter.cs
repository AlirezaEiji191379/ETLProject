using ETLProject.Common.PipeLine.Abstractions;

namespace ETLProject.Contract.Aggregate;

public class AggregationParameter : IPluginConfig
{
    public List<string> GroupByColumns { get; set; }
    public List<AggregateColumns> AggregateColumns { get; set; }
}