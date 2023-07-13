using ETLProject.Common.Common.DIManager;
using ETLProject.DataSource.Common.DIManager;
using ETLProject.Infrastructure;

namespace ETLProject.DIManager;

public static class ApiDependencyInjector
{
    public static void AddApiServices(this IServiceCollection services)
    {
        // Add services to the container.
        services.AddCommonServices();
        services.AddDataSourceQueryServices();
        services.AddInfrastructureServices();

        services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }
    
}