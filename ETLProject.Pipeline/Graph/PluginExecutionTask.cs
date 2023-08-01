using ETLProject.Common.Table;
using ETLProject.Contract.DBReader;
using ETLProject.Contract.Sort;
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

    public PluginExecutionTask(IPlugin plugin)
    {
        Plugin = plugin;
        _dbTableReader = ServiceProviderContainer.ServiceProvider.GetService<IDbTableReader>();
        _tableSorter = ServiceProviderContainer.ServiceProvider.GetService<ITableSorter>();

    }


    public Task<ETLTable> RunPlugin()
    {   
        return new Task<ETLTable>( () =>
        {
            Console.WriteLine("Hi");
            ETLTable result = null;
            if (Plugin is DbReaderPlugin dbReaderPlugin)
            {
                result = _dbTableReader.ReadTable(Plugin.PluginConfig as DbReaderContract);
            }
            else if(Plugin is OrderPlugin orderPlugin)
            {
                result = _tableSorter.SortTable(Plugin.GetInputTables()[0],Plugin.PluginConfig as SortContract);
            }
            return result;
        });
    }
    
}