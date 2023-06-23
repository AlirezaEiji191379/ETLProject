using ETLProject.Common.Database;

namespace ETLProject.Common.Abstractions
{
    public interface IQueryCompilerProvider
    {
        SqlKata.Compilers.Compiler GetCompilerFactoryInstance(DataSourceType dataSourceType);
    }
}
