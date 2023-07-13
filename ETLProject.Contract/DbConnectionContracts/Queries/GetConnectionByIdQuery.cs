using MediatR;

namespace ETLProject.Contract.DbConnectionContracts.Queries;

public class GetConnectionByIdQuery : IRequest<ResponseDto>
{
    public Guid ConnectionId { get; set; }
}