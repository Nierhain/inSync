#region

using inSync.Api.Data;
using inSync.Api.Models.Dtos;
using inSync.Api.Utils;
using inSync.Api.Validation;
using MediatR;

#endregion

namespace inSync.Api.Domain.Queries.Admin;

public class GetLists : IRequest<Response<List<ItemListOverview>>>
{
    public GetLists(string adminKey)
    {
        AdminKey = adminKey;
    }

    public string AdminKey { get; set; }
}

public class GetListsValidator : IValidationHandler<GetLists>
{
    private readonly IConfiguration _config;

    public GetListsValidator(IConfiguration config)
    {
        _config = config;
    }

    public Task<ValidationResult> Validate(GetLists request)
    {
        return request.AdminKey != _config.GetValue<string>("AdminKey")
            ? Task.FromResult(ValidationResult.Fail("Wrong AdminKey"))
            : Task.FromResult(ValidationResult.Success);
    }
}

public class GetListsHandler : IRequestHandler<GetLists, Response<List<ItemListOverview>>>
{
    private readonly IDbRepository _repository;

    public GetListsHandler(IDbRepository repository)
    {
        _repository = repository;
    }

    public async Task<Response<List<ItemListOverview>>> Handle(GetLists request, CancellationToken cancellationToken)
    {
        var lists = await _repository.GetLists();
        return Response<List<ItemListOverview>>.OK(lists);
    }
}