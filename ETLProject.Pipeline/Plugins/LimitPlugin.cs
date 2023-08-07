using ETLProject.Common.PipeLine.Abstractions;
using ETLProject.Common.PipeLine.Enums;
using ETLProject.Contract.Limit;
using ETLProject.Pipeline.Abstractions;

namespace ETLProject.Pipeline.Plugins;

public class LimitPlugin : IPlugin
{
    public Guid PluginId { get; init; }
    public PluginType PluginType => PluginType.Limit;
    public string PluginTitle { get; init; }
    public IPluginConfig PluginConfig { get; init; }

    public LimitPlugin(string pluginTitle,LimitContract limitContract)
    {
        PluginConfig = limitContract;
        PluginTitle = pluginTitle;
        PluginId = Guid.NewGuid();
    }
}