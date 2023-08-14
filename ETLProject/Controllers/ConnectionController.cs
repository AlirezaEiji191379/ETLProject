using ETLProject.Contract.DbConnectionContracts.Commands;
using ETLProject.Contract.DbConnectionContracts.Queries;
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

    [HttpPost]
    [Route("DatabasesByConnectionDto")]
    public async Task<IActionResult> GetConnectionDatabasesbyConnectionDto([FromBody] GetDatabasesByConnectionDtoQuery getDatabasesByConnectionDtoQuery)
    {
        var result = await _mediator.Send(getDatabasesByConnectionDtoQuery);
        return StatusCode(result.StatusCode,new {Message = result.Message});
    }

    [HttpGet]
    [Route("Databases")]
    public async Task<IActionResult> GetConnectionDatabases([FromQuery]Guid connectionId)
    {
        var getDatabasesQuery = new GetDatabasesByConnectionIdQuery()
        {
            ConnectionId = connectionId
        };
        var result = await _mediator.Send(getDatabasesQuery);
        return StatusCode(result.StatusCode,new {Message = result.Message});
    }

    [HttpGet]
    [Route("{databaseName}/Tables")]
    public async Task<IActionResult> GetDatabaseTables([FromQuery] Guid connectionId,string databaseName)
    {
        var getDatabaseTablesQuery = new GetDatabaseTablesQuery()
        {
            ConnectionId = connectionId,
            DatabaseName = databaseName
        };
        var result = await _mediator.Send(getDatabaseTablesQuery);
        return StatusCode(result.StatusCode,new {Message = result.Message});
    }

    [HttpGet]
    [Route("{databaseName}/{tableName}")]
    public async Task<IActionResult> GetTableColumnInfos([FromQuery] Guid connectionId,string databaseName,string tableName)
    {
        var getTableColumnInfosQuery = new GetTableColumnInfosQuery()
        {
            ConnectionId = connectionId,
            DatabaseName = databaseName,
            TableName = tableName
        };
        var result = await _mediator.Send(getTableColumnInfosQuery);
        return StatusCode(result.StatusCode,new {Message = result.Message});
    }

    [HttpGet]
    [Route("Connections")]
    public async Task<IActionResult> GetAllConnections()
    {
        var getAllConnectionsQuery = new GetDatabaseConnectionsQuery() { };
        var result = await _mediator.Send(getAllConnectionsQuery);
        return StatusCode(result.StatusCode,new {Message = result.Message});
    }

    [HttpGet]
    [Route("Connection/{connectionId}")]
    public async Task<IActionResult> GetConnection(Guid connectionId)
    {
        var getConnectionByIdQuery = new GetConnectionByIdQuery()
        {
            ConnectionId = connectionId
        };
        var result = await _mediator.Send(getConnectionByIdQuery);
        return StatusCode(result.StatusCode,new {Message = result.Message});
    }
    
    [HttpDelete]
    [Route("Connection/{connectionId}")]
    public async Task<IActionResult> DeleteConnection(Guid connectionId)
    {
        var deleteConnectionCommand = new DeleteConnectionCommand()
        {
            ConnectionId = connectionId
        };
        var result = await _mediator.Send(deleteConnectionCommand);
        return StatusCode(result.StatusCode,new {Message = result.Message});
    }
    
    [HttpPut]
    [Route("Connection")]
    public async Task<IActionResult> UpdateConnection([FromBody] UpdateConnectionCommand updateConnectionCommand)
    {
        var result = await _mediator.Send(updateConnectionCommand);
        return StatusCode(result.StatusCode,new {Message = result.Message});
    }

    [HttpPost]
    [Route("Test")]
    public async Task<IActionResult> TestConnection([FromBody] TestConnectionCommand testConnectionCommand)
    {
        var result = await _mediator.Send(testConnectionCommand);
        return StatusCode(result.StatusCode,new { Message = result.Message});
    }
    
}