using ETLProject.Common.Database;

namespace ETLProject.Common.Abstractions
{
    internal interface IQueryCompilerFactory
    {
        DataSourceType DataSourceType { get; }
        SqlKata.Compilers.Compiler CreateCompiler();
    }
}
