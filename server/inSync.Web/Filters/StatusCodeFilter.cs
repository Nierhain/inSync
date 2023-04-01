#region

using System.Net;
using inSync.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

#endregion

namespace inSync.Web.Filters;

public class StatusCodeFilter : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Result is ObjectResult objectResult && objectResult.Value is MediatorResponse mediatorResponse &&
            mediatorResponse.StatusCode != HttpStatusCode.OK)
            context.Result =
                new ObjectResult(new { mediatorResponse.ErrorMessage })
                    { StatusCode = (int)mediatorResponse.StatusCode };
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
    }
}