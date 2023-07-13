using ETLProject.Contract.DbConnectionContracts.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ETLProject.Controllers;

[ApiController]
[Route("DbConnection")]
public class ConnectionController : ControllerBase
{
    private readonly IMediator _mediator;

    public ConnectionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateConnection([FromBody] DbConnectionInsertCommand connectionInsertCommand)
    {
        var result = await _mediator.Send(connectionInsertCommand);
        return StatusCode(result.StatusCode, new {Message = result.Message});
    }
    
    
    
}