using ETLProject.Common.Table;
using System.Data;

namespace ETLProject.DataSource.Abstractions
{
    internal interface IDbTableFactory
    {
        Task CreateTempTable(ETLTable etlTable);
    }
}
