using inSync.Api.Data;
using inSync.Api.Models.Dtos;
using inSync.Api.Utils;
using MediatR;

namespace inSync.Api.Domain.Queries.User;

public class GetUserLists : IRequest<Response<List<ItemListOverviewDto>>>
{
    public GetUserLists(string username)
    {
        Username = username;
    }

    public string Username { get; set; }
}


public class GetUserListsHandler : IRequestHandler<GetUserLists, Response<List<ItemListOverviewDto>>>
{
    private readonly IDbRepository _repository;

    public GetUserListsHandler(IDbRepository repository)
    {
        _repository = repository;
    }

    public async Task<Response<List<ItemListOverviewDto>>> Handle(GetUserLists request, CancellationToken cancellationToken)
    {
        var lists = await _repository.GetListsForUser(request.Username);
        return Response<List<ItemListOverviewDto>>.OK(lists);
    }
}