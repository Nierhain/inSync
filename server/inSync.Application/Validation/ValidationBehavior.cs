﻿#region

using System.Net;
using inSync.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

#endregion

namespace inSync.Application.Validation;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : MediatorResponse, new()
{
    private readonly ILogger<ValidationBehavior<TRequest, TResponse>> logger;
    private readonly IValidationHandler<TRequest> validationHandler;

    // Have 2 constructors incase the validator does not exist
#pragma warning disable CS8618
    public ValidationBehavior(ILogger<ValidationBehavior<TRequest, TResponse>> logger)
#pragma warning restore CS8618
    {
        this.logger = logger;
    }

    public ValidationBehavior(ILogger<ValidationBehavior<TRequest, TResponse>> logger,
        IValidationHandler<TRequest> validationHandler)
    {
        this.logger = logger;
        this.validationHandler = validationHandler;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = request.GetType();
        if (validationHandler == null)
        {
            logger.LogInformation("{Request} does not have a validation handler configured.", requestName);
            return await next();
        }

        var result = await validationHandler.Validate(request);
        if (!result.IsSuccessful)
        {
            logger.LogWarning("Validation failed for {Request}. Error: {Error}", requestName, result.Error);
            return new TResponse { StatusCode = HttpStatusCode.BadRequest, ErrorMessage = result.Error };
        }

        logger.LogInformation("Validation successful for {Request}.", requestName);
        return await next();
    }
}