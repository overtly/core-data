using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Overt.User.Application
{
    public static class ModelValidationExtensions
    {
        public static bool IsValid(this object obj,out Exception exception)
        {
            exception = null;

            if(obj == null)
            {
                exception = new ArgumentNullException();
                return false;
            }

            var context = new ValidationContext(obj);
            var result = Validate(context);
            if (result == null)
                return true;

            exception = new Exception(result.ErrorMessage);
            return false;
        }

        private static ValidationResult Validate(ValidationContext context)
        {
            var properties = context.ObjectType.GetProperties();

            if (context.ObjectInstance is IValidatableObject)
            {
                IValidatableObject valid = (IValidatableObject)context.ObjectInstance;
                var validationResults = valid.Validate(context);
                if (validationResults != null && validationResults.Count() > 0)
                {
                    return valid.Validate(context).FirstOrDefault();
                }
            }

            foreach (var property in properties)
            {
                var validationAttributes = property.GetCustomAttributes(false).OfType<ValidationAttribute>();
                foreach (var attribute in validationAttributes)
                {
                    bool isValid = attribute.IsValid(property.GetValue(context.ObjectInstance));
                    if (!isValid)
                    {
                        return new ValidationResult(attribute.ErrorMessage, new[] { property.Name });
                    }
                }
            }

            return null;
        }
    }
}
