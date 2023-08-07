using ETLProject.Common.PipeLine.Abstractions;
using ETLProject.Common.PipeLine.Enums;
using ETLProject.Contract.Aggregate;
using ETLProject.Pipeline.Abstractions;

namespace ETLProject.Pipeline.Plugins;

public class AggPlugin : IPlugin
{
    public Guid PluginId { get; init; }
    public PluginType PluginType => PluginType.Agg;
    public string PluginTitle { get; init; }
    public IPluginConfig PluginConfig { get; init; }

    public AggPlugin(string pluginTitle,AggregationParameter aggregationParameter)
    {
        PluginConfig = aggregationParameter;
        PluginTitle = pluginTitle;
        PluginId = Guid.NewGuid();
    }
}