using ETLProject.Common.Database;

namespace ETLProject.Common.Abstractions
{
    public interface IQueryCompilerProvider
    {
        SqlKata.Compilers.Compiler GetCompiler(DataSourceType dataSourceType);
    }
}
