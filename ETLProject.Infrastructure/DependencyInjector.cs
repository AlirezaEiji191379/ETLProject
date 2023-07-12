using ETLProject.Infrastructure.DatabaseContext;
using ETLProject.Infrastructure.Entities;
using ETLProject.Infrastructure.Repositories;
using ETLProject.Infrastructure.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ETLProject.Infrastructure;

public static class DependencyInjector
{
    public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<EtlDbContext>
        (options => 
            options.UseNpgsql(MainDatabaseConnectionStringBuilder.GetPostgresqlConnection()));
        serviceCollection.AddScoped<IDataRepository<EtlConnection>,EtlConnectionRepository>();
    }
}