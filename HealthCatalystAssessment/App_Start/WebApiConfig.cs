using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using HealthCatalyst.Assessment.API.Filters;

namespace HealthCatalyst.Assessment.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));

            // Web API configuration and services
            config.Filters.Add(new ActionExceptionFilter());
            config.Filters.Add(new ApiNotFoundFilter());
            config.Filters.Add(new ModelValidatorFilter());
            config.Filters.Add(new SimulatedLatencyFilter(System.Configuration.ConfigurationManager.AppSettings["latencyMS"]));

            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            ApiActionFilterProvider.RegisterFilterProviders(GlobalConfiguration.Configuration);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
