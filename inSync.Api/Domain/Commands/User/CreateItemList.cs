#region

using AutoMapper;
using inSync.Api.Data;
using inSync.Api.Models.Dtos;
using inSync.Api.Utils;
using inSync.Api.Validation;
using inSync.Core.Models;
using MediatR;

#endregion

namespace inSync.Api.Domain.Commands.User;

public class CreateItemList : IRequest<Response<Guid>>
{
    public CreateItemList(ItemListRequest request)
    {
        Password = request.Password;
        Username = request.Username;
        Items = request.Items;
    }

    public string Password { get; set; }
    public string Username { get; set; }
    public List<ItemDto> Items { get; set; }
}

public class CreateItemListValidations : IValidationHandler<CreateItemList>
{
    public Task<ValidationResult> Validate(CreateItemList request)
    {
        foreach (var item in request.Items)
            if (item.Amount <= 0)
                return Task.FromResult(ValidationResult.Fail($"amount can not be 0 or less: [{item.ResourceKey}]"));

        return Task.FromResult(ValidationResult.Success);
    }
}

public class CreateItemListHandler : IRequestHandler<CreateItemList, Response<Guid>>
{
    private readonly IMapper _mapper;
    private readonly IDbRepository _repository;

    public CreateItemListHandler(IDbRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Response<Guid>> Handle(CreateItemList request, CancellationToken cancellationToken)
    {
        byte[] hash;
        byte[] salt;
        Crypto.CreateHash(request.Password, out hash, out salt);
        var list = new ItemList
        {
            Id = new Guid(),
            PasswordHash = hash,
            PasswordSalt = salt,
            CreatedAt = DateTime.Now,
            IsActive = true,
            Username = request.Username
        };
        list.Items = _mapper.Map(request.Items, list.Items);
        await _repository.CreateItemList(list);

        return Response<Guid>.Created(list.Id);
    }
}