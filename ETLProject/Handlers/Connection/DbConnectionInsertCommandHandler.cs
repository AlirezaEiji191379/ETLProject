using ETLProject.Contract;
using ETLProject.Contract.DbConnectionContracts.Commands;
using ETLProject.Infrastructure.Entities;
using ETLProject.Infrastructure.Repositories.Abstractions;
using FluentValidation;
using MediatR;

namespace ETLProject.Handlers.Connection;

public class DbConnectionInsertCommandHandler : IRequestHandler<DbConnectionInsertCommand,ResponseDto>
{
    private readonly IDataRepository<EtlConnection> _etlConnectionRepository;
    private readonly IValidator<DbConnectionInsertCommand> _validator;

    public DbConnectionInsertCommandHandler(IValidator<DbConnectionInsertCommand> validator,
        IDataRepository<EtlConnection> etlConnectionRepository)
    {
        _validator = validator;
        _etlConnectionRepository = etlConnectionRepository;
    }

    public async Task<ResponseDto> Handle(DbConnectionInsertCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            var etlConnection = new EtlConnection()
            {
                Host = request.ConnectionDto.Host,
                Password = request.ConnectionDto.Password,
                Username = request.ConnectionDto.Username,
                Port = request.ConnectionDto.Port,
                ConnectionName = request.ConnectionDto.ConnectionName,
                DataSourceType = request.ConnectionDto.DataSourceType,
                DatabaseName = request.ConnectionDto.DatabaseName,
                Id = Guid.NewGuid()
            };
            await _etlConnectionRepository.Create(etlConnection);
            await _etlConnectionRepository.SaveChangesAsync();
            return new ResponseDto()
            {
                StatusCode = 200,
                Message = etlConnection.Id.ToString()
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