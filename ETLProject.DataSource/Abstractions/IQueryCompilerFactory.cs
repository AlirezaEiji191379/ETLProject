using ETLProject.Common.Database;

namespace ETLProject.DataSource.Abstractions
{
    internal interface IQueryCompilerFactory
    {
        DataSourceType DataSourceType { get; }
        SqlKata.Compilers.Compiler CreateCompiler();
    }
}
