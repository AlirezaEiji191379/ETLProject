using System.Reflection;
using ETLProject.Common.Common.DIManager;
using ETLProject.Contract.DbConnectionContracts.Commands;
using ETLProject.Contract.DbConnectionContracts.Queries;
using ETLProject.DataSource.Common.DIManager;
using ETLProject.Infrastructure;
using ETLProject.Pipeline.Common;
using ETLProject.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ETLProject.DIManager;

public static class ApiDependencyInjector
{
    public static void AddApiServices(this IServiceCollection services)
    {
        services.AddCommonServices();
        services.AddDataSourceQueryServices();
        services.AddInfrastructureServices();
        services.AddPipelineServices();
        services.AddControllers()
            .AddNewtonsoftJson(opts =>
            {
                opts.SerializerSettings.Converters.Add(new StringEnumConverter());
            });
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddSingleton<IValidator<DbConnectionInsertCommand>,DbConnectionInsertCommandValidator>();
        services.AddSingleton<IValidator<GetDatabasesByConnectionIdQuery>,GetDataBasesQueryValidator>();
        services.AddSingleton<IValidator<GetDatabaseTablesQuery>,GetDataBaseTablesQueryValidator>();
        services.AddSingleton<IValidator<GetTableColumnInfosQuery>,GetTableColumnInfoQueryValidator>();
        services.AddSingleton<IValidator<GetConnectionByIdQuery>,GetConnectionByIdQueryValidator>();
        services.AddSingleton<IValidator<DeleteConnectionCommand>,DeleteConnectionCommandValidator>();
        services.AddSingleton<IValidator<UpdateConnectionCommand>,UpdateConnectionCommandValidator>();
        services.AddSingleton<IValidator<GetDatabasesByConnectionDtoQuery>,GetDatabasesByConnectionDtoQueryValidator>();
        
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

    }
    
}