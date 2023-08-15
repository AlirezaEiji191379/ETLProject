using System.Text;
using ETLProject.Contract.Pipeline;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
    public async Task<IActionResult> ExecutePipeline([FromForm] IFormFile file)
    {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        memoryStream.Seek(0, SeekOrigin.Begin);
        using var reader = new StreamReader(memoryStream, Encoding.UTF8);
        var fileContent = await reader.ReadToEndAsync();
        var graphDto = JsonConvert.DeserializeObject<GraphDto>(fileContent);
        var result = await _mediator.Send(graphDto);
        return StatusCode(result.StatusCode, result.Message);
    }
}