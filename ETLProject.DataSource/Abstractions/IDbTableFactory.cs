using ETLProject.Common.Table;
using System.Data;

namespace ETLProject.DataSource.Abstractions
{
    public interface IDbTableFactory
    {
        Task CreateTempTable(ETLTable etlTable);
    }
}
