using System;
using inSync.Api.Data;
using inSync.Api.Models.Dtos;
using inSync.Api.Utils;
using inSync.Api.Validation;
using MediatR;

namespace inSync.Api.Queries
{
	public class GetLists : IRequest<Response<List<ItemListDto>>>
	{
		public string AdminKey {get; set; }

        public GetLists(string adminKey)
        {
            AdminKey = adminKey;
        }
    }

    public class GetListsValidator : IValidationHandler<GetItems>
    {
        private readonly IConfiguration _config;

        public GetListsValidator(IConfiguration config)
        {
            _config = config;
        }

        public async Task<ValidationResult> Validate(GetItems request)
        {
            if (request.AdminKey != _config.GetValue<string>("AdminKey")) return ValidationResult.Fail("Wrong AdminKey");
            return ValidationResult.Success;
        }
    }

    public class GetListsHandler : IRequestHandler<GetLists, Response<List<ItemListDto>>>
    {
        private readonly IDbRepository _repository;

        public GetListsHandler(IDbRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<List<ItemListDto>>> Handle(GetLists request, CancellationToken cancellationToken)
        {
            var lists = await _repository.getItemLists();
            return Response<List<ItemListDto>>.OK(lists);
        }
    }
}

