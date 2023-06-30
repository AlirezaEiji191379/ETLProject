using ETLProject.Common.Database;
using SqlKata.Compilers;

namespace ETLProject.DataSource.Abstractions
{
    internal interface IQueryCompilerProvider
    {
        Compiler GetCompiler(DataSourceType dataSourceType);
    }
}
