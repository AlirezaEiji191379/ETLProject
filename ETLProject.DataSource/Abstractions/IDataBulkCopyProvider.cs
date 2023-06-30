using ETLProject.Common.Database;

namespace ETLProject.DataSource.Abstractions
{
    public interface IDataBulkCopyProvider
    {
        IDataBulkInserter GetBulkInserter(DataSourceType dataSourceType);
    }
}
