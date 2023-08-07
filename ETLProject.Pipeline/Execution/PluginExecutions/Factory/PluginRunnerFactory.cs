using ETLProject.Common.PipeLine.Abstractions;
using ETLProject.Common.PipeLine.Enums;
using ETLProject.Contract.Aggregate;
using ETLProject.Contract.DBReader;
using ETLProject.Contract.DbWriter;
using ETLProject.Contract.Limit;
using ETLProject.Contract.Sort;
using ETLProject.Contract.Where.Conditions;
using ETLProject.Pipeline.Abstractions;

namespace ETLProject.Pipeline.Execution.PluginExecutions.Factory;

public class PluginRunnerFactory : IPluginRunnerFactory
{
    public IPluginRunner GetPluginRunner(PluginType pluginType,IPluginConfig pluginConfig)
    {
        switch (pluginType)
        {
            case PluginType.Read:
                return new DbReadPluginExecution(pluginConfig as DbReaderContract);
            case PluginType.Write:
                return new DbWritePluginExecution(pluginConfig as DbWriterParameter);
            case PluginType.Distinct:
                return new DistinctPluginRunner();
            case PluginType.Agg:
                return new AggregationPluginRunner(pluginConfig as AggregationParameter);
            case PluginType.Limit:
                return new LimitPluginRunner(pluginConfig as LimitContract);
            case PluginType.Order:
                return new OrderPluginExecution(pluginConfig as SortContract);
            case PluginType.Where:
                return new WherePluginExecution(pluginConfig as Condition);
            default:
                throw new NotSupportedException();
        }
    }
}