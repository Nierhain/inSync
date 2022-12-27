#region

using inSync.Api.Domain.Commands.User;
using inSync.Api.Domain.Queries.User;
using inSync.Api.Models.Dtos;
using inSync.Api.Utils;
using inSync.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace inSync.Api.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/lists")]
public class ListController : ControllerBase
{
    private readonly IMediator _mediator;

    public ListController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(Response<List<ItemListOverview>>), 200)]
    public async Task<IActionResult> GetListsForUser(string username, CancellationToken token)
    {
        return Ok(await _mediator.Send(new GetUserLists(username), token));
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Response<ItemList>), 200)]
    public async Task<IActionResult> GetList(Guid id, [FromQuery] string password, CancellationToken token)
    {
        return Ok(await _mediator.Send(new GetListById(id, password), token));
    }

    [HttpPost]
    [ProducesResponseType(typeof(Response<Guid>), 200)]
    public async Task<IActionResult> CreateList(ItemListRequest request, CancellationToken token)
    {
        return Ok(await _mediator.Send(new CreateItemList(request), token));
    }

    [HttpPut]
    [ProducesResponseType(typeof(Response<bool>), 200)]
    public async Task<IActionResult> UpdateList(UpdateListRequest request, CancellationToken token)
    {
        return Ok(await _mediator.Send(new UpdateItemList(request), token));
    }
}