using ETLProject.Common.Database;
using ETLProject.DataSource.Query.Abstractions;
using ETLProject.DataSource.QueryManager.Common.Assembly;

namespace ETLProject.DataSource.QueryManager.Common.Providers.Compiler
{
    internal class CompilerFactoryProvider : IQueryCompilerProvider
    {

        private readonly Dictionary<DataSourceType, IQueryCompilerFactory> _compilerFactoryByDataSourceType;

        public CompilerFactoryProvider()
        {
            _compilerFactoryByDataSourceType = InitDictionary();
        }

        private Dictionary<DataSourceType, IQueryCompilerFactory> InitDictionary()
        {
            var result = new Dictionary<DataSourceType, IQueryCompilerFactory>();

            var factories = typeof(IAssemblyMarker)
                .Assembly
                .DefinedTypes
                .Where(type => !type.IsAbstract && !type.IsInterface && type.IsAssignableTo(typeof(IQueryCompilerFactory)))
                .Select(Activator.CreateInstance)
                .Cast<IQueryCompilerFactory>();

            foreach (var factory in factories)
            {
                _compilerFactoryByDataSourceType[factory.DataSourceType] = factory;
            }
            return result;
        }

        public SqlKata.Compilers.Compiler GetCompiler(DataSourceType dataSourceType)
        {
            return _compilerFactoryByDataSourceType[dataSourceType].CreateCompiler();
        }
    }
}
