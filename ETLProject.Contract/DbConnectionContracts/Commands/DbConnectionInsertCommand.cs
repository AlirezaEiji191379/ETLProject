using MediatR;

namespace ETLProject.Contract.DbConnectionContracts.Commands;

public class DbConnectionInsertCommand : IRequest
{
    public ConnectionDto ConnectionDto { get; set; }
}