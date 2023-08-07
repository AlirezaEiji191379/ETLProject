using ETLProject.Common.PipeLine.Abstractions;
using ETLProject.Common.PipeLine.Enums;
using ETLProject.Common.Table;
using ETLProject.Contract.DbWriter;
using ETLProject.DataSource.QueryBusiness.DbAddBusiness.Abstractions;
using ETLProject.Pipeline.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace ETLProject.Pipeline.Execution.PluginExecutions;

public class DbWritePluginExecution : IPluginRunner
{
    public DbWritePluginExecution(DbWriterParameter dbWriterParameter)
    {
        _dbWriterParameter = dbWriterParameter;
        _dbAddBusiness = ServiceProviderContainer.ServiceProvider.GetService<IDbAddBusiness>();
    }

    public PluginType PluginType => PluginType.Write;
    private DbWriterParameter _dbWriterParameter;
    private ETLTable _etlTable;
    private readonly IDbAddBusiness _dbAddBusiness;
    
    public void AddInputEtlTable(ETLTable etlTable)
    {
        _etlTable = etlTable;
    }
    
    public async Task<ETLTable> RunPlugin()
    {
        await _dbAddBusiness.WriteToTable(_etlTable,_dbWriterParameter);
        return null;
    }
    
}