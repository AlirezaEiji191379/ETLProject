using ETLProject.Common.Database;
using ETLProject.DataSource.Abstractions;
using ETLProject.DataSource.Common.Assembly;

namespace ETLProject.DataSource.Common.Providers.BulkCopy
{
    internal class DataBulkCopyProvider : IDataBulkCopyProvider
    {
        private readonly Dictionary<DataSourceType, IDataBulkInserter> _dataBulkInserterByDataSourceType;

        public DataBulkCopyProvider(IEnumerable<IDataBulkInserter> dataBulkInserters)
        {
            _dataBulkInserterByDataSourceType = InitDictionary(dataBulkInserters);
        }

        private static Dictionary<DataSourceType, IDataBulkInserter>? InitDictionary(IEnumerable<IDataBulkInserter> dataBulkInserters)
        {
            var result = new Dictionary<DataSourceType, IDataBulkInserter>();
            foreach (var type in dataBulkInserters)
                result[type.DataSourceType] = type;
            return result;
        }

        public IDataBulkInserter GetBulkInserter(DataSourceType dataSourceType)
        {
            return _dataBulkInserterByDataSourceType[dataSourceType];
        }
    }
}
