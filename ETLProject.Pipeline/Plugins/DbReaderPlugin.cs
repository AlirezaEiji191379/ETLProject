using ETLProject.Common.PipeLine.Abstractions;
using ETLProject.Common.PipeLine.Enums;
using ETLProject.Pipeline.Abstractions;

namespace ETLProject.Pipeline.Plugins;

public class DbReaderPlugin : IPlugin
{
    public Guid PluginId { get; init; }
    public PluginType PluginType => PluginType.Read;
    public string PluginTitle { get; init; }
    public IPluginConfig PluginConfig { get; init; }


    public DbReaderPlugin(string pluginTitle)
    {
        PluginId = Guid.NewGuid();
        
    }
    
    public void AddInputPlugin(Guid pluginId)
    {
        throw new NotImplementedException();
    }

    public void AddOutputPlugin(Guid pluginId)
    {
        throw new NotImplementedException();
    }
}