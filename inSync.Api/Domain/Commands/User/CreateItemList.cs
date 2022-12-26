using AutoMapper;
using inSync.Api.Data;
using inSync.Api.Models.Dtos;
using inSync.Api.Utils;
using inSync.Api.Validation;
using inSync.Core.Models;
using MediatR;

namespace inSync.Api.Domain.Commands.User;

public class CreateItemList : IRequest<Response<bool>>
{
   public string Password { get; set; }
   public string Username { get; set; }
   public List<ItemDto> Items { get; set; }

   public CreateItemList(ItemListRequest request)
   {
       Password = request.Password;
       Username = request.Username;
       Items = request.Items;
   }
}

public class CreateItemListValidations : IValidationHandler<CreateItemList>
{
    public async Task<ValidationResult> Validate(CreateItemList request)
    {
        foreach (var item in request.Items)
        {
            if (item.Amount <= 0)
            {
                return ValidationResult.Fail($"amount can not be 0 or less: [{item.ResourceKey}]");
            }
        }

        return ValidationResult.Success;
    }
}

public class CreateItemListHandler : IRequestHandler<CreateItemList, Response<bool>>
{
    private readonly IDbRepository _repository;
    private readonly IMapper _mapper;

    public CreateItemListHandler(IDbRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Response<bool>> Handle(CreateItemList request, CancellationToken cancellationToken)
    {
        byte[] hash;
        byte[] salt;
        Crypto.CreateHash(request.Password, out hash, out salt);
        var list = new ItemList
        {
          PasswordHash = hash,
          PasswordSalt = salt,
          CreatedAt = DateTime.Now,
          IsActive = true,
          Username = request.Username
        };
        list.Items = _mapper.Map(request.Items, list.Items);
        await _repository.CreateItemList(list);

        return Response<bool>.Created();
    }
}