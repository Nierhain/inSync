using inSync.Application.Models;
using inSync.Application.Models.Dtos;
using MediatR;

namespace inSync.Application.Queries;

public class GetItemNames : IRequest<Response<List<MinecraftItemDto>>>
{
}

public class GetItemNamesHandler : IRequestHandler<GetItemNames, Response<List<MinecraftItemDto>>>
{
    private readonly IDbRepository _repository;

    public GetItemNamesHandler(IDbRepository repository)
    {
        _repository = repository;
    }

    public async Task<Response<List<MinecraftItemDto>>> Handle(GetItemNames request, CancellationToken cancellationToken)
    {
        return Response<List<MinecraftItemDto>>.OK(await _repository.GetMinecraftItems());
    }
}