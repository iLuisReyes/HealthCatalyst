using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using HealthCatalyst.Assessment.API.Models;
using HealthCatalyst.Assessment.API.Models.Team;
using HealthCatalyst.Assessment.API.Facades;
using HealthCatalyst.Assessment.API.Filters;
using HealthCatalyst.Assessment.Domain.Validation;


namespace HealthCatalyst.Assessment.API.Controllers
{
    /// <summary>
    /// An interface to establish the routes and actions of team activities
    /// </summary>
    //[Authorize]
    [RoutePrefix("team")]
    public class TeamController : ApiController
    {
        /// <summary>
        /// A facade to the domain services that manage the business rules for this request
        /// </summary>
        private readonly TeammateSearchFacade searchFacade;

        /// <summary>
        /// Constructor for dependency injection       
        /// </summary>
        /// <param name="searchFacade">Facade encapsulating people search activities within the domain services</param>
        public TeamController(TeammateSearchFacade searchFacade)
        {
            this.searchFacade = searchFacade;
        }

        /// <summary>
        /// Searches the team for a teammate with specific value that could be found in the first and/or lastname
        /// </summary>
        /// <param name="term">The terms to search by (i.e. first name or last name)</param>
        /// <returns></returns>
        [Route("search/{term}", Name = "SearchTeam")]
        [ResponseType(typeof(IEnumerable<GetTeammateResponse>))]
        [HttpResponseCodes(HttpStatusCode.OK, HttpStatusCode.BadRequest)]
        [HttpGet]
        public IHttpActionResult SearchTeam([FromUri]string term)
        {
            var target = searchFacade.SearchTeam(term);
            return Ok(target);
        }

        /// <summary>
        /// Adds/Creates a new teammate
        /// </summary>
        /// <param name="body">The body of the request as a JSON representation of the CreateTeammateRequest DTO.</param>
        /// <returns></returns>
        /// <remarks>
        ///     Validation happens automatically via global action filter
        ///      Exception handling also happens automatically via global action filter
        ///      Logging happens automaticaly via global action filter
        ///      Transaction Management happens automatically via local action filter (per DBTransactionFilter attribute)
        ///  </remarks>
        [Route]
        [ResponseType(typeof(CreateTeammateResponse))]
        [DBTransactionFilter]
        [HttpResponseCodes(HttpStatusCode.Created, HttpStatusCode.BadRequest)]
        [HttpPost]
        public virtual IHttpActionResult CreateTeammate([FromBody]CreateTeammateRequest body)
        {
            Guard.NotNull(body, "Message Body");
            var result = searchFacade.CreateTeammate(body);

            string uri = this.Url == null ? "error" : this.Url.Link("GetTeammate", new { id = result.ID });
            return new CreatedContentActionResult<CreateTeammateResponse>(result, uri);
        }

        /// <summary>
        /// Gets an teammate based on the teammate's identifier
        /// </summary>
        /// <param name="id">The teammate's identifier</param>
        /// <returns>An individual teammate</returns>
        /// <remarks>Null check for return value is done in ApiNotFound global action filter</remarks>
        [Route("{id}", Name = "GetTeammate")]
        [ResponseType(typeof(GetTeammateResponse))]
        [HttpResponseCodes(HttpStatusCode.OK, HttpStatusCode.BadRequest)]
        [HttpGet]
        public IHttpActionResult GetTeammate([FromUri] long id)
        {
            var target = searchFacade.GetTeammate(id);
            return Ok(target);
        }

        /// <summary>
        /// Gets all teammates on the roster
        /// </summary>
        /// <returns>The list of individual team members</returns>
        /// <remarks>Null check for return value is done in ApiNotFound global action filter</remarks>
        [Route(Name = "GetTeam")]
        [ResponseType(typeof(IEnumerable<GetTeammateResponse>))]
        [HttpResponseCodes(HttpStatusCode.OK, HttpStatusCode.BadRequest)]
        [HttpGet]
        public IHttpActionResult GetTeam()
        {
            var target = searchFacade.GetTeam();
            return Ok(target);
        }
    }
}
