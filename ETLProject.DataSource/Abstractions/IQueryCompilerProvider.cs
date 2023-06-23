using ETLProject.Common.Database;

namespace ETLProject.DataSource.Abstractions
{
    internal interface IQueryCompilerProvider
    {
        SqlKata.Compilers.Compiler GetCompiler(DataSourceType dataSourceType);
    }
}
