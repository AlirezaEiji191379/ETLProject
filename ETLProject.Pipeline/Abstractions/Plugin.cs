using ETLProject.Common.PipeLine.Abstractions;
using ETLProject.Common.PipeLine.Enums;
using ETLProject.Common.Table;

namespace ETLProject.Pipeline.Abstractions;

public abstract class Plugin
{
    Guid PluginId { get; init; }
    PluginType PluginType { get; }
    string PluginTitle { get; init; }
    IPluginConfig PluginConfig { get; init; }
    int InputPlugins { get; }
    int? OutputPlugins { get; }
    public abstract void AddInputPlugin(Guid pluginId);
    public abstract void AddOutputPlugin(Guid pluginId);

    public virtual void AddResultETLTableToNextPlugins(Guid currentPluginId, ETLTable etlTable)
    {
        
    }
}