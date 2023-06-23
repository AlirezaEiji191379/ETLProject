﻿using ETLProject.DataSource.Query.Abstractions;
using ETLProject.DataSource.Query.Common.Providers;
using ETLProject.DataSource.Query.Common.Utilities;
using ETLProject.DataSource.Query.DataSourceInserting;
using Microsoft.Extensions.DependencyInjection;

namespace ETLProject.DataSource.Query.Common.DIManager
{
    public static class DependencyInjector
    {
        public static void AddDataSourceQueryServices(this IServiceCollection services)
        {

            services.AddTransient<IDataBaseBulkReader,DataBaseBulkReader>();
            services.AddSingleton<IQueryFactoryProvider,QueryFactoryProvider>();

            services.AddSingleton<ITableNameProvider, TableNameProvider>();

        }

    }
}
