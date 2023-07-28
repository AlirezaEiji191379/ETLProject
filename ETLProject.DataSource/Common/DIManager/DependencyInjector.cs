using ETLProject.Contract.Limit;
using ETLProject.Contract.Sort;
using ETLProject.DataSource.Abstractions;
using ETLProject.DataSource.ColumnMapping;
using ETLProject.DataSource.Common.Adapters;
using ETLProject.DataSource.Common.Providers;
using ETLProject.DataSource.Common.Providers.BulkCopy;
using ETLProject.DataSource.Common.Providers.ColumnMapper;
using ETLProject.DataSource.Common.Providers.DbConnection;
using ETLProject.DataSource.Common.Providers.DbConnectionMetaDataProvider;
using ETLProject.DataSource.Common.Utilities;
using ETLProject.DataSource.DataSourceInserting;
using ETLProject.DataSource.DataSourceReading;
using ETLProject.DataSource.DbConnectionMetaDataBusiness;
using ETLProject.DataSource.DbConnectionMetaDataBusiness.Abstractions;
using ETLProject.DataSource.DbTransfer;
using ETLProject.DataSource.DbTransfer.Strategies;
using ETLProject.DataSource.QueryBusiness.DbAddBusiness;
using ETLProject.DataSource.QueryBusiness.DbAddBusiness.Abstractions;
using ETLProject.DataSource.QueryBusiness.DbReaderBusiness;
using ETLProject.DataSource.QueryBusiness.DbReaderBusiness.Abstractions;
using ETLProject.DataSource.QueryBusiness.DistinctBusiness;
using ETLProject.DataSource.QueryBusiness.DistinctBusiness.Abstractions;
using ETLProject.DataSource.QueryBusiness.LimitBusiness;
using ETLProject.DataSource.QueryBusiness.LimitBusiness.Abstractions;
using ETLProject.DataSource.QueryBusiness.LimitBusiness.Validators;
using ETLProject.DataSource.QueryBusiness.SortBusiness;
using ETLProject.DataSource.QueryBusiness.SortBusiness.Abstractions;
using ETLProject.DataSource.QueryBusiness.SortBusiness.Validator;
using ETLProject.DataSource.QueryBusiness.WhereQueryBusiness;
using ETLProject.DataSource.QueryBusiness.WhereQueryBusiness.Abstractions;
using ETLProject.DataSource.TableFactory;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SqlKata;
using IConditionBuilder = ETLProject.DataSource.QueryBusiness.WhereQueryBusiness.Abstractions.IConditionBuilder;

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
            services.AddSingleton<ILimitQueryBusiness, LimitQueryBusiness>();
            services.AddSingleton<ITableSorter, TableSorter>();
            services.AddSingleton<IValidator<LimitContract>, LimitContractValidator>();
            services.AddSingleton<IValidator<SortContract>, SortContractValidator>();
            services.AddSingleton<IDistinctQueryBusiness, DistinctQueryBusiness>();
            services.AddSingleton<IDbTableReader, DbTableReader>();
            services.AddSingleton<IEtlColumnTypeMapper, EtlColumnTypeMapper>();
            
            services.AddSingleton<IDataTransferStrategy,AmongOneServerInsert>();
            services.AddSingleton<IDataTransferStrategy,AmongOneServerCreateInsert>();
            services.AddSingleton<IDataTransferStrategy,BetweenTwoServerInsert>();
            services.AddSingleton<IDataTransferStrategy,BetweenTwoServersCreateInsert>();
            services.AddSingleton<IDataTransferStrategyProvider,DataTransferStrategyProvider>();
            
            services.AddSingleton<IDbConnectionMetaDataBusiness,SqlServerDbConnectionMetaDataBusiness>();
            services.AddSingleton<IDbConnectionMetaDataBusiness,MySqlDbConnectionMetaDataBusiness>();
            services.AddSingleton<IDbConnectionMetaDataBusiness,PostgresqlConnectionMetaDataBusiness>();
            services.AddSingleton<IDbConnectionMetaDataBusinessProvider,DbConnectionMetaDataBusinessProvider>();
            services.AddSingleton<IDatabaseConnectionParameterAdapter,DatabaseConnectionParameterAdapter>();

            services.AddSingleton<IConditionBuilder, ConditionBuilder>();
            services.AddSingleton<IWhereQueryBusiness, WhereQueryBusiness>();
            
            services.AddScoped<IDbAddBusiness,DbAddBusiness>();
            
            services.AddKataServices();
        }
    }
}