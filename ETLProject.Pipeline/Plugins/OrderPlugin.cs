using ETLProject.Common.PipeLine.Abstractions;
using ETLProject.Common.PipeLine.Enums;
using ETLProject.Common.Table;
using ETLProject.Contract.Sort;
using ETLProject.Pipeline.Abstractions;
using ETLProject.Pipeline.Common.Enums;
using ETLProject.Pipeline.Common.Exceptions;

namespace ETLProject.Pipeline.Plugins;

public class OrderPlugin : IPlugin
{
    public Guid PluginId { get; init; }
    public PluginType PluginType => PluginType.Order;
    public string PluginTitle { get; init; }
    

    public IPluginConfig PluginConfig { get; init; }
    

    public OrderPlugin(string pluginTitle, SortContract sortContract)
    {
        PluginId = Guid.NewGuid();
        PluginTitle = pluginTitle;
        PluginConfig = sortContract;
    }
    
}