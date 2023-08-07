using ETLProject.Common.Table;
using ETLProject.Contract.DBReader;
using ETLProject.Contract.DbWriter;
using ETLProject.Contract.Sort;
using ETLProject.DataSource.QueryBusiness.DbAddBusiness.Abstractions;
using ETLProject.DataSource.QueryBusiness.DbReaderBusiness.Abstractions;
using ETLProject.DataSource.QueryBusiness.SortBusiness.Abstractions;
using ETLProject.Pipeline.Abstractions;
using ETLProject.Pipeline.Plugins;
using Microsoft.Extensions.DependencyInjection;

namespace ETLProject.Pipeline.Graph;

public class PluginExecutionTask
{
    public IPlugin Plugin { get; init; }
    private IDbTableReader _dbTableReader;
    private ITableSorter _tableSorter;
    private IDbAddBusiness _addBusiness;
    public List<ETLTable> InputTables { get; set; }
    public Task<ETLTable> NodeTask { get; init; }

    public PluginExecutionTask(IPlugin plugin)
    {
        Plugin = plugin;
        _dbTableReader = ServiceProviderContainer.ServiceProvider.GetService<IDbTableReader>();
        _tableSorter = ServiceProviderContainer.ServiceProvider.GetService<ITableSorter>();
        _addBusiness = ServiceProviderContainer.ServiceProvider.GetService<IDbAddBusiness>();
        InputTables = new List<ETLTable>();
        //NodeTask = RunPlugin();
    }


    public async Task<ETLTable> RunPlugin()
    {
        ETLTable result = null;
        if (Plugin is DbReaderPlugin dbReaderPlugin)
        {
            result = _dbTableReader.ReadTable(Plugin.PluginConfig as DbReaderContract);
        }
        else if (Plugin is OrderPlugin orderPlugin)
        {
            result = _tableSorter.SortTable(InputTables[0], Plugin.PluginConfig as SortContract);
        }
        else if (Plugin is DbAddPlugin)
        {
            await _addBusiness.WriteToTable(InputTables[0], Plugin.PluginConfig as DbWriterParameter);
        }
        return result;
    }
}