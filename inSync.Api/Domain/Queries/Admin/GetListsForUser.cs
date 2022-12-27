#region

using inSync.Api.Data;
using inSync.Api.Models.Dtos;
using inSync.Api.Utils;
using inSync.Api.Validation;
using MediatR;

#endregion

namespace inSync.Api.Domain.Queries.Admin;

public class GetListsForUser : IRequest<Response<List<ItemListOverview>>>
{
    public GetListsForUser(string username, string adminKey)
    {
        Username = username;
        AdminKey = adminKey;
    }

    public string Username { get; set; }
    public string AdminKey { get; set; }
}

public class GetListsForUserValidator : IValidationHandler<GetListsForUser>
{
    private readonly IConfiguration _config;

    public GetListsForUserValidator(IConfiguration config)
    {
        _config = config;
    }

    public Task<ValidationResult> Validate(GetListsForUser request)
    {
        if (request.AdminKey != _config["AdminKey"]) return Task.FromResult(ValidationResult.Fail("Wrong AdminKey"));
        return Task.FromResult(ValidationResult.Success);
    }
}

public class GetListsForUserHandler : IRequestHandler<GetListsForUser, Response<List<ItemListOverview>>>
{
    private readonly IDbRepository _repository;

    public GetListsForUserHandler(IDbRepository repository)
    {
        _repository = repository;
    }

    public async Task<Response<List<ItemListOverview>>> Handle(GetListsForUser request,
        CancellationToken cancellationToken)
    {
        return Response<List<ItemListOverview>>.OK(await _repository.GetListsForUser(request.Username));
    }
}