using MediatR;

namespace ETLProject.Contract.DbConnectionContracts.Commands;

public class TestConnectionCommand : IRequest<ResponseDto>
{
    public ConnectionDto ConnectionDto { get; set; }
}