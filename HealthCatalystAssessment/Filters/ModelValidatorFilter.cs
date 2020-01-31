using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace HealthCatalyst.Assessment.API.Filters
{
    /// <summary>
    /// Manage model state validation
    /// </summary>
    public class ModelValidatorFilter : ActionFilterAttribute
    {
        /// <summary>
        /// Manage model state validation on a request is received.
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.Request.Method == HttpMethod.Put || actionContext.Request.Method == HttpMethod.Post)
            {
                if (actionContext.ModelState != null)
                {
                    if (actionContext.ModelState.IsValid == false)
                    {
                        actionContext.Response = actionContext.Request.CreateErrorResponse(
                            HttpStatusCode.BadRequest, actionContext.ModelState);
                    }
                }
            }
        }
    }
}