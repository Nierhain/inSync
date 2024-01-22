#region

using inSync.Application.Commands.User;
using inSync.Application.Queries.User;
using inSync.Domain.Shared;
using inSync.Domain.Users;
using inSync.Web.Dtos;
using inSync.Web.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace inSync.Web.Controllers;

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
    [ProducesResponseType(typeof(Result<List<ItemListOverview>>), 200)]
    public async Task<IActionResult> GetListsForUser([FromQuery]string username, CancellationToken token)
    {
        return Ok(await _mediator.Send(new GetUserLists(username), token));
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Result<ItemListDto>), 200)]
    public async Task<IActionResult> GetList(Guid id, [FromHeader] string password, [FromHeader] string username ,CancellationToken token)
    {
        return Ok(await _mediator.Send(new GetListById(id, new Credentials {Username = username, Password = password}), token));
    }

    [HttpPost]
    [ProducesResponseType(typeof(Result<Guid>), 200)]
    public async Task<IActionResult> CreateList(ItemListRequest request, [FromHeader] string password, CancellationToken token)
    {
        return Ok(await _mediator.Send(new CreateItemList(request, password), token));
    }

    [HttpPut]
    [ProducesResponseType(typeof(Result<bool>), 200)]
    public async Task<IActionResult> UpdateList(UpdateListRequest request, [FromHeader] string password, CancellationToken token)
    {
        return Ok(await _mediator.Send(new UpdateItemList(request, password), token));
    }
}