using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;


namespace HealthCatalyst.Assessment.API.Filters
{
    /// <summary>
    /// Maps an outgoing response to the DTO object
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class MapToDtoFilter : ActionFilterAttribute
    {

        /// <summary>
        /// The destination DTO type
        /// </summary>
        private readonly Type type;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="type">the destination DTO type</param>
        public MapToDtoFilter(Type type)
        {
            this.type = type;
        }



        /// <summary>
        ///  Validates HTTP GET request and, if data value was not found, returns HTTP NOTFOUND response.
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if ((actionExecutedContext.Response != null) && actionExecutedContext.Response.IsSuccessStatusCode)
            {
                string mimetype = actionExecutedContext.Response.Content.Headers.ContentType.MediaType;
                if (mimetype == "application/json")
                {
                    actionExecutedContext.Response.Content = Transform(actionExecutedContext.Response.Content);
                }
            }
        }

        //TODO: Test this
        internal ObjectContent Transform(HttpContent source)
        {
            var objectContent = source as ObjectContent;
            if (objectContent != null)
            {
                var mapper = MappingConfig.Mapper.CreateMapper();
                objectContent = mapper.Map(objectContent.Value, objectContent.GetType(), type) as ObjectContent;
            }
            return objectContent;
        }
    }
}