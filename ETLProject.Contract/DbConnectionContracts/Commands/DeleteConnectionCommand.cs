using MediatR;

namespace ETLProject.Contract.DbConnectionContracts.Commands;

public class DeleteConnectionCommand : IRequest<ResponseDto>
{
    public Guid ConnectionId { get; set; }
}