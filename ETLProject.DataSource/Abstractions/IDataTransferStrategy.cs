using ETLProject.Common.Table;
using ETLProject.Contract.DbWriter;
using ETLProject.DataSource.Common;
using ETLProject.DataSource.DbTransfer;
using ETLProject.DataSource.DbTransfer.Configs;

namespace ETLProject.DataSource.Abstractions
{
    internal interface IDataTransferStrategy
    {
        DataTransferType DataTransferType { get; }
        DataTransferAction DataTransferAction { get; }
        Task TransferData(DataTransferParameter dataTransferParameter);
    }
}
