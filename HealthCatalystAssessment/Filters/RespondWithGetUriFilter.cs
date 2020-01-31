using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Controllers;
using System.Net.Http;
using System.Web.Http.Filters;
using System.Net;


namespace HealthCatalyst.Assessment.API.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class RespondWithGetUriFilter : ActionFilterAttribute
    {
        private readonly string getUri;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="getURI"></param>
        public RespondWithGetUriFilter(string getURI)
        {
            this.getUri = getURI;
        }

        /// <summary>
        ///  Adds a Get URI to the response.
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Request.Method == HttpMethod.Post && actionExecutedContext.Response.StatusCode == HttpStatusCode.Created)
            {
                if ((actionExecutedContext.Response != null) && actionExecutedContext.Response.IsSuccessStatusCode)
                {
                    string mimetype = actionExecutedContext.Response.Content.Headers.ContentType.MediaType;
                    if (mimetype == "application/json")
                    {
                        string uri = GetCreatedResourceURI(actionExecutedContext.Response, actionExecutedContext.ActionContext.RequestContext, this.getUri);
                        actionExecutedContext.Response.Headers.Location = new Uri(uri);
                    }
                }
            }
        }

        //TODO: this will fail because objects returned must have values
        internal string GetCreatedResourceURI(HttpResponseMessage response, HttpRequestContext request, string baseUr)
        {
            object id = null;
            response.TryGetContentValue<object>(out id);

            return request.Url == null ? "error" : request.Url.Link(this.getUri, new { id = id });
        }
    }
}