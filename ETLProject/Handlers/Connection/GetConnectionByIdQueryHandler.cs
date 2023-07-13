using ETLProject.Contract;
using ETLProject.Contract.DbConnectionContracts.Queries;
using ETLProject.Infrastructure.Entities;
using ETLProject.Infrastructure.Repositories.Abstractions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ETLProject.Handlers.Connection;

public class GetConnectionByIdQueryHandler : IRequestHandler<GetConnectionByIdQuery,ResponseDto>
{
    private readonly IValidator<GetConnectionByIdQuery> _validator;
    private readonly IDataRepository<EtlConnection> _etlConnectionRepository;

    public GetConnectionByIdQueryHandler(IDataRepository<EtlConnection> etlConnectionRepository, IValidator<GetConnectionByIdQuery> validator)
    {
        _etlConnectionRepository = etlConnectionRepository;
        _validator = validator;
    }

    public async Task<ResponseDto> Handle(GetConnectionByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            await _validator.ValidateAndThrowAsync(request,cancellationToken);
            var result = await _etlConnectionRepository.FindByCondition(x => x.Id == request.ConnectionId).FirstOrDefaultAsync(cancellationToken);
            if (result == null)
            {
                return new ResponseDto()
                {
                    StatusCode = 404,
                    Message = "Connection Not found!"
                };
            }
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
                StatusCode = 400,
                Message = exception.Message
            };
        }
    }
}