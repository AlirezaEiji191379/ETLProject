using MediatR;

namespace ETLProject.Contract.DbConnectionContracts.Commands;

public class DbConnectionInsertCommand : IRequest<ResponseDto>
{
    public ConnectionDto ConnectionDto { get; set; }
}