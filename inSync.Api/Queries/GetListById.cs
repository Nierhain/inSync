using System;
using inSync.Core.Models;
using MediatR;

namespace inSync.Api.Queries
{
	public class GetListById : IRequest<ItemList>
	{
		public Guid Id { get; set; }
	}

    public class GetListByIdHandler : IRequestHandler<GetListById, ItemList>
    {
        private readonly 
        public Task<ItemList> Handle(GetListById request, CancellationToken cancellationToken)
        {
            
        }
    }
}