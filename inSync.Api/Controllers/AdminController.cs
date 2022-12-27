#region

using inSync.Api.Domain.Commands.Admin;
using inSync.Api.Domain.Queries.Admin;
using inSync.Api.Models.Dtos;
using inSync.Api.Utils;
using MediatR;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace inSync.Api.Controllers;

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
    public async Task<IActionResult> GetIsAdmin([FromHeader] string adminKey, CancellationToken token)
    {
        return Ok(await _mediator.Send(new VerifyAdminKey(adminKey), token));
    }

    [HttpGet("lists")]
    [ProducesResponseType(typeof(Response<List<ItemListOverview>>), 200)]
    public async Task<IActionResult> GetAllLists([FromHeader] string adminKey, CancellationToken token)
    {
        return Ok(await _mediator.Send(new GetLists(adminKey), token));
    }

    [HttpGet("lists/{id:guid}")]
    [ProducesResponseType(typeof(Response<ItemListDto>), 200)]
    public async Task<IActionResult> GetList(Guid id, [FromHeader] string adminKey, CancellationToken token)
    {
        return Ok(await _mediator.Send(new GetListById(id, adminKey), token));
    }

    [HttpGet("lists/user/{username}")]
    [ProducesResponseType(typeof(Response<List<ItemListOverview>>), 200)]
    public async Task<IActionResult> GetListsForUser(string username, [FromHeader] string adminKey,
        CancellationToken token)
    {
        return Ok(await _mediator.Send(new GetListsForUser(username, adminKey), token));
    }

    [HttpPut("lists/lock")]
    [ProducesResponseType(typeof(Response<bool>), 200)]
    public async Task<IActionResult> LockList(ListLock request, [FromHeader] string adminKey, CancellationToken token)
    {
        return Ok(await _mediator.Send(new LockList(request.Id, adminKey, request.Reason, request.IsLocked),
            token));
    }
}

public class ListLock
{
    public Guid Id { get; set; }
    public string Reason { get; set; } = string.Empty;
    public bool IsLocked { get; set; }
}