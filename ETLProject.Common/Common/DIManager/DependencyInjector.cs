using ETLProject.Common.Abstractions;
using ETLProject.Common.Database.DBConnection.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace ETLProject.Common.Common.DIManager
{
    public static class DependencyInjector
    {
        public static void AddCommonServices(this IServiceCollection services)
        {
            services.AddSingleton<IRandomStringGenerator,RandomStringGenerator>();

            services.AddSingleton<IConnectionStringFactory,SqlServerConnectionStringFactory>();
            services.AddSingleton<IConnectionStringFactory,MySqlConnectionStringFactory>();
            services.AddSingleton<IConnectionStringFactory,PostgresqlConnectionStringFactory>();

            services.AddSingleton<IConnectionStringProvider,ConnectionStringProvider>();

        }

    }
}
