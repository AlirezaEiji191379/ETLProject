using ETLProject.Common.PipeLine.Abstractions;
using ETLProject.Common.PipeLine.Enums;
using ETLProject.Common.Table;
using ETLProject.Contract.Join;
using ETLProject.DataSource.QueryBusiness.JoinBusiness.Abstractions;
using ETLProject.Pipeline.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace ETLProject.Pipeline.Execution.PluginExecutions;

public class JoinPluginRunner : IPluginRunner
{
    public JoinPluginRunner(JoinParameter joinParameter)
    {
        _joinQueryBusiness = ServiceProviderContainer.ServiceProvider.GetService<IJoinQueryBusiness>();
        _inputTables = new List<ETLTable>();
        _joinParameter = joinParameter;
    }

    public PluginType PluginType => PluginType.Join;
    private readonly IJoinQueryBusiness _joinQueryBusiness;
    private JoinParameter _joinParameter;
    private List<ETLTable> _inputTables;

    public void AddInputEtlTable(ETLTable etlTable)
    {
        _inputTables.Add(etlTable);
    }
    public async Task<ETLTable> RunPlugin()
    {
        return await _joinQueryBusiness.JoinTables(_inputTables[0],_inputTables[1],_joinParameter);
    }
}