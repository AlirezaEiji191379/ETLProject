using ETLProject.Contract;
using ETLProject.Contract.DbConnectionContracts.Queries;
using ETLProject.Infrastructure.Entities;
using ETLProject.Infrastructure.Repositories.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ETLProject.Handlers.Connection;

public class GetDatabaseConnectionsQueryHandler : IRequestHandler<GetDatabaseConnectionsQuery,ResponseDto>
{
    private readonly IDataRepository<EtlConnection> _etlConnectionRepository;

    public GetDatabaseConnectionsQueryHandler(IDataRepository<EtlConnection> etlConnectionRepository)
    {
        _etlConnectionRepository = etlConnectionRepository;
    }

    public async Task<ResponseDto> Handle(GetDatabaseConnectionsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _etlConnectionRepository.GetAll().ToListAsync(cancellationToken);
            return new ResponseDto()
            {
                Message = result.Select(x => new
                {
                    Host = x.Host,
                    Port = x.Port,
                    ConnectionName = x.ConnectionName,
                    Id = x.Id,
                    DataSourceType = x.DataSourceType,
                    Username = x.Username
                }),
                StatusCode = 200
            };
        }
        catch (Exception exception)
        {
            return new ResponseDto()
            {
                StatusCode = 400,
                Message = exception.Message
            };
        }
    }
}