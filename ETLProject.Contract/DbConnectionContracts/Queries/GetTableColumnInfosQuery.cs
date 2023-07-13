using MediatR;

namespace ETLProject.Contract.DbConnectionContracts.Queries;

public class GetTableColumnInfosQuery : IRequest<ResponseDto>
{
    public Guid ConnectionId { get; set; }
    public string DatabaseName { get; set; }
    public string TableName { get; set; }
}