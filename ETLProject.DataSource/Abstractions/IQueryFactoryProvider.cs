using System.Data;
using ETLProject.Common.Database;
using ETLProject.Common.Table;
using SqlKata.Execution;

namespace ETLProject.DataSource.Abstractions
{
    internal interface IQueryFactoryProvider
    {
        QueryFactory GetQueryFactory(ETLTable etlTable);
        QueryFactory GetQueryFactoryByConnection(DatabaseConnectionParameters databaseConnectionParameters);
        IDbConnection GetDbConnection(DatabaseConnectionParameters databaseConnectionParameters);
    }
}
