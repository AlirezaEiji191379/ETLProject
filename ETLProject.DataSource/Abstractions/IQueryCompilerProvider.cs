using ETLProject.Common.Database;

namespace ETLProject.DataSource.Query.Abstractions
{
    internal interface IQueryCompilerProvider
    {
        SqlKata.Compilers.Compiler GetCompiler(DataSourceType dataSourceType);
    }
}
