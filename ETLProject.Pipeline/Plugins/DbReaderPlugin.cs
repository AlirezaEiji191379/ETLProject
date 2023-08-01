﻿using ETLProject.Common.PipeLine.Abstractions;
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
    
    public PluginRunState PluginRunState
    {
        get;
        set;
    }

    public IPluginConfig PluginConfig { get; init; }

    public List<Guid> OutputPlugins { get;}

    public DbReaderPlugin(string pluginTitle, DbReaderContract dbReaderContract)
    {
        PluginId = Guid.NewGuid();
        PluginConfig = dbReaderContract;
        PluginTitle = pluginTitle;
        OutputPlugins = new List<Guid>();
        PluginRunState = PluginRunState.ReadyToRun;
    }

    public void AddInputPlugin(Guid pluginId)
    {
        throw new OutputPluginExceededException("the db reader plugin can not have input plugins!");
    }

    public void AddOutputPlugin(Guid pluginId)
    {
        OutputPlugins.Add(pluginId);
    }

    public void AddInputTable(ETLTable inputSchema)
    {
        return;
    }

    public List<ETLTable> GetInputTables()
    {
        return new List<ETLTable>();
    }

    public void AddOutputSchema()
    {
        
    }
}