using ETLProject.Common.Database;
using ETLProject.Common.Table;
using ETLProject.Contract.DbWriter;
using ETLProject.DataSource.Abstractions;
using ETLProject.DataSource.Common;

namespace ETLProject.DataSource.DbTransfer
{
    internal class DataTransfer : IDataTransfer
    {
        private readonly IDbTableFactory _dbTableCreator;
        private readonly IDataBulkCopyProvider _dataBulkCopyProvider;
        private readonly IDataBaseBulkReader _dataBulkReader;
        public DataTransfer(IDbTableFactory dbTableCreator, IDataBulkCopyProvider dataBulkCopyProvider, IDataBaseBulkReader dataBulkReader)
        {
            _dbTableCreator = dbTableCreator;
            _dataBulkCopyProvider = dataBulkCopyProvider;
            _dataBulkReader = dataBulkReader;
        }

        public async Task TransferDataBetweenTwoDifferentConnections(ETLTable sourceTable, ETLTable destinationTable, BulkConfiguration bulkConfiguration)
        {
            destinationTable.Columns = sourceTable.CloneEtlColumns();
            var dataInserter = _dataBulkCopyProvider.GetBulkInserter(destinationTable.DataSourceType);
            await _dbTableCreator.CreateTable(destinationTable);
            await foreach (var dt in _dataBulkReader.ReadDataInBulk(sourceTable, new BulkConfiguration() { BatchSize = 2 }))
            {
                await dataInserter.InsertBulk(dt, destinationTable);
            }
        }

        public async Task TransferDataInSingleConnection(ETLTable sourceTable,string newTableName,TableType newTableType = TableType.Temp)
        {
            if (sourceTable.DataSourceType != DataSourceType.SQLServer)
            {
                await _dbTableCreator.CreateTableAs(sourceTable, newTableName, newTableType);
                return;
            }
            await _dbTableCreator.SelectInto(sourceTable,newTableName);
        }

    }
}
