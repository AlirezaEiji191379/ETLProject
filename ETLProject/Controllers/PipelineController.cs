using ETLProject.Contract.Pipeline;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ETLProject.Controllers;

[ApiController]
[Route("Pipeline")]
public class PipelineController : ControllerBase
{
    private readonly IMediator _mediator;

    public PipelineController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("Execute")]
    public async Task<IActionResult> ExecutePipeline([FromBody] GraphDto graphDto)
    {
        var result = await _mediator.Send(graphDto);
        return StatusCode(result.StatusCode,result.Message);
    }
    
}