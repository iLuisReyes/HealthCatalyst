using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace HealthCatalyst.Assessment.Domain
{
    public class AggregatedDataValidationException : ValidationException
    {
        public IEnumerable<ValidationResult> Errors { get; set; }

        public AggregatedDataValidationException() : base("One or more validation errors were found prior to saving your data.")
        {
            this.Errors = new List<ValidationResult>();
        }

        public AggregatedDataValidationException(IEnumerable<ValidationResult> errors) : base("One or more validation errors were found prior to saving your data.")
        {
            this.Errors = errors;
        }

        public AggregatedDataValidationException(string msg) : base(msg) { }

        public AggregatedDataValidationException(string msg, Exception InnerException)
            : base(msg, InnerException) { }

        public AggregatedDataValidationException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
