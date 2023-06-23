using ETLProject.Common.Database;

namespace ETLProject.DataSource.Query.Abstractions
{
    public interface IQueryCompilerProvider
    {
        SqlKata.Compilers.Compiler GetCompiler(DataSourceType dataSourceType);
    }
}
