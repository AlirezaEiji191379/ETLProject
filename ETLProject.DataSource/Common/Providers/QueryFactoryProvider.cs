using ETLProject.Common.Table;
using ETLProject.DataSource.Query.Abstractions;
using SqlKata.Execution;

namespace ETLProject.DataSource.QueryManager.Common.Providers
{
    internal class QueryFactoryProvider : IQueryFactoryProvider
    {
        private readonly IQueryCompilerProvider _compilerFactoryProvider;
        private readonly IDbConnectionProvider _connectionProvider;

        public QueryFactoryProvider(IQueryCompilerProvider compilerFactoryProvider, IDbConnectionProvider connectionProvider)
        {
            _compilerFactoryProvider = compilerFactoryProvider;
            _connectionProvider = connectionProvider;
        }


        public QueryFactory GetQueryFactory(ETLTable etlTable)
        {
            var connection = _connectionProvider.GetConnection(etlTable.DatabaseConnection);
            var compiler = _compilerFactoryProvider.GetCompiler(etlTable.DataSourceType);
            return new QueryFactory(connection, compiler);
        }
    }
}
