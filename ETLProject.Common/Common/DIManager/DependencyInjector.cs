using ETLProject.Common.Abstractions;
using ETLProject.Common.Database.Compiler;
using ETLProject.Common.Database.DBConnection.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace ETLProject.Common.Common.DIManager
{
    public static class DependencyInjector
    {
        public static void AddCommonServices(this IServiceCollection services)
        {
            services.AddSingleton<IRandomStringGenerator,RandomStringGenerator>();

            services.AddSingleton<IDbConnectionFactory,SqlServerConnectionFactory>();
            services.AddSingleton<IDbConnectionFactory,MySqlConnectionFactory>();
            services.AddSingleton<IDbConnectionFactory,PostgresqlConnectionFactory>();

            services.AddSingleton<IDbConnectionProvider,DbConnectionProvider>();
            services.AddSingleton<IQueryCompilerProvider, CompilerFactoryProvider>();

        }

    }
}
