using System;

namespace Diploma.Behaviors
{
    /// <summary>
    /// Defines numeric validation rules.
    /// </summary>
    public static class NumericValidationRule
    {
        /// <summary>
        /// Validates string.
        /// </summary>
        /// <param name="value">String value.</param>
        /// <param name="errorText">If validation failed returns error text.</param>
        /// <returns>Is value valid.</returns>
        public static bool Validate(this string value, out string errorText)
        {
            errorText = null;

            var canConvert = Int32.TryParse(value, out var _);

            if (!canConvert)
            {
                errorText = "- Value must be numeric.";
                return false;
            }

            if ((value.Length != 5))
            {
                errorText = "- Value must be in range 10000 - 99999.";
                return false;
            }

            return true;
        }
    }
}