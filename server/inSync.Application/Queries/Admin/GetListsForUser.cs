#region

using inSync.Application.Models.Dtos;
using inSync.Application.Validation;
using inSync.Domain.Models;
using MediatR;
using Microsoft.Extensions.Configuration;

#endregion

namespace inSync.Application.Queries.Admin;

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
        return Response<List<ItemListOverview>>.Ok(await _repository.GetListsForUser(request.Username));
    }
}