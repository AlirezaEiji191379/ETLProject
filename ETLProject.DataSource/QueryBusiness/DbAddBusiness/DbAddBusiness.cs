using ETLProject.Common.Abstractions;
using ETLProject.Common.Table;
using ETLProject.Contract.DbWriter;
using ETLProject.DataSource.Abstractions;
using ETLProject.DataSource.QueryBusiness.DbAddBusiness.Abstractions;

namespace ETLProject.DataSource.QueryBusiness.DbAddBusiness;

internal class DbAddBusiness : IDbAddBusiness
{
    private readonly IRandomStringGenerator _randomStringGenerator;
    private readonly IDataTransfer _dataTransfer;
    
    public void AddToTable(ETLTable inputTable, DbWriterParameter dbWriterParameter)
    {
        if (dbWriterParameter.TableType == TableType.Temp)
        {
            
        }
        else
        {
            
        }
    }
    
    
}