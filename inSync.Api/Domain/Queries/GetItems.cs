using System;
using System.Configuration;
using inSync.Api.Data;
using inSync.Api.Models.Dtos;
using inSync.Api.Utils;
using inSync.Api.Validation;
using inSync.Core.Models;
using MediatR;

namespace inSync.Api.Queries
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
        private readonly IConfiguration _config;

        public GetItemsValidator(IConfiguration config)
        {
            _config = config;
        }

        public async Task<ValidationResult> Validate(GetItems request)
        {
            if (request.AdminKey != _config.GetValue<string>("AdminKey")) return ValidationResult.Fail("Wrong AdminKey");
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

