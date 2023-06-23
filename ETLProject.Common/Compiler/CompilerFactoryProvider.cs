using ETLProject.Common.Abstractions;
using ETLProject.Common.Common.AssemblyMarker;
using ETLProject.Common.Database;

namespace ETLProject.Common.Compiler
{
    internal class CompilerFactoryProvider : ICompilerFactoryProvider
    {

        private readonly Dictionary<DataSourceType, ICompilerFactory> _compilerFactoryByDataSourceType;

        public CompilerFactoryProvider()
        {
            _compilerFactoryByDataSourceType = InitDictionary();
        }

        private Dictionary<DataSourceType, ICompilerFactory> InitDictionary()
        {
            var result = new Dictionary<DataSourceType, ICompilerFactory>();

            var factories = typeof(IAssemblyMarker)
                .Assembly
                .DefinedTypes
                .Where(type => !type.IsAbstract && !type.IsInterface && type.IsAssignableTo(typeof(ICompilerFactory)))
                .Select(Activator.CreateInstance)
                .Cast<ICompilerFactory>();
            
            foreach(var factory in factories)
            {
                _compilerFactoryByDataSourceType[factory.DataSourceType] = factory;
            }
            return result;
        }

        public ICompilerFactory GetCompilerFactoryInstance(DataSourceType dataSourceType)
        {
            return _compilerFactoryByDataSourceType[dataSourceType];
        }
    }
}
