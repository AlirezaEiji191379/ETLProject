using ETLProject.DataSource.Abstractions;
using ETLProject.DataSource.ColumnMapping;
using ETLProject.DataSource.Common.Providers;
using ETLProject.DataSource.Common.Providers.BulkCopy;
using ETLProject.DataSource.Common.Providers.ColumnMapper;
using ETLProject.DataSource.Common.Providers.Compiler;
using ETLProject.DataSource.Common.Providers.DbConnection;
using ETLProject.DataSource.Common.Utilities;
using ETLProject.DataSource.DataSourceInserting;
using ETLProject.DataSource.DataSourceReading;
using Microsoft.Extensions.DependencyInjection;

namespace ETLProject.DataSource.Common.DIManager
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

            services.AddSingleton<IColumnTypeMapper, SqlServerColumnMapper>();
            services.AddSingleton<IColumnTypeMapper, MySqlColumnMapper>();
            services.AddSingleton<IColumnTypeMapper, PostgresqlColumnMapper>();
            services.AddSingleton<IColumnMapperProvider,ColumnMapperProvider>();

            services.AddSingleton<IDataBulkInserter,SqlServerBulkCopy>();
            services.AddSingleton<IDataBulkInserter,MySqlBulkCopy>();
            services.AddSingleton<IDataBulkInserter,PostgresqlBulkCopy>();
            services.AddSingleton<IDataBulkCopyProvider,DataBulkCopyProvider>();


        }

    }
}
