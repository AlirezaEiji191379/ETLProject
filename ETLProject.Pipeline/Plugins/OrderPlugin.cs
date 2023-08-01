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
    
    public PluginRunState PluginRunState
    {
        get;
        set;
    }

    public IPluginConfig PluginConfig { get; init; }

    private readonly List<Guid> _inputPlugins;
    private readonly List<Guid> _outputPlugins;
    private ETLTable? _inputTable;

    public OrderPlugin(string pluginTitle, SortContract sortContract)
    {
        PluginId = Guid.NewGuid();
        PluginTitle = pluginTitle;
        PluginConfig = sortContract;
        _inputPlugins = new List<Guid>();
        _outputPlugins = new List<Guid>();
        PluginRunState = PluginRunState.NotReady;
    }

    public void AddInputPlugin(Guid pluginId)
    {
        if (_outputPlugins.Count == 1)
        {
            throw new InputPluginExceededException("Order plugin inputs can not be more than 1");
        }

        _inputPlugins.Add(pluginId);
    }

    public void AddOutputPlugin(Guid pluginId)
    {
        _outputPlugins.Add(pluginId);
    }

    public void AddInputTable(ETLTable inputTable)
    {
        if (inputTable == null)
        {
            throw new ArgumentNullException(nameof(inputTable));
        }
        if (_inputTable == null)
        {
            _inputTable = inputTable;
            if (PluginRunState == PluginRunState.NotReady)
            {
                PluginRunState = PluginRunState.ReadyToRun;
            }
        }
    }

    public List<ETLTable> GetInputTables()
    {
        return new List<ETLTable>()
        {
            _inputTable
        };
    }

    public void AddOutputSchema()
    {
        throw new NotImplementedException();
    }
}