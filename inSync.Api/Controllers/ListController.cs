using inSync.Api.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace inSync.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ListController : ControllerBase
{
    private readonly IMediator _mediator;
    public ListController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetList(Guid id, [FromQuery] string password, CancellationToken token)
    {
        return Ok(await _mediator.Send(new GetListById(id, password), token));
    }
}