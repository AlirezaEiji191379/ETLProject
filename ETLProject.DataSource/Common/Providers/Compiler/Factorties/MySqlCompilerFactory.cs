using ETLProject.Common.Database;
using ETLProject.DataSource.Abstractions;
using SqlKata.Compilers;

namespace ETLProject.DataSource.Common.Providers.Compiler.Factorties
{
    internal class MySqlCompilerFactory : IQueryCompilerFactory
    {
        public DataSourceType DataSourceType => DataSourceType.MySql;

        public SqlKata.Compilers.Compiler CreateCompiler()
        {
            return new MySqlCompiler();
        }
    }
}
