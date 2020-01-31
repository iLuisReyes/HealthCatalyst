using System;
using System.Collections.Generic;
using System.Linq;


namespace HealthCatalyst.Assessment.Domain.Models
{
    /// <summary>
    /// Represents an player on the team roster
    /// </summary>
    public class Teammate : IEntity
    {
        /// <summary>
        /// The HealthCatalyst identifier for the teammate
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

        /// <summary>
        /// The birthdate of this teammate
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// The currently known age of this teammate
        /// </summary>
        public int? Age => this.CalculateAge(this.BirthDate);

        /// <summary>
        /// The player's height
        /// </summary>
        public string Height { get; set; }

        /// <summary>
        /// Indicates the player's primary position on the court
        /// </summary>
        public Position? PrimaryPosition { get; set; }

        /// <summary>
        /// Indicates whether the player is everyday starter 
        /// </summary>
        public bool IsStarter { get; set; }

        /// <summary>
        /// The teammate's interests
        /// </summary>
        public string Interests { get; set; }

        /// <summary>
        /// The last known home street address of the teammate
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// The last known city where the teammate lived
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// The last known state where the teammate lived
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// The last known zipcode where the teammate lived
        /// </summary>
        public string Zipcode { get; set; }


        private int? CalculateAge(DateTime? birthDate)
        {
            if (birthDate.HasValue)
            {
                DateTime current = DateTime.Today;
                int age = current.Year - birthDate.Value.Year;
                if (birthDate.Value > current.AddYears(-age)) age--;
                return age;
            }
            return null;
        }

    }

    /// <summary>
    /// Indicates the position the player plays on the court
    /// </summary>
    public enum Position
    {
        PG, SG, SF, PF, C
    }
}
