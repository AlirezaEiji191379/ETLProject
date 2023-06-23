using ETLProject.Common.Abstractions;
using ETLProject.Common.Database;
using SqlKata.Compilers;


namespace ETLProject.Common.Database.Compiler.Factorties
{
    internal class PostgresqlCompilerFactory : IQueryCompilerFactory
    {
        public DataSourceType DataSourceType => DataSourceType.Postgresql;

        public SqlKata.Compilers.Compiler CreateCompiler()
        {
            return new PostgresCompiler();
        }
    }
}
