#region

using inSync.Application.Validation;
using inSync.Domain.Models;
using MediatR;
using Microsoft.Extensions.Configuration;

#endregion

namespace inSync.Application.Queries.Admin;

public class VerifyAdminKey : IRequest<Response<bool>>
{
    public VerifyAdminKey(string adminKey)
    {
        AdminKey = adminKey;
    }

    public string AdminKey { get; set; }
}

public class VerifyAdminKeyValidation : IValidationHandler<VerifyAdminKey>
{
    public Task<ValidationResult> Validate(VerifyAdminKey request)
    {
        return Task.FromResult(ValidationResult.Success);
    }
}

public class VerifyAdminKeyHandler : IRequestHandler<VerifyAdminKey, Response<bool>>
{
    private readonly IConfiguration _config;

    public VerifyAdminKeyHandler(IConfiguration config)
    {
        _config = config;
    }

    public Task<Response<bool>> Handle(VerifyAdminKey request, CancellationToken cancellationToken)
    {
        var response = request.AdminKey == _config["AdminKey"]
            ? Response<bool>.Ok(true)
            : Response<bool>.BadRequest("Wrong admin key");
        return Task.FromResult(response);
    }
}