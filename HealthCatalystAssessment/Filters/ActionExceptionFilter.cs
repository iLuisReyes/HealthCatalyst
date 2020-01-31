using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using HealthCatalyst.Assessment.API.Logging;

namespace HealthCatalyst.Assessment.API.Filters
{
    public class ActionExceptionFilter : ExceptionFilterAttribute, IExceptionFilter
    {
        /// <summary>
        /// Manages all unhandled exceptions
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception == null)
                return;

            Logger.WriteError(actionExecutedContext.Exception.Message, actionExecutedContext.Exception);
            HttpResponseMessage response = BuildResponse(actionExecutedContext.Exception);
            actionExecutedContext.Response = response;
        }

        /// <summary>
        /// Builds the proper response type based on the error type
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        internal virtual HttpResponseMessage BuildResponse(Exception exception)
        {
            string res = exception.Message;
            HttpStatusCode statusCode = HttpStatusCode.BadRequest;

            Logger.WriteError(exception.Message, exception);


            return new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(res),
                ReasonPhrase = exception.Source
            };
        }
    }
}