using ETLProject.Common.PipeLine.Abstractions;
using ETLProject.Common.PipeLine.Enums;
using ETLProject.Common.Table;
using ETLProject.Pipeline.Common.Enums;

namespace ETLProject.Pipeline.Abstractions;

public interface IPlugin
{
    Guid PluginId { get; init; }
    PluginType PluginType { get; }
    string PluginTitle { get; init; }
    PluginRunState PluginRunState { get; set; }
    IPluginConfig PluginConfig { get; init; }
    void AddInputPlugin(Guid pluginId);
    void AddOutputPlugin(Guid pluginId);
    void AddInputTable(ETLTable inputSchema);
    List<ETLTable> GetInputTables();
    void AddOutputSchema();
}