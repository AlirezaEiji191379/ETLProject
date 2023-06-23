using ETLProject.Common.Database;
using ETLProject.DataSource.Abstractions;
using SqlKata.Compilers;

namespace ETLProject.DataSource.Common.Providers.Compiler.Factorties
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
