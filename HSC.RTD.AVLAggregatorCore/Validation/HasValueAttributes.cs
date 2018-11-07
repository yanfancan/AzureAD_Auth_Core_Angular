using System;
using System.Globalization;
using System.ComponentModel.DataAnnotations;

namespace HSC.RTD.AVLAggregatorCore
{ 
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class HasValueAttribute : ValidationAttribute
    {
        public ValidateDirection Direction { get; set; }
        public HasValueAttribute(ValidateDirection direction = ValidateDirection.Any)
        {
            Direction = direction;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            string direction = validationContext.Items.ContainsKey("Direction") ? (string)validationContext.Items["Direction"] : "";
            switch (direction)
            {
                case "Inbound":
                    if ((Direction & ValidateDirection.Inbound) == 0)
                    {
                        return null;
                    }
                    break;
                case "Outbound":
                    if ((Direction & ValidateDirection.Outbound) == 0)
                    {
                        return null;
                    }
                    break;
                default:
                    break;
            }

            var t = value.GetType();
            if (t == typeof(int))
            {
                if ((int)value == default(int))
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }
            }
            if (t == typeof(Guid))
            {
                if ((Guid)value == default(Guid))
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }
            }
            if (t == typeof(DateTime))
            {
                if ((DateTime)value == DateTime.MinValue)
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }
            } 
            return null;
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentCulture, "The {0} field is required.", name);
        }
    }

    [Flags]
    public enum ValidateDirection
    {
        Inbound = 1,
        Outbound = 2,
        Any = 3
    }
}
