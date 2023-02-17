#region

using inSync.Api.Data;
using inSync.Api.Models.Dtos;
using inSync.Api.Utils;
using inSync.Api.Validation;
using inSync.Core.Models;
using MediatR;

#endregion

namespace inSync.Api.Domain.Queries.User;

public class GetListById : IRequest<Response<ItemListDto>>
{
    public GetListById(Guid id, string password)
    {
        Id = id;
        Password = password;
    }

    public Guid Id { get; set; }
    public string Password { get; set; }
}

public class GetListByIdValidator : IValidationHandler<GetListById>
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
        if (!await _repository.Exists<ItemList>(request.Id)) return ValidationResult.Fail("List not found");
        if (!await _crypto.VerifyHash(request.Id, request.Password)) return ValidationResult.Fail("not authorized");
        return ValidationResult.Success;
    }
}

public class GetListByIdHandler : IRequestHandler<GetListById, Response<ItemListDto>>
{
    private readonly IDbRepository _repository;

    public GetListByIdHandler(IDbRepository repository)
    {
        _repository = repository;
    }

    public async Task<Response<ItemListDto>> Handle(GetListById request, CancellationToken cancellationToken)
    {
        var list = await _repository.GetItemList(request.Id);
        return Response<ItemListDto>.OK(list);
    }
}