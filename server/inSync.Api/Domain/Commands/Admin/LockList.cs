#region

using inSync.Api.Data;
using inSync.Api.Utils;
using inSync.Api.Validation;
using inSync.Core.Models;
using MediatR;

#endregion

namespace inSync.Api.Domain.Commands.Admin;

public class LockList : IRequest<Response<bool>>
{
    public LockList(Guid id, string adminKey, string reason, bool isLocked)
    {
        Id = id;
        AdminKey = adminKey;
        Reason = reason;
        IsLocked = isLocked;
    }

    public Guid Id { get; set; }
    public string AdminKey { get; set; }
    public string Reason { get; set; }
    public bool IsLocked { get; set; }
}

public class LockListValidator : IValidationHandler<LockList>
{
    private readonly IConfiguration _config;
    private readonly IDbRepository _repository;

    public LockListValidator(IConfiguration config, IDbRepository repository)
    {
        _config = config;
        _repository = repository;
    }

    public async Task<ValidationResult> Validate(LockList request)
    {
        if (request.AdminKey != _config["AdminKey"]) return ValidationResult.Fail("wrong AdminKey");

        if (!await _repository.Exists<ItemList>(request.Id))
            return ValidationResult.Fail($"List with id [{request.Id}] does not exist");
        return ValidationResult.Success;
    }
}

public class LockListHandler : IRequestHandler<LockList, Response<bool>>
{
    private readonly IDbRepository _repository;

    public LockListHandler(IDbRepository repository)
    {
        _repository = repository;
    }

    public async Task<Response<bool>> Handle(LockList request, CancellationToken cancellationToken)
    {
        var list = await _repository.GetItemList(request.Id);
        list.IsLockedByAdmin = request.IsLocked;
        list.LockReason = request.Reason;
        await _repository.UpdateItemList(list.Id, list);
        return Response<bool>.OK(true);
    }
}