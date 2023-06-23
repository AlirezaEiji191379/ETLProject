using ETLProject.Common.Abstractions;
using ETLProject.Common.Database;
using SqlKata.Compilers;

namespace ETLProject.Common.Compiler.Factorties
{
    internal class MySqlCompilerFactory : ICompilerFactory
    {
        public DataSourceType DataSourceType => DataSourceType.MySql;

        public SqlKata.Compilers.Compiler CreateCompiler()
        {
            return new MySqlCompiler();
        }
    }
}
