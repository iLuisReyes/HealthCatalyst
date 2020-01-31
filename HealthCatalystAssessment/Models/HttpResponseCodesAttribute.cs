using System;
using System.Collections.Generic;
using System.Net;

namespace HealthCatalyst.Assessment.API.Models
{
    /// <summary>
    /// Attribute that returns the HTTP response code for the API documentation
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class HttpResponseCodesAttribute : Attribute
    {
        /// <summary>
        /// Attribute that returns the HTTP response code for the API documentation
        /// </summary>
        /// <param name="statusCodes"></param>
        public HttpResponseCodesAttribute(params HttpStatusCode[] statusCodes)
        {
            this.HttpResponseCodes = statusCodes;
        }

        /// <summary>
        /// The http response codes
        /// </summary>
        public IEnumerable<HttpStatusCode> HttpResponseCodes { get; private set; }
        
    }
}