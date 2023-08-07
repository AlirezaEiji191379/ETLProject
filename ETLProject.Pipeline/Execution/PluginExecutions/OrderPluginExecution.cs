using ETLProject.Common.PipeLine.Abstractions;
using ETLProject.Common.PipeLine.Enums;
using ETLProject.Common.Table;
using ETLProject.Contract.Sort;
using ETLProject.DataSource.QueryBusiness.SortBusiness.Abstractions;
using ETLProject.Pipeline.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace ETLProject.Pipeline.Execution.PluginExecutions;

public class OrderPluginExecution : IPluginRunner
{
    private readonly ITableSorter _tableSorter;
    private ETLTable _etlTable;
    private SortContract? _sortContract;
    
    public PluginType PluginType => PluginType.Order;
    
    public OrderPluginExecution(SortContract sortContract)
    {
        _sortContract = sortContract;
        _tableSorter = ServiceProviderContainer.ServiceProvider.GetService<ITableSorter>();
    }
    
    public void AddInputEtlTable(ETLTable etlTable)
    {
        _etlTable = etlTable;
    }
    
    public async Task<ETLTable> RunPlugin()
    {
        return _tableSorter.SortTable(_etlTable,_sortContract);
    }
    
}