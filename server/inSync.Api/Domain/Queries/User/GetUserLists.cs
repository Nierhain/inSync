#region

using inSync.Api.Data;
using inSync.Api.Models.Dtos;
using inSync.Api.Utils;
using MediatR;

#endregion

namespace inSync.Api.Domain.Queries.User;

public class GetUserLists : IRequest<Response<List<ItemListOverview>>>
{
    public GetUserLists(string username)
    {
        Username = username;
    }

    public string Username { get; set; }
}

public class GetUserListsHandler : IRequestHandler<GetUserLists, Response<List<ItemListOverview>>>
{
    private readonly IDbRepository _repository;

    public GetUserListsHandler(IDbRepository repository)
    {
        _repository = repository;
    }

    public async Task<Response<List<ItemListOverview>>> Handle(GetUserLists request,
        CancellationToken cancellationToken)
    {
        var lists = await _repository.GetListsForUser(request.Username);
        return Response<List<ItemListOverview>>.OK(lists);
    }
}