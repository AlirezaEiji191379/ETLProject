using MediatR;

namespace ETLProject.Contract.DbConnectionContracts.Commands;

public class UpdateConnectionCommand : IRequest<ResponseDto>
{
    public Guid Id { get; set; }
    public string? ConnectionName { get; set; }
    public string? Host { get; set; }
    public string? Port { get; set; }
    public string? Password { get; set; }
    public string? Username { get; set; }
}