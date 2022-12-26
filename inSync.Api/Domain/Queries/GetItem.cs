using inSync.Api.Data;
using inSync.Api.Models.Dtos;
using inSync.Api.Utils;
using inSync.Api.Validation;
using inSync.Core.Models;
using MediatR;

namespace inSync.Api.Domain.Queries
{
	public class GetItem : IRequest<Response<ItemDto>>
	{
		public Guid Id { get; set; }
        public GetItem(Guid id)
        {
            Id = id;
        }
    }

    public class Validator : IValidationHandler<GetItem>
    {
        private readonly IDbRepository _repository;

        public Validator(IDbRepository repository)
        {
            _repository = repository;
        }

        public async Task<ValidationResult> Validate(GetItem request)
        {
            if (! await _repository.exists<Item>(request.Id))
            {
                return ValidationResult.Fail("Item does not exist");
            }
            return ValidationResult.Success;    
        }
    }

    public class GetItemHandler : IRequestHandler<GetItem, Response<ItemDto>>{

        private readonly IDbRepository _repository;

        public GetItemHandler(IDbRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<ItemDto>> Handle(GetItem request, CancellationToken cancellationToken)
        {
            var item = await _repository.getItem(request.Id);
            return Response<ItemDto>.OK(item);
        }
    }
}

