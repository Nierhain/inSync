using inSync.Api.Data;
using inSync.Api.Models.Dtos;
using inSync.Api.Utils;
using inSync.Api.Validation;
using MediatR;

namespace inSync.Api.Domain.Queries.Admin;

public class GetListsForUser : IRequest<Response<List<ItemListOverviewDto>>>
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

    public async Task<ValidationResult> Validate(GetListsForUser request)
    {
        if (request.AdminKey != _config["AdminKey"])
        {
            return ValidationResult.Fail("Wrong AdminKey");
        }
        return ValidationResult.Success;
    }
}

public class GetListsForUserHandler : IRequestHandler<GetListsForUser, Response<List<ItemListOverviewDto>>>
{
    private readonly IDbRepository _repository;

    public GetListsForUserHandler(IDbRepository repository)
    {
        _repository = repository;
    }

    public async Task<Response<List<ItemListOverviewDto>>> Handle(GetListsForUser request, CancellationToken cancellationToken)
    {
        return Response<List<ItemListOverviewDto>>.OK(await _repository.GetListsForUser(request.Username));
    }
}