using ETLProject.Contract.Aggregate;
using ETLProject.Contract.DBReader;
using ETLProject.Contract.DbWriter;
using ETLProject.Contract.Join;
using ETLProject.Contract.Limit;
using ETLProject.Contract.Sort;
using ETLProject.Contract.Where.Conditions;

namespace ETLProject.Contract.Pipeline;

public class PluginConfigDto
{
    public AggregationParameter? Agg { get; set; }
    public DbReaderContract? DbReader { get; set; }
    public DbWriterParameter? DbWriter { get; set; }
    public JoinParameter? Join { get; set; }
    public LimitContract? LimitContract { get; set; }
    public SortContract? SortContract { get; set; }
    public Condition? Condition { get; set; }
}