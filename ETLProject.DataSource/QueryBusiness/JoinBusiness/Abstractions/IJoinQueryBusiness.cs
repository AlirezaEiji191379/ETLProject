using ETLProject.Common.Table;
using ETLProject.Contract.Join;

namespace ETLProject.DataSource.QueryBusiness.JoinBusiness.Abstractions;

public interface IJoinQueryBusiness
{
    Task<ETLTable> JoinTables(ETLTable leftTable, ETLTable rightTable, JoinParameter joinParameter);
}