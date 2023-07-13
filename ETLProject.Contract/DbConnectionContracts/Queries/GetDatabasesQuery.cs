using MediatR;

namespace ETLProject.Contract.DbConnectionContracts.Queries;

public class GetDatabasesQuery : IRequest
{
    public ConnectionDto ConnectionDto { get; set; }
}