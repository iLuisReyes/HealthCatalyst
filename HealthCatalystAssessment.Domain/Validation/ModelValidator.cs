using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace HealthCatalyst.Assessment.Domain
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

        public static void Validate(Enrollment enrollment)
        {
            ModelValidator.Validate(enrollment as object);
            var children = enrollment.GetType().GetProperties().Where(x => x.GetValue(enrollment) is AbstractDbObject);
            children.ToList().ForEach(x => ModelValidator.Validate(x));
        }
    }
}
