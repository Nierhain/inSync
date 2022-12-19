using System;
using inSync.Api.Data;
using inSync.Api.Models.Dtos;
using inSync.Api.Utils;
using MediatR;

namespace inSync.Api.Queries
{
	public class GetListsForUser: IRequest<Response<List<ItemListDto>>>
	{
		public string Username { get; set; }
        public string AdminKey { get; set; }

        public GetListsForUser(string username, string adminKey)
        {
            Username = username;
            AdminKey = adminKey;
        }
	}

    public class GetListsForUserHandler : IRequestHandler<GetListsForUser, Response<List<ItemListDto>>>
    {
        private readonly IDbRepository _repository;

        public GetListsForUserHandler(IDbRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<List<ItemListDto>>> Handle(GetListsForUser request, CancellationToken cancellationToken)
        {
            var list = await _repository.getListsForUser(request.Username);
            return Response<List<ItemListDto>>.OK(list);
        }
    }
}

