using System;
using System.Collections.Generic;
using System.Linq;
using HealthCatalyst.Assessment.API.Formatters;
using Newtonsoft.Json;

namespace HealthCatalyst.Assessment.API.Models.Team
{
    public class GetTeammateResponse
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

        /// <summary>
        /// The currently age of this teammate
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// The birthdate of this teammate
        /// </summary>
        [JsonConverter(typeof(DateFormatConverter), "yyyy-MM-dd")]
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// The teammate's height
        /// </summary>
        public string Height { get; set; }

        /// <summary>
        /// The teammate's primary position on the court
        /// </summary>
        public string PrimaryPosition { get; set; }

        /// <summary>
        /// Indicates whether the teammate is an everyday starter 
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
    }
}