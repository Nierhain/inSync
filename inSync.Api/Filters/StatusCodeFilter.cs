using System;
using System.Net;
using inSync.Api.Data;
using inSync.Api.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace inSync.Api.Filters
{
    public class StatusCodeFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is ObjectResult objectResult && objectResult.Value is MediatorResponse mediatorResponse &&
                mediatorResponse.StatusCode != HttpStatusCode.OK)
            {
                context.Result =
                    new ObjectResult(new { mediatorResponse.ErrorMessage }) { StatusCode = (int)mediatorResponse.StatusCode };
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}

