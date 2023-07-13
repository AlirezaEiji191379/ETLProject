using ETLProject.Contract;
using ETLProject.Contract.DbConnectionContracts.Commands;
using ETLProject.Infrastructure.Entities;
using ETLProject.Infrastructure.Repositories.Abstractions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ETLProject.Handlers.Connection;

public class DeleteConnectionCommandHandler : IRequestHandler<DeleteConnectionCommand,ResponseDto>
{
    private readonly IValidator<DeleteConnectionCommand> _validator;
    private readonly IDataRepository<EtlConnection> _etlConnectionRepository;

    public DeleteConnectionCommandHandler(IValidator<DeleteConnectionCommand> validator, IDataRepository<EtlConnection> etlConnectionRepository)
    {
        _validator = validator;
        _etlConnectionRepository = etlConnectionRepository;
    }

    public async Task<ResponseDto> Handle(DeleteConnectionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _validator.ValidateAndThrowAsync(request,cancellationToken);
            var result = new EtlConnection()
            {
                Id = request.ConnectionId
            };
            _etlConnectionRepository.Delete(result);
            await _etlConnectionRepository.SaveChangesAsync();
            return new ResponseDto()
            {
                Message = "deleted successfully",
                StatusCode = 200
            };
        }
        catch (Exception exception)
        {
            return new ResponseDto()
            {
                StatusCode = 404,
                Message = "connection not found"
            };
        }
    }
}