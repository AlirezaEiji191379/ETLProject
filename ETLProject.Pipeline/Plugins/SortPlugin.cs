using ETLProject.Common.PipeLine.Abstractions;
using ETLProject.Common.PipeLine.Enums;
using ETLProject.Pipeline.Abstractions;

namespace ETLProject.Pipeline.Plugins;

public class SortPlugin : IPlugin
{
    public Guid PipeLineId { get; init; }
    public Guid PluginId { get; init; }
    public PluginType PluginType { get; }
    public string PluginTitle { get; init; }
    public IPluginConfig PluginConfig { get; init; }
    public void AddInputPlugin(Guid pluginId)
    {
        throw new NotImplementedException();
    }

    public void AddOutputPlugin(Guid pluginId)
    {
        throw new NotImplementedException();
    }
}