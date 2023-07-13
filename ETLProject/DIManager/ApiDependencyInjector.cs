using System.Reflection;
using ETLProject.Common.Common.DIManager;
using ETLProject.Contract.DbConnectionContracts.Commands;
using ETLProject.DataSource.Common.DIManager;
using ETLProject.Infrastructure;
using ETLProject.Validators;
using FluentValidation;

namespace ETLProject.DIManager;

public static class ApiDependencyInjector
{
    public static void AddApiServices(this IServiceCollection services)
    {
        services.AddCommonServices();
        services.AddDataSourceQueryServices();
        services.AddInfrastructureServices();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddSingleton<IValidator<DbConnectionInsertCommand>,DbConnectionInsertCommandValidator>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

    }
    
}