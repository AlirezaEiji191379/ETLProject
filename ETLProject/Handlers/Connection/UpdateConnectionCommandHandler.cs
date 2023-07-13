using ETLProject.Contract;
using ETLProject.Contract.DbConnectionContracts.Commands;
using ETLProject.Infrastructure.Entities;
using ETLProject.Infrastructure.Repositories.Abstractions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ETLProject.Handlers.Connection;

public class UpdateConnectionCommandHandler : IRequestHandler<UpdateConnectionCommand,ResponseDto>
{
    private readonly IValidator<UpdateConnectionCommand> _validator;
    private readonly IDataRepository<EtlConnection> _etlConnectionRepository;

    public UpdateConnectionCommandHandler(IValidator<UpdateConnectionCommand> validator, IDataRepository<EtlConnection> etlConnectionRepository)
    {
        _validator = validator;
        _etlConnectionRepository = etlConnectionRepository;
    }

    public async Task<ResponseDto> Handle(UpdateConnectionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _validator.ValidateAndThrowAsync(request,cancellationToken);
            var etlConnection = new EtlConnection()
            {
                Id = request.Id
            };

            if (request.ConnectionName != null)
                etlConnection.ConnectionName = request.ConnectionName;
            
            if (request.Host != null)
                etlConnection.Host = request.Host;
            
            if (request.Port != null)
                etlConnection.Port = request.Port;
            
            if (request.Username != null)
                etlConnection.Username = request.Username;
            
            if (request.Password != null)
                etlConnection.Password = request.Password;
            
            _etlConnectionRepository.Attach(etlConnection);
            await _etlConnectionRepository.SaveChangesAsync();

            return new ResponseDto()
            {
                StatusCode = 200,
                Message = "updated successfully!"
            };
        }
        catch (Exception e)
        {
            return new ResponseDto()
            {
                StatusCode = 400,
                Message = e.InnerException?.Message
            };
        }
    }
}