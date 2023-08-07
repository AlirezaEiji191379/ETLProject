using ETLProject.Common.PipeLine.Abstractions;
using ETLProject.Common.PipeLine.Enums;
using ETLProject.Common.Table;
using ETLProject.Contract.DbWriter;
using ETLProject.Pipeline.Abstractions;
using ETLProject.Pipeline.Common.Enums;

namespace ETLProject.Pipeline.Plugins;

public class DbAddPlugin : IPlugin
{
    public DbAddPlugin(string pluginTitle, DbWriterParameter dbWriterParameter)
    {
        PluginId = Guid.NewGuid();
        PluginConfig = dbWriterParameter;
    }
    public Guid PluginId { get; init; }
    public PluginType PluginType => PluginType.Write;
    public string PluginTitle { get; init; }
    public IPluginConfig PluginConfig { get; init; }
}