using ETLProject.Common.PipeLine.Abstractions;
using ETLProject.Common.PipeLine.Enums;
using ETLProject.Common.Table;
using ETLProject.Contract.Where.Conditions;
using ETLProject.DataSource.QueryBusiness.WhereQueryBusiness.Abstractions;
using ETLProject.Pipeline.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace ETLProject.Pipeline.Execution.PluginExecutions;

public class WherePluginExecution : IPluginRunner
{
    private readonly IWhereQueryBusiness _whereQueryBusiness;
    private ETLTable _etlTable;
    private Condition _condition;
    
    public WherePluginExecution(Condition condition)
    {
        _condition = condition;
        _whereQueryBusiness = ServiceProviderContainer.ServiceProvider.GetService<IWhereQueryBusiness>();
    }

    public void AddInputEtlTable(ETLTable etlTable)
    {
        _etlTable = etlTable;
    }

    public PluginType PluginType => PluginType.Where;
    public async Task<ETLTable> RunPlugin()
    {
        return _whereQueryBusiness.AddWhereCondition(_etlTable,_condition);
    }
}