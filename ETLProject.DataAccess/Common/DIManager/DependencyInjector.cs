using ETLProject.DataSource.Query.Abstractions;
using ETLProject.DataSource.QueryManager.Common.Providers;
using ETLProject.DataSource.QueryManager.Common.Providers.Compiler;
using ETLProject.DataSource.QueryManager.Common.Providers.DbConnection;
using ETLProject.DataSource.QueryManager.Common.Utilities;
using ETLProject.DataSource.QueryManager.DataSourceReading;
using Microsoft.Extensions.DependencyInjection;

namespace ETLProject.DataSource.QueryManager.Common.DIManager
{
    public static class DependencyInjector
    {
        public static void AddDataSourceQueryServices(this IServiceCollection services)
        {

            services.AddTransient<IDataBaseBulkReader, DataBaseBulkReader>();
            services.AddSingleton<IQueryFactoryProvider, QueryFactoryProvider>();

            services.AddSingleton<ITableNameProvider, TableNameProvider>();


            services.AddSingleton<IDbConnectionFactory, SqlServerConnectionFactory>();
            services.AddSingleton<IDbConnectionFactory, MySqlConnectionFactory>();
            services.AddSingleton<IDbConnectionFactory, PostgresqlConnectionFactory>();

            services.AddSingleton<IDbConnectionProvider, DbConnectionProvider>();
            services.AddSingleton<IQueryCompilerProvider, CompilerFactoryProvider>();
        }

    }
}
