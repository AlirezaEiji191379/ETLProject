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
    IPluginConfig PluginConfig { get; init; }
}