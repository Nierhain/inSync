using inSync.Api.Data;
using inSync.Api.Models.Dtos;
using inSync.Api.Utils;
using inSync.Api.Validation;
using MediatR;

namespace inSync.Api.Domain.Queries
{
	public class GetItems : IRequest<Response<List<ItemDto>>>
	{
		public string AdminKey { get; set; }

        public GetItems(string adminKey)
        {
            AdminKey = adminKey;
        }
	}

    public class GetItemsValidator : IValidationHandler<GetItems>
    {
        private readonly string _adminKey;

        public GetItemsValidator(IConfiguration config)
        {
            _adminKey = config.GetValue<string>("AdminKey");
        }

        public async Task<ValidationResult> Validate(GetItems request)
        {
            if (request.AdminKey != _adminKey) return ValidationResult.Fail("Wrong AdminKey");
            return ValidationResult.Success;
        }
    }

    public class GetItemsHandler : IRequestHandler<GetItems, Response<List<ItemDto>>>
    {
        private readonly IDbRepository _repository;

        public GetItemsHandler(IDbRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<List<ItemDto>>> Handle(GetItems request, CancellationToken cancellationToken)
        {
            var items = await _repository.getItems();
            return Response<List<ItemDto>>.OK(items);
        }
    }
}

