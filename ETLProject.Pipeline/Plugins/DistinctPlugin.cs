using ETLProject.Common.PipeLine.Abstractions;
using ETLProject.Common.PipeLine.Enums;
using ETLProject.Pipeline.Abstractions;

namespace ETLProject.Pipeline.Plugins;

public class DistinctPlugin : IPlugin
{
    public Guid PluginId { get; init; }
    public PluginType PluginType => PluginType.Distinct;
    public string PluginTitle { get; init; }
    public IPluginConfig PluginConfig { get; init; }

    public DistinctPlugin(string pluginTitle)
    {
        PluginTitle = pluginTitle;
        PluginId = Guid.NewGuid();
    }
}