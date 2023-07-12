using ETLProject.Contract.Limit;
using ETLProject.Contract.Sort;
using ETLProject.DataSource.Abstractions;
using ETLProject.DataSource.ColumnMapping;
using ETLProject.DataSource.Common.Providers;
using ETLProject.DataSource.Common.Providers.BulkCopy;
using ETLProject.DataSource.Common.Providers.ColumnMapper;
using ETLProject.DataSource.Common.Providers.DbConnection;
using ETLProject.DataSource.Common.Utilities;
using ETLProject.DataSource.DataSourceInserting;
using ETLProject.DataSource.DataSourceReading;
using ETLProject.DataSource.DbTransfer;
using ETLProject.DataSource.QueryBusiness.DistinctBusiness;
using ETLProject.DataSource.QueryBusiness.DistinctBusiness.Abstractions;
using ETLProject.DataSource.QueryBusiness.LimitBusiness;
using ETLProject.DataSource.QueryBusiness.LimitBusiness.Abstractions;
using ETLProject.DataSource.QueryBusiness.LimitBusiness.Validators;
using ETLProject.DataSource.QueryBusiness.SortBusiness;
using ETLProject.DataSource.QueryBusiness.SortBusiness.Abstractions;
using ETLProject.DataSource.QueryBusiness.SortBusiness.Validator;
using ETLProject.DataSource.TableFactory;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SqlKata;

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
            services.AddSingleton<IColumnTypeMapper, SqlServerColumnMapper>();
            services.AddSingleton<IColumnTypeMapper, MySqlColumnMapper>();
            services.AddSingleton<IColumnTypeMapper, PostgresqlColumnMapper>();
            services.AddSingleton<IColumnMapperProvider, ColumnMapperProvider>();
            services.AddSingleton<IDataBulkInserter, SqlServerBulkCopy>();
            services.AddSingleton<IDataBulkInserter, MySqlBulkCopy>();
            services.AddSingleton<IDataBulkInserter, PostgresqlBulkCopy>();
            services.AddSingleton<IDataBulkCopyProvider, DataBulkCopyProvider>();
            services.AddSingleton<IDbTableFactory, DbTableFactory>();
            services.AddSingleton<IDataTransfer, DataTransfer>();
            services.AddSingleton<ILimitQueryBusiness, LimitQueryBusiness>();
            services.AddSingleton<ITableSorter, TableSorter>();
            services.AddSingleton<IValidator<LimitContract>, LimitContractValidator>();
            services.AddSingleton<IValidator<SortContract>, SortContractValidator>();
            services.AddSingleton<IDistinctQueryBusiness, DistinctQueryBusiness>();
            services.AddKataServices();
        }
    }
}