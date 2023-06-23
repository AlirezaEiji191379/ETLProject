using ETLProject.Common.Database;
using ETLProject.DataSource.Query.Abstractions;
using SqlKata.Compilers;

namespace ETLProject.DataSource.QueryManager.Common.Providers.Compiler.Factorties
{
    internal class SqlServerCompilerFactory : IQueryCompilerFactory
    {
        public DataSourceType DataSourceType => DataSourceType.SQLServer;

        public SqlKata.Compilers.Compiler CreateCompiler()
        {
            return new SqlServerCompiler();
        }
    }
}
