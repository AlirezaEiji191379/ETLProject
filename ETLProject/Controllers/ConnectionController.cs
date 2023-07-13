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

    [HttpGet]
    [Route("Databases")]
    public async Task<IActionResult> GetConnectionDatabases([FromQuery]Guid connectionId)
    {
        var getDatabasesQuery = new GetDatabasesQuery()
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
    
    
}