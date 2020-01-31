using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace HealthCatalyst.Assessment.Domain.Extensions
{
    /// <summary>
    /// Contains extension methods of <see cref="String"/> class.
    /// </summary>
    [DebuggerStepThrough]
    public static class StringExtensions
    {
        /// <summary>
        /// Indicates whether a specified string is null, empty, or consists only of white-space characters.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <returns>true if the value is null or <see cref="String.Empty"/>, or if value consists exclusively of white-space characters.</returns>
        public static bool IsNullOrEmpty(this string value)
        {
            return String.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// Indicates whether a specified string is not null, empty, or consists only of white-space characters.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <returns>true if the value is not null or <see cref="String.Empty"/>, or if value consists exclusively of white-space characters.</returns>
        public static bool IsNotNullOrEmpty(this string value)
        {
            return !IsNullOrEmpty(value);
        }

        /// <summary>
        /// Indicates whether the string is a number or not.
        /// </summary>
        /// <param name="value">The string.</param>
        /// <returns>true if the string is a number; otherwise, false.</returns>
        public static bool IsNumeric(this string value)
        {
            double output;
            return Double.TryParse(value, out output);
        }

        /// <summary>
        /// Returns a value indicating whether the specified System.String object occurs within this string.
        /// </summary>
        /// <param name="string">The string.</param>
        /// <param name="value">The string to seek.</param>
        /// <param name="comparison">One of the enumeration values that specifies the rules for the search.</param>
        /// <returns>True if the value parameter occurs within this string, or if value is the empty string; otherwise, false.</returns>
        public static bool Contains(this string @string, string value, StringComparison comparison)
        {
            return @string.IndexOf(value, comparison) >= 0;
        }

        /// <summary>
        /// Determines whether the string not contains the string parameter or yes.
        /// </summary>
        /// <param name="string">String to evaluate</param>
        /// <param name="value">String to find in the string</param>
        /// <returns>True if the value parameter is present in the string; otherwise, false.</returns>
        public static bool NotContains(this string @string, string value)
        {
            return !@string.Contains(value);
        }

        /// <summary>
        /// Rules for removing +1 prefix
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        internal static string RemovePhonePrefix(this string phone)
        {
            if (!phone.StartsWith("+"))
                return phone;

            return phone.Replace("+1", "").Replace("+", "");
        }
    }
}
