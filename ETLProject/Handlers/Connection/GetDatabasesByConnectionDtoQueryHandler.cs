using ETLProject.Contract;
using ETLProject.Contract.DbConnectionContracts;
using ETLProject.Contract.DbConnectionContracts.Queries;
using ETLProject.DataSource.Abstractions;
using FluentValidation;
using MediatR;

namespace ETLProject.Handlers.Connection;

public class GetDatabasesByConnectionDtoQueryHandler : IRequestHandler<GetDatabasesByConnectionDtoQuery, ResponseDto>
{
    private readonly IValidator<GetDatabasesByConnectionDtoQuery> _validator;
    private readonly IDbConnectionMetaDataBusinessProvider _connectionMetaDataBusinessProvider;


    public GetDatabasesByConnectionDtoQueryHandler(IValidator<GetDatabasesByConnectionDtoQuery> validator,
        IDbConnectionMetaDataBusinessProvider connectionMetaDataBusinessProvider)
    {
        _validator = validator;
        _connectionMetaDataBusinessProvider = connectionMetaDataBusinessProvider;
    }

    public async Task<ResponseDto> Handle(GetDatabasesByConnectionDtoQuery request, CancellationToken cancellationToken)
    {
        try
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            var connectionDto = new ConnectionDto()
            {
                DataSourceType = request.ConnectionDto.DataSourceType,
                Host = request.ConnectionDto.Host,
                Password = request.ConnectionDto.Password,
                Port = request.ConnectionDto.Port,
                Username = request.ConnectionDto.Username,
            };
            var result =  await _connectionMetaDataBusinessProvider.GetMetaDataBusiness(request.ConnectionDto.DataSourceType)
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
                Message = $"the connection can not be initiailized!",
                StatusCode = 404
            };
        }
    }
}