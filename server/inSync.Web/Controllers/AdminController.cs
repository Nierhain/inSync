#region

using inSync.Application.Commands.Admin;
using inSync.Application.Queries.Admin;
using inSync.Domain.Models;
using inSync.Web.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace inSync.Web.Controllers;

[ApiController]
[Route("api/admin")]
[Produces("application/json")]
public class AdminController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(Response<bool>), 200)]
    public async Task<IActionResult> GetIsAdmin([FromHeader] string adminkey, CancellationToken token)
    {
        return Ok(await _mediator.Send(new VerifyAdminKey(adminkey), token));
    }

    [HttpGet("lists")]
    [ProducesResponseType(typeof(Response<List<ItemListOverview>>), 200)]
    public async Task<IActionResult> GetAllLists([FromHeader] string adminkey, CancellationToken token)
    {
        return Ok(await _mediator.Send(new GetLists(adminkey), token));
    }

    [HttpGet("lists/{id:guid}")]
    [ProducesResponseType(typeof(Response<ItemListDto>), 200)]
    public async Task<IActionResult> GetList(Guid id, [FromHeader] string adminkey, CancellationToken token)
    {
        return Ok(await _mediator.Send(new GetListById(id, adminkey), token));
    }

    [HttpGet("lists/user/{username}")]
    [ProducesResponseType(typeof(Response<List<ItemListOverview>>), 200)]
    public async Task<IActionResult> GetListsForUser(string username, [FromHeader] string adminkey,
        CancellationToken token)
    {
        return Ok(await _mediator.Send(new GetListsForUser(username, adminkey), token));
    }

    [HttpPut("lists/lock")]
    [ProducesResponseType(typeof(Response<bool>), 200)]
    public async Task<IActionResult> LockList(ListLock request, [FromHeader] string adminkey, CancellationToken token)
    {
        return Ok(await _mediator.Send(new LockList(request.Id, adminkey, request.Reason, request.IsLocked),
            token));
    }
}

public class ListLock
{
    public Guid Id { get; set; }
    public string Reason { get; set; } = string.Empty;
    public bool IsLocked { get; set; }
}