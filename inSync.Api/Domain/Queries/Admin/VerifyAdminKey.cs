#region

using inSync.Api.Utils;
using MediatR;

#endregion

namespace inSync.Api.Domain.Queries.Admin;

public class VerifyAdminKey : IRequest<Response<bool>>
{
    public VerifyAdminKey(string adminKey)
    {
        AdminKey = adminKey;
    }

    public string AdminKey { get; set; }
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
            ? Response<bool>.OK(true)
            : Response<bool>.BadRequest("Wrong admin key");
        return Task.FromResult(response);
    }
}