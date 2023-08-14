using ETLProject.Contract;
using ETLProject.Contract.DbConnectionContracts.Commands;
using ETLProject.DataSource.Abstractions;
using FluentValidation;
using MediatR;

namespace ETLProject.Handlers.Connection;

public class TestConnectionCommandHandler : IRequestHandler<TestConnectionCommand, ResponseDto>
{
    private readonly IValidator<TestConnectionCommand> _validator;
    private readonly IDbConnectionMetaDataBusinessProvider _connectionMetaDataBusinessProvider;

    public TestConnectionCommandHandler(IValidator<TestConnectionCommand> validator,
        IDbConnectionMetaDataBusinessProvider connectionMetaDataBusinessProvider)
    {
        _validator = validator;
        _connectionMetaDataBusinessProvider = connectionMetaDataBusinessProvider;
    }

    public async Task<ResponseDto> Handle(TestConnectionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var metadataBusiness = _connectionMetaDataBusinessProvider.GetMetaDataBusiness(request.ConnectionDto.DataSourceType);
            await metadataBusiness.TestConnection(request.ConnectionDto);
            return new ResponseDto()
            {
                StatusCode = 200,
                Message = "connected!"
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