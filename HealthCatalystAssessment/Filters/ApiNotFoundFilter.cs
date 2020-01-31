using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace HealthCatalyst.Assessment.API.Filters
{
    /// <summary>
    /// Converts <c>null</c> return values into an HTTP 404 return code.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class ApiNotFoundFilter : ActionFilterAttribute
    {
        /// <summary>
        ///  Validates HTTP GET request and, if data value was not found, returns HTTP NOTFOUND response.
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Request.Method == HttpMethod.Get)
            {
                if ((actionExecutedContext.Response != null) && actionExecutedContext.Response.IsSuccessStatusCode)
                {
                    string mimetype = actionExecutedContext.Response.Content.Headers.ContentType.MediaType;
                    if (mimetype == "application/json")
                    {
                        actionExecutedContext.Response.TryGetContentValue<object>(out object contentValue);
                        if (contentValue == null)
                        {
                            actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.NotFound, "Object not found");
                        }
                    }
                }
            }
        }
    }
}