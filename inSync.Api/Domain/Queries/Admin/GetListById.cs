using inSync.Api.Data;
using inSync.Api.Models.Dtos;
using inSync.Api.Utils;
using inSync.Api.Validation;
using inSync.Core.Models;
using MediatR;

namespace inSync.Api.Domain.Queries.Admin;

public class GetListById : IRequest<Response<ItemListDto>>
{
    public GetListById(Guid id, string adminKey)
    {
        Id = id;
        AdminKey = adminKey;
    }

    public Guid Id { get; set; }
    public string AdminKey { get; set; }
}

public class GetListByIdValidation : IValidationHandler<GetListById>
{
    private readonly IConfiguration _config;
    private readonly IDbRepository _repository;
    
    public GetListByIdValidation(IConfiguration config, IDbRepository repository)
    {
        _config = config;
        _repository = repository;
    }

    public async Task<ValidationResult> Validate(GetListById request)
    {
        if (request.AdminKey != _config.GetValue<string>("AdminKey"))
        {
            return ValidationResult.Fail("Wrong AdminKey");
        }

        if (!await _repository.Exists<ItemList>(request.Id))
        {
            return ValidationResult.Fail($"List with id [{request.Id}] does not exists");
        }
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
        return Response<ItemListDto>.OK(await _repository.GetItemList(request.Id));
    }
}