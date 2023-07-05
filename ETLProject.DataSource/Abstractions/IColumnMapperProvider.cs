using ETLProject.Common.Database;

namespace ETLProject.DataSource.Abstractions
{
    public interface IColumnMapperProvider
    {
        IColumnTypeMapper GetColumnTypeMapper(DataSourceType dataSourceType);
    }
}
