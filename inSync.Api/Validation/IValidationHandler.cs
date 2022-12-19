﻿using System;
using System.ComponentModel.DataAnnotations;

namespace inSync.Api.Validation
{
    public interface IValidationHandler
    {
    }

    public interface IValidationHandler<T> : IValidationHandler
    {
        Task<ValidationResult> Validate(T request);
    }
}
