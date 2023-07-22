using ETLProject.Common.Table;
using ETLProject.Contract.DbWriter;

namespace ETLProject.DataSource.QueryBusiness.DbAddBusiness.Abstractions;

public interface IDbAddBusiness
{
    Task WriteToTable(ETLTable inputTable, DbWriterParameter dbWriterParameter);
}