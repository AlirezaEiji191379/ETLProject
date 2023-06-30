using ETLProject.Common.Database;
using ETLProject.DataSource.Abstractions;
using ETLProject.DataSource.Common.Assembly;

namespace ETLProject.DataSource.Common.Providers.ColumnMapper
{
    internal class ColumnMapperProvider : IColumnMapperProvider
    {
        private readonly Dictionary<DataSourceType, IColumnTypeMapper> _columnMapperByDataSourceType;

        public ColumnMapperProvider()
        {
            _columnMapperByDataSourceType = InitDictionary();
        }

        private static Dictionary<DataSourceType, IColumnTypeMapper>? InitDictionary()
        {
            var result = new Dictionary<DataSourceType, IColumnTypeMapper>();
            var columnTypeMappers = typeof(IAssemblyMarker)
                                    .Assembly
                                    .DefinedTypes
                                    .Where(type => !type.IsAbstract && !type.IsInterface && type.IsAssignableTo(typeof(IColumnTypeMapper)))
                                    .Select(Activator.CreateInstance)
                                    .Cast<IColumnTypeMapper>();
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
