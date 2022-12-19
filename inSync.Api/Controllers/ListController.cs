using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace inSync.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ListController
	{
		private readonly IMediator _mediator;
        public ListController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetList(Guid id, CancellationToken token)
        {
            return await _mediator.Send(new GetListById(id, token));
        }


    }
}

