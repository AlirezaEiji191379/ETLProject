using ETLProject.Common.Database;

namespace ETLProject.Common.Abstractions
{
    public interface ICompilerFactoryProvider
    {
        ICompilerFactory GetCompilerFactoryInstance(DataSourceType dataSourceType);
    }
}
