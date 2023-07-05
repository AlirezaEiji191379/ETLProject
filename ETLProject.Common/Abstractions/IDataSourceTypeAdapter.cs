using ETLProject.Common.Database;
using SqlKata.Compilers.Enums;

namespace ETLProject.Common.Abstractions
{
    public interface IDataSourceTypeAdapter
    {
        DataSource CreateDataSourceFromDataSourceType(DataSourceType dataSourceType);
    }
}
