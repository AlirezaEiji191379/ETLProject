using ETLProject.Common.Abstractions;
using ETLProject.Common.Database;
using SqlKata.Compilers;

namespace ETLProject.Common.Compiler.Factorties
{
    internal class SqlServerCompilerFactory : ICompilerFactory
    {
        public DataSourceType DataSourceType => DataSourceType.SQLServer;

        public SqlKata.Compilers.Compiler CreateCompiler()
        {
            return new SqlServerCompiler();
        }
    }
}
