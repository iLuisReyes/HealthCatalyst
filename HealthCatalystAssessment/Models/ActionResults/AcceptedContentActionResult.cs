using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Text;

namespace HealthCatalyst.Assessment.API.Models
{
    /// <summary>
    /// An accepted content object that does not return strings with extra quotation marks.  
    /// </summary>
    public class AcceptedContentActionResult<T> : IHttpActionResult
    {
        private readonly T _response;
        private readonly string _location;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="response"></param>
        /// <param name="location"></param>
        public AcceptedContentActionResult(T response, string location)
        {
            _response = response;
            _location = location;
        }

        /// <summary>
        /// HTTP Response handler
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(HttpStatusCode.Accepted); //_request.CreateResponse(HttpStatusCode.Created);
            response.Content = new StringContent(_response.ToString(), Encoding.UTF8, "application/json");
            response.Headers.Location = new Uri(_location);
            return Task.FromResult(response);
        }
    }
}