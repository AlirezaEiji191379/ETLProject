using ETLProject.Common.PipeLine.Abstractions;
using ETLProject.Common.PipeLine.Enums;
using ETLProject.Common.Table;
using ETLProject.Contract.DBReader;
using ETLProject.Pipeline.Abstractions;
using ETLProject.Pipeline.Common.Enums;
using ETLProject.Pipeline.Common.Exceptions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ETLProject.Pipeline.Plugins;

public class DbReaderPlugin : IPlugin
{
    public Guid PluginId { get; init; }
    public PluginType PluginType => PluginType.Read;
    public string PluginTitle { get; init; }
    
    public IPluginConfig PluginConfig { get; init; }

    public DbReaderPlugin(string pluginTitle, DbReaderContract dbReaderContract)
    {
        PluginId = Guid.NewGuid();
        PluginConfig = dbReaderContract;
        PluginTitle = pluginTitle;
    }
    
}