using ETLProject.Common.PipeLine.Abstractions;
using ETLProject.Common.PipeLine.Enums;
using ETLProject.Common.Table;
using ETLProject.Contract.Aggregate;
using ETLProject.DataSource.QueryBusiness.AggregateBusiness.Abstractions;
using ETLProject.Pipeline.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace ETLProject.Pipeline.Execution.PluginExecutions;

public class AggregationPluginRunner : IPluginRunner
{
    private AggregationParameter _aggregationParameter;
    private IAggregateQueryBusiness _aggregateQueryBusiness;
    private ETLTable _etlTable;

    public AggregationPluginRunner(AggregationParameter aggregationParameter)
    {
        _aggregationParameter = aggregationParameter;
        _aggregateQueryBusiness = ServiceProviderContainer.ServiceProvider.GetService<IAggregateQueryBusiness>();
    }
    
    public void AddInputEtlTable(ETLTable etlTable)
    {
        _etlTable = etlTable;
    }

    public PluginType PluginType => PluginType.Agg;
    public async Task<ETLTable> RunPlugin()
    {
        return _aggregateQueryBusiness.AddAggregation(_etlTable,_aggregationParameter);
    }
    
}