using ETLProject.Common.PipeLine.Abstractions;
using ETLProject.Common.PipeLine.Enums;
using ETLProject.Common.Table;
using ETLProject.Contract.Limit;
using ETLProject.DataSource.QueryBusiness.LimitBusiness.Abstractions;
using ETLProject.Pipeline.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace ETLProject.Pipeline.Execution.PluginExecutions;

public class LimitPluginRunner : IPluginRunner
{
    private ILimitQueryBusiness _limitQueryBusiness;
    private ETLTable _etlTable;
    private LimitContract _limitContract;
    
    public LimitPluginRunner(LimitContract limitContract)
    {
        _limitContract = limitContract;
        _limitQueryBusiness = ServiceProviderContainer.ServiceProvider.GetService<ILimitQueryBusiness>();
    }

    public void AddInputEtlTable(ETLTable etlTable)
    {
        _etlTable = etlTable;
    }

    public PluginType PluginType => PluginType.Limit;
    public async Task<ETLTable> RunPlugin()
    {
        return _limitQueryBusiness.AddLimitQuery(_etlTable,_limitContract);
    }
}