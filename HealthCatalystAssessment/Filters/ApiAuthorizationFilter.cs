using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.ModelBinding;
using Newtonsoft.Json;
using System.Web.Http;


namespace HealthCatalyst.Assessment.API.Filters
{
    /// <summary>
    /// Manage authorization for API
    /// </summary>
    public class ApiAuthorizationFilter : AuthorizeAttribute
    {
        private const string _responseReason = "Must have a valid authorization to access this service.";

        public bool ByPassAuthorization { get; set; }

        /// <summary>
        /// Ensures unauthorized access results in a Forbidden HTTP response and appropriate message
        /// </summary>
        /// <param name="actionContext"></param>
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden);
            if (!string.IsNullOrEmpty(_responseReason))
                actionContext.Response.ReasonPhrase = _responseReason;
        }

        /// <summary>
        /// Checks run prior to authorizing a user
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //logic that will run before even authorizing the customer / user. if this logic fails
            // then the user checking against our custom database table will not processed.
            // you can skip this if you don't have such requirements and directly call
            // if (IsApiPageed(actionContext))
            //{
            //if (!this.IsPluginAvailableForCurrentStore())
            //{
            //    this.HandleUnauthorized(actionContext);
            //    _responseReason = "Web services plugin is not available in this store";
            //}
            //else
            //{
            base.OnAuthorization(actionContext);
            //}
            //}

        }

        /// <summary>
        /// Processes run after a user is successfully authorized
        /// </summary>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            ////logic for check whether we have an attribute with ByPassAuthorization = true e.g [ByPassAuthorization(true)], if so then just return true 
            //if (ByPassAuthorization
            //    //|| GetApiAuthorizeAttributes(actionContext.ActionDescriptor).Any(x => x.ByPassAuthorization)
            //    )
            //    return true;
            ////TODO: checking against our custom table goes here
            ////if (!this.HasWebServiceAccess())
            ////{
            //this.HandleUnauthorizedRequest(actionContext);
            //_responseReason = "Access Denied";
            //return false;
            ////}

            ////return base.IsAuthorized(actionContext);
            return true;
        }


        //private bool IsApiPageed(HttpActionContext actionContext)
        //{
        //    var apiAttributes = GetApiAuthorizeAttributes(actionContext.ActionDescriptor);
        //    if (apiAttributes != null && apiAttributes.Any())
        //        return true;
        //    return false;
        //}



    }
}