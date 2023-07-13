using MediatR;

namespace ETLProject.Contract.DbConnectionContracts.Queries;

public class GetTableColumnInfosQuery : IRequest
{
    public ConnectionDto ConnectionDto { get; set; }
    public string DatabaseName { get; set; }
    public string TableName { get; set; }
}