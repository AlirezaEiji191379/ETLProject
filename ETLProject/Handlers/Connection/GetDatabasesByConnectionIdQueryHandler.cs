using ETLProject.Common.Database;
using ETLProject.Contract;
using ETLProject.Contract.DbConnectionContracts;
using ETLProject.Contract.DbConnectionContracts.Queries;
using ETLProject.DataSource.Abstractions;
using ETLProject.Infrastructure.Entities;
using ETLProject.Infrastructure.Repositories.Abstractions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ETLProject.Handlers.Connection;

public class GetDatabasesByConnectionIdQueryHandler : IRequestHandler<GetDatabasesByConnectionIdQuery, ResponseDto>
{
    private readonly IValidator<GetDatabasesByConnectionIdQuery> _validator;
    private readonly IDataRepository<EtlConnection> _etlConnectionRepository;
    private readonly IDbConnectionMetaDataBusinessProvider _connectionMetaDataBusinessProvider;

    public GetDatabasesByConnectionIdQueryHandler(IValidator<GetDatabasesByConnectionIdQuery> validator,
        IDataRepository<EtlConnection> etlConnectionRepository, IDbConnectionMetaDataBusinessProvider connectionMetaDataBusinessProvider)
    {
        _validator = validator;
        _etlConnectionRepository = etlConnectionRepository;
        _connectionMetaDataBusinessProvider = connectionMetaDataBusinessProvider;
    }

    public async Task<ResponseDto> Handle(GetDatabasesByConnectionIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            var etlConnection = await _etlConnectionRepository.FindByCondition(x => x.Id == request.ConnectionId)
                .FirstAsync(cancellationToken: cancellationToken);
            var connectionDto = new ConnectionDto()
            {
                DataSourceType = etlConnection.DataSourceType,
                Host = etlConnection.Host,
                Password = etlConnection.Password,
                Port = etlConnection.Port,
                Username = etlConnection.Username,
                ConnectionName = etlConnection.ConnectionName
            };
            var result =  await _connectionMetaDataBusinessProvider.GetMetaDataBusiness(etlConnection.DataSourceType)
                .GetDatabases(connectionDto);
            return new ResponseDto()
            {
                Message = result,
                StatusCode = 200
            };
        }
        catch (Exception exception)
        {
            return new ResponseDto()
            {
                Message = $"the connection with id {request.ConnectionId} not found",
                StatusCode = 404
            };
        }
    }
}