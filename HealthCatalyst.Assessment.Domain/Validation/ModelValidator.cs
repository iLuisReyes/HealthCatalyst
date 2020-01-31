using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using HealthCatalyst.Assessment.Domain.Models;

namespace HealthCatalyst.Assessment.Domain.Validation
{
    public class ModelValidator
    {
        public static void Validate(object source)
        {
            var context = new ValidationContext(source, null, null);
            var results = new List<ValidationResult>();

            Validator.TryValidateObject(source, context, results, true);

            var errors = results.Where(err => err != ValidationResult.Success)?.ToList();

            if (errors?.Count > 0)
                throw new AggregatedDataValidationException(errors);
        }

        public static void Validate(IEnumerable<object> list)
        {
            foreach (var item in list)
                ModelValidator.Validate(item);
        }

        public static void Validate(Teammate teammate)
        {
            ModelValidator.Validate(teammate as object);
            var children = teammate.GetType().GetProperties().Where(x => x.GetValue(teammate) is IEntity);
            children.ToList().ForEach(x => ModelValidator.Validate(x));
        }
    }
}
