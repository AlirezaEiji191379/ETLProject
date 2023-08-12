using ETLProject.Common.PipeLine.Abstractions;
using ETLProject.Common.PipeLine.Enums;
using ETLProject.Contract.Where.Conditions;
using ETLProject.Pipeline.Abstractions;

namespace ETLProject.Pipeline.Plugins;

public class WherePlugin : IPlugin
{
    public Guid PluginId { get; init; }
    public PluginType PluginType => PluginType.Where;
    public string PluginTitle { get; init; }
    public IPluginConfig PluginConfig { get; init; }

    public WherePlugin(string pluginTitle,Condition condition)
    {
        PluginConfig = condition;
        PluginTitle = pluginTitle;
        PluginId = Guid.NewGuid();
    }
    
}