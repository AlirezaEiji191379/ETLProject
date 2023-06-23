using ETLProject.Common.Abstractions;
using ETLProject.Common.Database;
using SqlKata.Compilers;


namespace ETLProject.Common.Compiler.Factorties
{
    internal class PostgresqlCompilerFactory : ICompilerFactory
    {
        public DataSourceType DataSourceType => DataSourceType.Postgresql;

        public SqlKata.Compilers.Compiler CreateCompiler()
        {
            return new PostgresCompiler();
        }
    }
}
