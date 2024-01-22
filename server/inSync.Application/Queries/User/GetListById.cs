#region

using inSync.Application.Errors;
using inSync.Application.Models.Dtos;
using inSync.Application.Validation;
using inSync.Domain.Abstractions;
using inSync.Domain.ItemLists;
using inSync.Domain.Models;
using inSync.Domain.Users;
using MediatR;

#endregion

namespace inSync.Application.Queries.User;

public class GetListById : IRequest<Response<ItemListDto>>
{
    public GetListById(Guid id, Credentials credentials)
    {
        Id = id;
        Credentials = credentials;
    }

    public Guid Id { get; set; }
    public Credentials Credentials { get; set; }
}

internal sealed class GetListByIdValidator : IValidationHandler<GetListById>
{
    private readonly ICryptoRepository _crypto;
    private readonly IDbRepository _repository;

    public GetListByIdValidator(IDbRepository repository, ICryptoRepository crypto)
    {
        _repository = repository;
        _crypto = crypto;
    }

    public async Task<ValidationResult> Validate(GetListById request)
    {
        if (!await _repository.Exists<ItemList>(request.Id)) return ValidationResult.Fail(Errors.Errors.ItemLists.ListDoesNotExist);
        if (!await _crypto.VerifyHash(request.Id, request.Credentials.Password)) return ValidationResult.Fail(Errors.Errors.Users.WrongCredentials);
        
        var list = await _repository.GetItemList(request.Id); 
        if(list.Username != request.Credentials.Username) return ValidationResult.Fail(Errors.Errors.Users.WrongCredentials);
        
        return ValidationResult.Success;
    }
}

internal sealed class GetListByIdHandler : IRequestHandler<GetListById, Response<ItemListDto>>
{
    private readonly IDbRepository _repository;

    public GetListByIdHandler(IDbRepository repository)
    {
        _repository = repository;
    }

    public async Task<Response<ItemListDto>> Handle(GetListById request, CancellationToken cancellationToken)
    {
        var list = await _repository.GetItemList(request.Id);
        return Response<ItemListDto>.Ok(list);
    }
}