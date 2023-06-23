using ETLProject.Common.Database;
using ETLProject.DataSource.Abstractions;
using SqlKata.Compilers;


namespace ETLProject.DataSource.Common.Providers.Compiler.Factorties
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
