using MediatR;

namespace ETLProject.Contract.DbConnectionContracts.Queries;

public class GetDatabaseTablesQuery : IRequest<ResponseDto>
{
    public Guid ConnectionId { get; set; }
    public string DatabaseName { get; set; }
}