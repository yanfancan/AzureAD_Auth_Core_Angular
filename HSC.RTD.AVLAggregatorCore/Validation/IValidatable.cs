using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace HSC.RTD.AVLAggregatorCore
{
    public interface IValidatable
    {
    }

    public static class ValidateExtension
    {
        public static void ValidateInbound(this IValidatable source)
        {
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(source, new ValidationContext(source, null, new Dictionary<object, object>() { { "Direction", "Inbound" } }), validationResults, true))
            {
                throw new ValidationException(string.Join(Environment.NewLine, validationResults.Select(x => x.ErrorMessage).ToArray()));
            }
        }

        public static void ValidateOutbound(this IValidatable source)
        {
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(source, new ValidationContext(source, null, new Dictionary<object, object>() { { "Direction", "Outbound" } }), validationResults, true))
            {
                throw new ValidationException(string.Join(Environment.NewLine, validationResults.Select(x => x.ErrorMessage).ToArray()));
            }
        }
    }
}
