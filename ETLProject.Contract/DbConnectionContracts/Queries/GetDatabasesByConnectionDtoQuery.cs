using MediatR;

namespace ETLProject.Contract.DbConnectionContracts.Queries;

public class GetDatabasesByConnectionDtoQuery : IRequest<ResponseDto>
{
    public ConnectionDto ConnectionDto { get; set; }
}