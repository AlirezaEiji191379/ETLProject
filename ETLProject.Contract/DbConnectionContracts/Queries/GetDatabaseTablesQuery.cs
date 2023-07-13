using MediatR;

namespace ETLProject.Contract.DbConnectionContracts.Queries;

public class GetDatabaseTablesQuery : IRequest<ResponseDto>
{
    public ConnectionDto ConnectionDto { get; set; }
    public string DatabaseName { get; set; }
}