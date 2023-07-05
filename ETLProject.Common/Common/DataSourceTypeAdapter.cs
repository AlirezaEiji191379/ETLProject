using ETLProject.Common.Abstractions;
using ETLProject.Common.Database;
using SqlKata.Compilers.Enums;

namespace ETLProject.Common.Common
{
    internal class DataSourceTypeAdapter : IDataSourceTypeAdapter
    {
        public DataSource CreateDataSourceFromDataSourceType(DataSourceType dataSourceType)
        {
            switch (dataSourceType)
            {
                case DataSourceType.SQLServer:
                    return DataSource.SqlServer;
                case DataSourceType.Postgresql:
                    return DataSource.Postgresql;
                case DataSourceType.MySql: 
                    return DataSource.MySql;
                default:
                    throw new NotSupportedException();
            };
        }
    }
}
