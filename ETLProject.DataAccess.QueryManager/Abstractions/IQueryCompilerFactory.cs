using ETLProject.Common.Database;

namespace ETLProject.DataSource.Query.Abstractions
{
    internal interface IQueryCompilerFactory
    {
        DataSourceType DataSourceType { get; }
        SqlKata.Compilers.Compiler CreateCompiler();
    }
}
