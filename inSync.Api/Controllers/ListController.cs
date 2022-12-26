using inSync.Api.Domain.Commands.User;
using inSync.Api.Domain.Queries.User;
using inSync.Api.Models.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace inSync.Api.Controllers;

[ApiController]
[Route("api/lists")]
public class ListController : ControllerBase
{
    private readonly IMediator _mediator;
    public ListController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetListsForUser(string username, CancellationToken token)
    {
        return Ok(await _mediator.Send(new GetUserLists(username), token));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetList(Guid id, [FromQuery] string password, CancellationToken token)
    {
        return Ok(await _mediator.Send(new GetListById(id, password), token));
    }

    [HttpPost]
    public async Task<IActionResult> CreateList(ItemListRequest request, CancellationToken token)
    {
        return Ok(await _mediator.Send(new CreateItemList(request), token));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateList(UpdateListRequest request, CancellationToken token)
    {
        return Ok(await _mediator.Send(new UpdateItemList(request.Id, request.Password, request.ItemList), token));
    }
}