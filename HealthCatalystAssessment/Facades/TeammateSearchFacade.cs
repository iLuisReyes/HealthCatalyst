using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using HealthCatalyst.Assessment.API.Models.Team;
using HealthCatalyst.Assessment.Domain.Services;
using HealthCatalyst.Assessment.Domain.Models;
using AutoMapper;


namespace HealthCatalyst.Assessment.API.Facades
{
    /// <summary>
    /// A facade for the People API, responsible primarily for transformation of DTOs between the controller and the domain service.
    /// </summary>
    public class TeammateSearchFacade
    {
        /// <summary>
        /// Manages the roster
        /// </summary>
        private readonly IRosterService service;

        /// <summary>
        /// Maps dtos and entities
        /// </summary>
        private readonly IMapper mapper;

        /// <summary>
        /// Parameterless constructor to support Mocks.
        /// </summary>
        public TeammateSearchFacade() { }

        /// <summary>
        /// Default constructor for dependency injection
        /// </summary>
        /// <param name="service">A service to retreive from the roster domain</param>
        /// <param name="mapper">A DTO-Entity mapper</param>
        public TeammateSearchFacade(IRosterService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        /// <summary>
        /// Searches for a teammate with specific value that could be found in the first and/or lastname
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public virtual IEnumerable<GetTeammateResponse> SearchTeam(string term)
        {
            var list = service.SearchTeam(term);
            if (list.Count() <= 0)
                return null;
            
            return list.Select<Teammate, GetTeammateResponse>(
                player => mapper.Map<Teammate, GetTeammateResponse>(player)
                );
        }

        /// <summary>
        /// Add a new teammate to the team
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual CreateTeammateResponse CreateTeammate(CreateTeammateRequest request)
        {
            var source = mapper.Map<CreateTeammateRequest, Teammate>(request);
            var teammate = service.CreateTeammate(source);
            return mapper.Map<Teammate, CreateTeammateResponse>(teammate);
        }

        /// <summary>
        /// Get a specific teammate
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual GetTeammateResponse GetTeammate(long id)
        {            
            var teammate = service.GetTeammate(id);
            return mapper.Map<Teammate, GetTeammateResponse>(teammate);
        }

        /// <summary>
        /// Get all teammates on a team
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<GetTeammateResponse> GetTeam()
        {
            return service.GetTeam().Select<Teammate, GetTeammateResponse>(
                player => mapper.Map<Teammate, GetTeammateResponse>(player)
                );
        }
    }
}