using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using HealthCatalyst.Assessment.Domain.Models;

namespace HealthCatalyst.Assessment.API.Models.Team
{
    /// <summary>
    /// A Create Teammate DTO object for a request to a Create a teammate 
    /// </summary>
    public class CreateTeammateRequest
    {
        /// <summary>
        /// The teammate's common first name
        /// </summary>
        [Required(ErrorMessage ="{0} is a required field.")]
        public string FirstName { get; set; }

        /// <summary>
        /// The teammate's family name
        /// </summary>
        [Required(ErrorMessage = "{0} is a required field.")]
        public string LastName { get; set; }

        /// <summary>
        /// The birthdate of this teammate
        /// </summary>
        [Required(ErrorMessage = "{0} is a required field.")]
        [Range(typeof(DateTime), "1/1/1900", "12/31/2200", ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// The teammate's height
        /// </summary>
        public string Height { get; set; }

        /// <summary>
        /// The teammate's primary position on the court
        /// </summary>
        [Required(ErrorMessage = "{0} is a required field.")]
        public string PrimaryPosition { get; set; }

        /// <summary>
        /// Indicates whether the teammate is the everyday starter 
        /// </summary>
        [Required(ErrorMessage = "{0} is a required field.")]
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

        /// <summary>
        /// Determines whether the specified object is equal to the current object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var request = obj as CreateTeammateRequest;
            if (this.GetHashCode() == request.GetHashCode())
                return true;

            return request != null &&
                   FirstName == request.FirstName &&
                   LastName == request.LastName &&
                   BirthDate == request.BirthDate &&
                   Height == request.Height &&
                   PrimaryPosition == request.PrimaryPosition &&
                   IsStarter == request.IsStarter &&
                   Interests == request.Interests &&
                   Address == request.Address &&
                   City == request.City &&
                   State == request.State &&
                   Zipcode == request.Zipcode;
        }

        /// <summary>
        /// Serves as the default hash object
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            var hashCode = 1867947502;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FirstName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LastName);
            hashCode = hashCode * -1521134295 + BirthDate.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Height);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PrimaryPosition);
            hashCode = hashCode * -1521134295 + IsStarter.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Interests);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Address);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(City);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(State);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Zipcode);
            return hashCode;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool Equals(Teammate request)
        {
            string position = request.PrimaryPosition.HasValue ? 
                Enum.GetName(request.PrimaryPosition.Value.GetType(), request.PrimaryPosition.Value) : null;

            return request != null &&
                   FirstName == request.FirstName &&
                   LastName == request.LastName &&
                   BirthDate == request.BirthDate &&
                   Height == request.Height &&
                   PrimaryPosition == position &&
                   IsStarter == request.IsStarter &&
                   Interests == request.Interests &&
                   Address == request.Address &&
                   City == request.City &&
                   State == request.State &&
                   Zipcode == request.Zipcode;
        }
    }
}