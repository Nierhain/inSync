using inSync.Api.Data;
using inSync.Api.Models.Dtos;
using inSync.Api.Utils;
using inSync.Api.Validation;
using MediatR;

namespace inSync.Api.Domain.Commands.Admin;

public class UpdateMinecraftItems : IRequest<Response<List<MinecraftItemDto>>>
{
    public UpdateMinecraftItems(List<MinecraftItemDto> items, string adminKey)
    {
        Items = items;
        AdminKey = adminKey;
    }

    public List<MinecraftItemDto> Items { get; set; }
    public string AdminKey { get; set; }
}

public class UpdateMinecraftItemsValidator : IValidationHandler<UpdateMinecraftItems>
{
    private readonly IConfiguration _config;

    public UpdateMinecraftItemsValidator(IConfiguration config)
    {
        _config = config;
    }

    public Task<ValidationResult> Validate(UpdateMinecraftItems request)
    {
        if (_config["AdminKey"] != request.AdminKey) return Task.FromResult(ValidationResult.Fail("wrong admin key"));
        return Task.FromResult( ValidationResult.Success);
    }
}

public class UpdateMinecraftItemsHandler : IRequestHandler<UpdateMinecraftItems, Response<List<MinecraftItemDto>>>
{
    private readonly IDbRepository _repository;

    public UpdateMinecraftItemsHandler(IDbRepository repository)
    {
        _repository = repository;
    }

    public async Task<Response<List<MinecraftItemDto>>> Handle(UpdateMinecraftItems request, CancellationToken cancellationToken)
    {
        await _repository.UpdateMinecraftItems(request.Items);
        return Response<List<MinecraftItemDto>>.OK(await _repository.GetMinecraftItems());
    }
}