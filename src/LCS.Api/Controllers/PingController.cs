using LCS.Application.Features.Ping.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LCS.Api.Controllers;

[ApiController]
[Route("api/v1/ping")]
public class PingController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken ct)
    {
        var response = await sender.Send(new PingQuery(), ct);
        return Ok(response);
    }
}
