using MediatR;

namespace ETLProject.Contract.DbConnectionContracts.Queries;

public class GetDatabasesByConnectionIdQuery : IRequest<ResponseDto>
{
    public Guid ConnectionId { get; set; }
}