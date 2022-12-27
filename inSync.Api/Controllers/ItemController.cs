using inSync.Api.Domain.Commands.Admin;
using inSync.Api.Domain.Queries;
using inSync.Api.Models.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace inSync.Api.Controllers;

[ApiController]
[Route("api/items")]
[Produces("application/json")]
public class ItemController : ControllerBase
{
    private readonly IMediator _mediator;

    public ItemController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetMinecraftItems(CancellationToken token)
    {
        return Ok(await _mediator.Send(new GetItemNames(), token));
    }

    [HttpPost]
    public async Task<IActionResult> UpdateMinecraftItems(MinecraftItemUpdate request, CancellationToken token)
    {
        return Ok(await _mediator.Send( new UpdateMinecraftItems(request), token));
    }
}

public class MinecraftItemUpdate
{
    public List<MinecraftItemDto> Items { get; set; } = new();
    public string AdminKey { get; set; } = string.Empty;
}