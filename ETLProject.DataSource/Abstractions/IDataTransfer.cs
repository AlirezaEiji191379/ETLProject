using ETLProject.Common.Table;
using ETLProject.DataSource.Common;

namespace ETLProject.DataSource.Abstractions
{
    public interface IDataTransfer
    {
        Task TransferData(ETLTable sourceTable,ETLTable destinationTable,BulkConfiguration bulkConfiguration);
    }
}
