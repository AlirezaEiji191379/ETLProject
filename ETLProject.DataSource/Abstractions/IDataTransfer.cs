using ETLProject.Common.Table;
using ETLProject.Contract.DbWriter;
using ETLProject.DataSource.Common;

namespace ETLProject.DataSource.Abstractions
{
    public interface IDataTransfer
    {
        Task TransferDataBetweenTwoDifferentConnections(ETLTable sourceTable, ETLTable destinationTable, BulkConfiguration bulkConfiguration);
        Task TransferDataInSingleConnection(ETLTable sourceTable,string newTableName,TableType tableType = TableType.Temp);
    }
}
