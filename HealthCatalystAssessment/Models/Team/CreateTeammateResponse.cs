using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthCatalyst.Assessment.API.Models.Team
{
    /// <summary>
    /// A Create Teammate DTO object for responding to a Create Teammate Request
    /// </summary>
    public class CreateTeammateResponse
    {
        /// <summary>
        /// The identifier for the teammate
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// The teammate's common first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The teammate's family name
        /// </summary>
        public string LastName { get; set; }

        public override int GetHashCode()
        {
            unchecked 
            { 
                int hash = 13;
                hash = (hash * 7) + ID.GetHashCode();
                hash = (hash * 7) + FirstName.GetHashCode();
                hash = (hash * 7) + LastName.GetHashCode();
   
                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            if (this.GetHashCode() == obj.GetHashCode())
                return true;

            var target = obj as CreateTeammateResponse;

            return (this.FirstName.Equals(target.FirstName) &&
                this.LastName.Equals(target.LastName));
        }
    }
}