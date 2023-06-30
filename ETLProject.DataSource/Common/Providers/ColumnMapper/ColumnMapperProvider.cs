using ETLProject.Common.Database;
using ETLProject.DataSource.Abstractions;
using ETLProject.DataSource.Common.Assembly;

namespace ETLProject.DataSource.Common.Providers.ColumnMapper
{
    internal class ColumnMapperProvider : IColumnMapperProvider
    {
        private readonly Dictionary<DataSourceType, IColumnTypeMapper> _columnMapperByDataSourceType;

        public ColumnMapperProvider(IEnumerable<IColumnTypeMapper> columnTypeMappers)
        {
            _columnMapperByDataSourceType = InitDictionary(columnTypeMappers);
        }

        private static Dictionary<DataSourceType, IColumnTypeMapper>? InitDictionary(IEnumerable<IColumnTypeMapper> columnTypeMappers)
        {
            var result = new Dictionary<DataSourceType, IColumnTypeMapper>();
            foreach(var type in columnTypeMappers)
            {
                result[type.DataSourceType] = type;
            }
            return result;
        }

        public IColumnTypeMapper GetColumnTypeMapper(DataSourceType dataSourceType)
        {
            return _columnMapperByDataSourceType[dataSourceType];
        }
    }
}
