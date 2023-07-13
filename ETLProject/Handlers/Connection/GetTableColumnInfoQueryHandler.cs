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

public class GetTableColumnInfoQueryHandler : IRequestHandler<GetTableColumnInfosQuery, ResponseDto>
{
    private readonly IValidator<GetTableColumnInfosQuery> _validator;
    private readonly IDataRepository<EtlConnection> _etlConnectionRepository;
    private readonly IDbConnectionMetaDataBusinessProvider _connectionMetaDataBusinessProvider;

    public GetTableColumnInfoQueryHandler(IValidator<GetTableColumnInfosQuery> validator,
        IDataRepository<EtlConnection> etlConnectionRepository,
        IDbConnectionMetaDataBusinessProvider connectionMetaDataBusinessProvider)
    {
        _validator = validator;
        _etlConnectionRepository = etlConnectionRepository;
        _connectionMetaDataBusinessProvider = connectionMetaDataBusinessProvider;
    }

    public async Task<ResponseDto> Handle(GetTableColumnInfosQuery request, CancellationToken cancellationToken)
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
                .GetTableColumns(connectionDto,request.DatabaseName,request.TableName);
            return new ResponseDto()
            {
                Message = result,
                StatusCode = 200
            };
        }
        catch (Exception e)
        {
            return new ResponseDto()
            {
                Message = e.Message,
                StatusCode = 400
            };

        }
    }
}