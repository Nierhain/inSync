﻿using System;
using inSync.Api.Data;
using inSync.Api.Models.Dtos;
using inSync.Api.Utils;
using inSync.Api.Validation;
using inSync.Core.Models;
using MediatR;

namespace inSync.Api.Queries
{
	public class GetListById : IRequest<Response<ItemListDto>>
	{
        public GetListById(Guid id, string password)
        {
            Id = id;
            Password = password;
        }

        public Guid Id { get; set; }
        public string Password { get; set; }
	}

    public class GetListByIdValidator : IValidationHandler<GetListById>
    {
        private readonly IDbRepository _repository;

        public GetListByIdValidator(IDbRepository repository)
        {
            _repository = repository;
        }

        public async Task<ValidationResult> Validate(GetListById request)
        {
            var list = await _repository.getItemList(request.Id);
            if(Crypto.VerifyHash(request.Password, list.))
            return ValidationResult.Success;
        }
    }

    public class GetListByIdHandler : IRequestHandler<GetListById, Response<ItemListDto>>
    {
        private readonly IDbRepository _repository;

        public GetListByIdHandler(IDbRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<ItemListDto>> Handle(GetListById request, CancellationToken cancellationToken)
        {
            var list = await _repository.getItemList(request.Id);
            return Response<ItemListDto>.OK(list);
        }
    }
}