using ETLProject.Common.PipeLine.Abstractions;
using ETLProject.Common.PipeLine.Enums;
using ETLProject.Common.Table;

namespace ETLProject.Pipeline.Abstractions;

public interface IPlugin
{
    Guid PluginId { get; init; }
    PluginType PluginType { get; }
    string PluginTitle { get; init; }
    IPluginConfig PluginConfig { get; init; }
    public void AddInputPlugin(Guid pluginId);
    public void AddOutputPlugin(Guid pluginId);
}