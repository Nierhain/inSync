using inSync.Api.Data;
using inSync.Api.Models.Dtos;
using inSync.Api.Utils;
using MediatR;

namespace inSync.Api.Domain.Queries;

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