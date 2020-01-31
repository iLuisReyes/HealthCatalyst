using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HealthCatalyst.Assessment.Domain.Models;

namespace HealthCatalyst.Assessment.Domain.Services
{
    /// <summary>
    /// An interface for managing roster events for the team
    /// </summary>
    public interface IRosterService
    {
        /// <summary>
        /// Searches for a teammate with specific value that could be found in the first and/or lastname
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        IEnumerable<Teammate> SearchTeam(string term);

        /// <summary>
        /// Add a new teammate to the team
        /// </summary>
        /// <param name="teammate"></param>
        /// <returns></returns>
        Teammate CreateTeammate(Teammate teammate);

        /// <summary>
        /// Get a specific teammate
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Teammate GetTeammate(long id);

        /// <summary>
        /// Get all teammates on a team
        /// </summary>
        /// <returns></returns>
        IEnumerable<Teammate> GetTeam();
    }
}
