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
using Newtonsoft.Json;

namespace HealthCatalyst.Assessment.API.Models
{
    /// <summary>
    /// A created content object that does not return strings with extra quotation marks.  
    /// </summary>
    public class CreatedContentActionResult<T> : IHttpActionResult
    {
        internal readonly T _response;
        private readonly string _location;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="response"></param>
        /// <param name="location"></param>
        public CreatedContentActionResult(T response, string location)
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
            var response = new HttpResponseMessage(HttpStatusCode.Created)
            {
                Content = new StringContent(JsonConvert.SerializeObject(_response), encoding: Encoding.UTF8, mediaType: "application/json")
            };
            response.Headers.Location = new Uri(_location);

            return Task.FromResult(response);
        }
    }
}