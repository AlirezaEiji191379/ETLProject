using ETLProject.Common.PipeLine.Abstractions;
using ETLProject.Common.PipeLine.Enums;
using ETLProject.Contract.Join;
using ETLProject.Pipeline.Abstractions;

namespace ETLProject.Pipeline.Plugins;

public class JoinPlugin : IPlugin
{
    public Guid PluginId { get; init; }
    public PluginType PluginType => PluginType.Join;
    public string PluginTitle { get; init; }
    public IPluginConfig PluginConfig { get; init; }

    public JoinPlugin(string pluginTitle, JoinParameter joinParameter)
    {
        PluginConfig = joinParameter;
        PluginTitle = pluginTitle;
        PluginId = Guid.NewGuid();
    }
}