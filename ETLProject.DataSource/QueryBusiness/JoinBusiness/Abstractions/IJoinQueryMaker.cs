using ETLProject.Common.Table;
using ETLProject.Contract.Join;
using SqlKata;

namespace ETLProject.DataSource.QueryBusiness.JoinBusiness.Abstractions;

internal interface IJoinQueryMaker
{
    void AddJoinQueryToResultTable(ETLTable leftTable, ETLTable rightTable, ETLTable resultTable, JoinParameter joinParameter);
}