#region

using AutoMapper;
using inSync.Application.Models.Requests;
using inSync.Application.Utils;
using inSync.Application.Validation;
using inSync.Domain.ItemLists;
using inSync.Domain.Models;
using MediatR;

#endregion

namespace inSync.Application.Commands.User;

public class CreateItemList : IRequest<Response<Guid>>
{
    public CreateItemList(ItemListRequest request, string password)
    {
        Password = password;
        List = request;
    }

    public string Password { get; set; }
    public ItemListRequest List { get; set; }
}

public class CreateItemListValidations : IValidationHandler<CreateItemList>
{
    public Task<ValidationResult> Validate(CreateItemList request)
    {
        foreach (var item in request.List.Items)
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
            IsActive = true
        };
        list = _mapper.Map(request.List, list);
        await _repository.CreateItemList(list);

        return Response<Guid>.Created(list.Id);
    }
}