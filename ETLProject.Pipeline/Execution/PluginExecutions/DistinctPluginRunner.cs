using ETLProject.Common.PipeLine.Abstractions;
using ETLProject.Common.PipeLine.Enums;
using ETLProject.Common.Table;
using ETLProject.DataSource.QueryBusiness.DistinctBusiness.Abstractions;
using ETLProject.Pipeline.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace ETLProject.Pipeline.Execution.PluginExecutions;

public class DistinctPluginRunner : IPluginRunner
{
    private readonly IDistinctQueryBusiness _distinctQueryBusiness;
    private ETLTable _etlTable;

    public DistinctPluginRunner()
    {
        _distinctQueryBusiness = ServiceProviderContainer.ServiceProvider.GetService<IDistinctQueryBusiness>();
    }

    public void AddInputEtlTable(ETLTable etlTable)
    {
        _etlTable = etlTable;
    }

    public PluginType PluginType => PluginType.Distinct;
    public async Task<ETLTable> RunPlugin()
    {
        return _distinctQueryBusiness.DistinctTable(_etlTable);
    }
}