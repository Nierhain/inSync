using inSync.Api.Data;
using inSync.Api.Models.Dtos;
using inSync.Api.Utils;
using inSync.Api.Validation;
using inSync.Core.Models;
using MediatR;

namespace inSync.Api.Domain.Commands.User;

public class UpdateItemList : IRequest<Response<bool>>
{
    public UpdateItemList(Guid id, string password, ItemListDto itemList)
    {
        Id = id;
        Password = password;
        ItemList = itemList;
    }

    public Guid Id { get; set; }
    public string Password { get; set; }
    public ItemListDto ItemList { get; set; }
}

public class UpdateItemListValidator : IValidationHandler<UpdateItemList>
{
    private readonly ICryptoRepository _crypto;
    private readonly IDbRepository _repository;

    public UpdateItemListValidator(ICryptoRepository crypto, IDbRepository repository)
    {
        _crypto = crypto;
        _repository = repository;
    }

    public async Task<ValidationResult> Validate(UpdateItemList request)
    {
        if (!await _crypto.VerifyHash(request.Id, request.Password))
        {
            return ValidationResult.Fail("wrong password");
        }
        if (!await _repository.Exists<ItemList>(request.Id))
        {
            return ValidationResult.Fail("List does not exist");
        }
        return ValidationResult.Success;
    }
}

public class UpdateItemListHandler : IRequestHandler<UpdateItemList, Response<bool>>
{
    private readonly IDbRepository _repository;

    public UpdateItemListHandler(IDbRepository repository)
    {
        _repository = repository;
    }

    public async Task<Response<bool>> Handle(UpdateItemList request, CancellationToken cancellationToken)
    {
        await _repository.UpdateItemList(request.Id, request.ItemList);
        return Response<bool>.OK(true);
    }
}