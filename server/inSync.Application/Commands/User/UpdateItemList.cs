#region

using inSync.Application.Models.Dtos;
using inSync.Application.Models.Requests;
using inSync.Application.Validation;
using inSync.Domain.Abstractions;
using inSync.Domain.ItemLists;
using inSync.Domain.Models;
using MediatR;

#endregion

namespace inSync.Application.Commands.User;

public class UpdateItemList : IRequest<Response<bool>>
{
    public UpdateItemList(UpdateListRequest request, string password)
    {
        Id = request.Id;
        Password = password;
        ItemList = request.ItemList;
    }

    public Guid Id { get; set; }
    public string Password { get; set; }
    public ItemListUpdate ItemList { get; set; }
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
        if (!await _crypto.VerifyHash(request.Id, request.Password)) return ValidationResult.Fail("wrong password");
        if (!await _repository.Exists<ItemList>(request.Id)) return ValidationResult.Fail("List does not exist");

        var list = await _repository.GetItemList(request.Id);
        if (list.IsLockedByAdmin) return ValidationResult.Fail("List is locked by admin");
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
        return Response<bool>.Ok(true);
    }
}