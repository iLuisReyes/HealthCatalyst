using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;


namespace HealthCatalyst.Assessment.API.Filters
{
    public class SimulatedLatencyFilter : ActionFilterAttribute
    {
        private readonly int minDelayInMs = 0;
        private readonly int maxDelayInMs;     
            
        private readonly ThreadLocal<Random> _random;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="latencyMS"></param>
        public SimulatedLatencyFilter(string latencyMS)
        {
            int.TryParse(latencyMS, out int delay);
            maxDelayInMs = delay < 0 ? 0 : delay; 

            _random = new ThreadLocal<Random>(() => new Random());
        }

        /// <summary>
        /// Manage database initiation and transactions for API when a request is received.
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (maxDelayInMs == 0)
                return;

            int delayInMs = _random.Value.Next(
                minDelayInMs,
                maxDelayInMs
            );

            Thread.Sleep(TimeSpan.FromMilliseconds(delayInMs));
            base.OnActionExecuting(actionContext);
        }
    }
}