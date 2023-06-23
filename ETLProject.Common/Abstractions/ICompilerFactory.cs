using ETLProject.Common.Database;

namespace ETLProject.Common.Abstractions
{
    public interface ICompilerFactory
    {
        DataSourceType DataSourceType { get; }
        SqlKata.Compilers.Compiler CreateCompiler();
    }
}
