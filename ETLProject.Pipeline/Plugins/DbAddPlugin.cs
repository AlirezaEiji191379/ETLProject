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
    public PluginRunState PluginRunState { get; set; }
    public IPluginConfig PluginConfig { get; init; }

    public void AddInputPlugin(Guid pluginId)
    {
        throw new NotImplementedException();
    }

    public void AddOutputPlugin(Guid pluginId)
    {
        throw new NotImplementedException();
    }

    public void AddInputTable(ETLTable inputSchema)
    {
        throw new NotImplementedException();
    }

    public List<ETLTable> GetInputTables()
    {
        throw new NotImplementedException();
    }

    public void AddOutputSchema()
    {
        throw new NotImplementedException();
    }
}