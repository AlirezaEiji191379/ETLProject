using ETLProject.Common.Abstractions;
using ETLProject.Common.Table;
using ETLProject.DataSource.Query.Abstractions;
using System.Data;

namespace ETLProject.DataSource.Query.DataSourceInserting
{
    internal class DataBaseBulkReader : IDataBaseBulkReader
    {
        private readonly IQueryCompilerProvider _compilerFactoryProvider;
        private readonly IDbConnectionProvider _connectionStringProvider;


        public DataBaseBulkReader(IQueryCompilerProvider compilerFactoryProvider, IDbConnectionProvider connectionStringProvider)
        {
            _compilerFactoryProvider = compilerFactoryProvider;
            _connectionStringProvider = connectionStringProvider;
        }

        public DataTable ReadDataInBulk(ETLTable etlTable)
        {

            throw new NotImplementedException();
        }
    }
}
