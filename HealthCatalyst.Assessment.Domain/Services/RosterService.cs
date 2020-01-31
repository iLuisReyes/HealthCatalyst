using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HealthCatalyst.Assessment.Domain.Models;
using HealthCatalyst.Assessment.Domain.DataAccess;
using HealthCatalyst.Assessment.Domain.Validation;
using System.Data.Entity;

namespace HealthCatalyst.Assessment.Domain.Services
{
    /// <summary>
    /// Manages roster events for the team
    /// </summary>
    public class RosterService : IRosterService
    {
        /// <summary>
        /// The db context for team rosters
        /// </summary>
        private readonly RosterContext context;

        /// <summary>
        /// Defines whether the search should match loosely (partial names) or strictly (full name).
        /// </summary>
        private readonly bool strictMatching;

        /// <summary>
        /// Parameterless Constructor for testing
        /// </summary>
        protected RosterService() { }

        /// <summary>
        /// Default Constructor for dependency injection
        /// </summary>
        /// <param name="context"></param>
        public RosterService(DbContext context, bool strictMatching)
        {
            this.context = context as RosterContext;
            this.strictMatching = strictMatching;
        }

        /// <summary>
        /// Searches for a teammate with specific value that could be found in the first and/or lastname
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public virtual IEnumerable<Teammate> SearchTeam(string term)
        {
            Guard.NotNullOrEmpty(term, "Search Term");

            IQueryable<Teammate> query;

            if (strictMatching)
                query = from player in context.Team
                        where player.FirstName.Equals(term) || player.LastName.Equals(term)
                        select player;
            else
                query = from player in context.Team
                        where player.FirstName.Contains(term) || player.LastName.Contains(term)
                        select player;

            return query.ToList<Teammate>();
        }

        /// <summary>
        /// Add a new teammate to the team
        /// </summary>
        /// <param name="teammate"></param>
        /// <returns></returns>
        public virtual Teammate CreateTeammate(Teammate teammate)
        {
            Guard.NotNull(teammate, "Teammate");

            var team = context.Set<Teammate>();
            team.Add(teammate);
            context.SaveChanges();

            return teammate;
        }

        /// <summary>
        /// Get a specific teammate
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual Teammate GetTeammate(long id)
        {
            Guard.NotNegativeOrZero((int)id, nameof(id), "A valid id was not provided");

            var query = from player in context.Team
                        where player.ID == id
                        select player;

            return query.FirstOrDefault<Teammate>();
        }

        /// <summary>
        /// Get all teammates on a team
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<Teammate> GetTeam()
        {
            var query = from player in context.Team
                        select player;

            return query.ToList<Teammate>();
        }
    }
}
