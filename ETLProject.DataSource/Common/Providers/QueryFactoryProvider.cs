using ETLProject.Common.Abstractions;
using ETLProject.Common.Table;
using ETLProject.DataSource.Abstractions;
using SqlKata.Compilers;
using SqlKata.Compilers.Abstractions;
using SqlKata.Execution;
using System.Data;

namespace ETLProject.DataSource.Common.Providers
{
    internal class QueryFactoryProvider : IQueryFactoryProvider
    {
        private readonly ICompilerProvider _compilerProvider;
        private readonly IDbConnectionProvider _connectionProvider;
        private readonly IDataSourceTypeAdapter _dataSourceTypeAdapter;

        public QueryFactoryProvider(ICompilerProvider compilerProvider,
            IDbConnectionProvider connectionProvider,
            IDataSourceTypeAdapter dataSourceTypeAdapter)
        {
            _compilerProvider = compilerProvider;
            _connectionProvider = connectionProvider;
            _dataSourceTypeAdapter = dataSourceTypeAdapter;
        }


        public QueryFactory GetQueryFactory(ETLTable etlTable)
        {
            if (etlTable.DbConnection == null)
            {
                etlTable.DbConnection = _connectionProvider.GetConnection(etlTable.DatabaseConnection);
            }
            if (etlTable.DbConnection.State != ConnectionState.Open)
                etlTable.DbConnection.Open();

            var compiler = CreateCompiler(etlTable);
            return new QueryFactory(etlTable.DbConnection, compiler);
        }

        private Compiler CreateCompiler(ETLTable etlTable)
        {
            var dataSource = _dataSourceTypeAdapter.CreateDataSourceFromDataSourceType(etlTable.DataSourceType);
            var compiler = _compilerProvider.CreateCompiler(dataSource);
            return compiler;
        }
    }
}
