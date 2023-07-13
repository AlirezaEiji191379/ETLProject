using MediatR;

namespace ETLProject.Contract.DbConnectionContracts.Queries;

public class GetDatabasesQuery : IRequest<ResponseDto>
{
    public ConnectionDto ConnectionDto { get; set; }
}