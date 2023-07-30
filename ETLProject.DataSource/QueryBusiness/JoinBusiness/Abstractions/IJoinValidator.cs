using ETLProject.Common.Table;
using ETLProject.Contract.Join;

namespace ETLProject.DataSource.QueryBusiness.JoinBusiness.Abstractions;

internal interface IJoinValidator
{
    void ValidateJoinParameter(ETLTable leftTable, ETLTable rightTable, JoinParameter joinParameter);
}