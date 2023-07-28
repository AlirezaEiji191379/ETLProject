namespace ETLProject.Contract.Aggregate;

public class AggregationParameter
{
    public List<string> GroupByColumns { get; set; }
    public List<AggregateColumns> AggregateColumns { get; set; }
}