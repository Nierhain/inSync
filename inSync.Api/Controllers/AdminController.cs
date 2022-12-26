using inSync.Api.Domain.Commands.Admin;
using inSync.Api.Domain.Queries.Admin;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace inSync.Api.Controllers;

[ApiController]
[Route("api/admin/lists")]
public class AdminController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllLists([FromQuery] string adminKey, CancellationToken token)
    {
        return Ok(await _mediator.Send(new GetLists(adminKey), token));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetList(Guid id, [FromQuery] string adminKey, CancellationToken token)
    {
        return Ok(await _mediator.Send(new GetListById(id, adminKey), token));
    }

    [HttpGet("user/{username}")]
    public async Task<IActionResult> GetListsForUser(string username, [FromQuery] string adminKey, CancellationToken token)
    {
        return Ok(await _mediator.Send(new GetListsForUser(username, adminKey), token));
    }

    [HttpPut("lock")]
    public async Task<IActionResult> LockList(ListLock request, CancellationToken token)
    {
        return Ok(await _mediator.Send(new LockList(request.Id, request.AdminKey, request.Reason, request.IsLocked), token));
    }
}

public class ListLock
{
    public Guid Id { get; set; }
    public string AdminKey { get; set; }
    public string Reason { get; set; }
    public bool IsLocked { get; set; }
}