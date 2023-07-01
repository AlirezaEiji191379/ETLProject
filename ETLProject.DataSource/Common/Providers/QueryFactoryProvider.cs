using ETLProject.Common.Table;
using ETLProject.DataSource.Abstractions;
using SqlKata.Execution;
using System.Data;

namespace ETLProject.DataSource.Common.Providers
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

            if (etlTable.DbConnection == null)
            {
                etlTable.DbConnection = _connectionProvider.GetConnection(etlTable.DatabaseConnection);
            }
            if(etlTable.DbConnection.State != ConnectionState.Open)
                etlTable.DbConnection.Open();
            var compiler = _compilerFactoryProvider.GetCompiler(etlTable.DataSourceType);
            return new QueryFactory(etlTable.DbConnection, compiler);
        }
    }
}
