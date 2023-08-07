using ETLProject.Common.PipeLine.Abstractions;
using ETLProject.Common.PipeLine.Enums;
using ETLProject.Common.Table;
using ETLProject.Contract.DBReader;
using ETLProject.DataSource.QueryBusiness.DbReaderBusiness.Abstractions;
using ETLProject.Pipeline.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace ETLProject.Pipeline.Execution.PluginExecutions;

public class DbReadPluginExecution : IPluginRunner
{
    public DbReadPluginExecution(DbReaderContract dbReaderContract)
    {
        _dbReaderContract = dbReaderContract;
        _dbTableReader = ServiceProviderContainer.ServiceProvider.GetService<IDbTableReader>();
    }

    public PluginType PluginType => PluginType.Read;
    private DbReaderContract _dbReaderContract;
    private readonly IDbTableReader _dbTableReader;
    
    public void AddInputEtlTable(ETLTable etlTable)
    {
        throw new Exception("db reader plugin does not have input table or input edge!");
    }
    
    public async Task<ETLTable> RunPlugin()
    {
        return _dbTableReader.ReadTable(_dbReaderContract);
    }
}