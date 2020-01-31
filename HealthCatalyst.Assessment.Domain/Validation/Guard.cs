using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using HealthCatalyst.Assessment.Domain.Extensions;

namespace HealthCatalyst.Assessment.Domain.Validation
{
    /// <summary>
    /// Contains different guard clauses to validate arguments and protect a piece of code invariants.
    /// Really useful to write defensive programming code.
    /// </summary>
    [DebuggerStepThrough]
    public static class Guard
    {
        /// <summary>
        /// Guard clause to prevent null arguments, if the provided parameter value is null an exception is thrown.
        /// </summary>
        /// <param name="parameter">The value.</param>
        /// <param name="parameterName">The parameter's name.</param>
        /// <param name="message">A custom message.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void NotNull(object parameter, string parameterName, string message = null)
        {
            if (parameter.IsNull())
            {
                throw message.IsNullOrEmpty()
                    ? new ArgumentNullException(parameterName)
                    : new ArgumentNullException(parameterName, message);
            }
        }

        /// <summary>
        /// Guard clause to prevent <see cref="String"/> values to be null, empty or contains only white spaces.
        /// </summary>
        /// <param name="parameter">The string value.</param>
        /// <param name="parameterName">The parameter's name.</param>
        /// <param name="message">A custom message.</param>
        /// <exception cref="ArgumentException"></exception>
        public static void NotNullOrEmpty(string parameter, string parameterName, string message = null)
        {
            if (parameter.IsNullOrEmpty())
            {
                throw message.IsNullOrEmpty()
                    ? new ArgumentException($"{parameterName} cannot be null or empty")
                    : new ArgumentException(message, parameterName);
            }
        }

        /// <summary>
        /// Guard clause to prevent a value to be its default value. <remarks>default(TValue)</remarks>
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="parameter">The value.</param>
        /// <param name="parameterName">The parameter's name.</param>
        /// <param name="message">A custom message.</param>
        /// <exception cref="ArgumentException"></exception>
        public static void NotDefault<TValue>(TValue parameter, string parameterName, string message = null)
        {
            if (parameter.Equals(default(TValue)))
            {
                throw message.IsNullOrEmpty()
                    ? new ArgumentException($"{parameterName} cannot be the default value")
                    : new ArgumentException(message, parameterName);
            }
        }

        /// <summary>
        /// Guard clause to prevent a value to be its default value. <remarks>default(TValue)</remarks>
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="parameter">The value.</param>
        /// <param name="parameterName">The parameter's name.</param>
        /// <param name="message">A custom message.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void NotNegativeOrZero(int parameter, string parameterName, string message = null)
        {
            if (parameter <= 0)
            {
                throw message.IsNullOrEmpty()
                        ? new ArgumentOutOfRangeException($"{parameterName} cannot be negative or zero")
                        : new ArgumentOutOfRangeException(message, parameterName);
            }
        }


        /// <summary>
        /// Guard clause to prevent a value to be its default value. <remarks>default(TValue)</remarks>
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="parameter">The value.</param>
        /// <param name="parameterName">The parameter's name.</param>
        /// <param name="message">A custom message.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void NotNegativeOrZero(long parameter, string parameterName, string message = null)
        {
            if (parameter <= 0)
            {
                throw message.IsNullOrEmpty()
                        ? new ArgumentOutOfRangeException($"{parameterName} cannot be negative or zero")
                        : new ArgumentOutOfRangeException(message, parameterName);
            }
        }

        /// <summary>
        /// Guard clause to prevent a value to be its default value. <remarks>default(TValue)</remarks>
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="parameter">The value.</param>
        /// <param name="parameterName">The parameter's name.</param>
        /// <param name="message">A custom message.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void NotNegative(int parameter, string parameterName, string message = null)
        {
            if (parameter < 0)
            {
                throw message.IsNullOrEmpty()
                        ? new ArgumentOutOfRangeException($"{parameterName} cannot be negative")
                        : new ArgumentOutOfRangeException(message, parameterName);
            }
        }


        /// <summary>
        /// Guard clause to prevent a value to be greater than a specified value.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="maximum">The maximum.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="message">The message.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void LessThan(int parameter, int maximum, string parameterName, string message = null)
        {
            if (parameter > maximum)
            {
                throw message.IsNullOrEmpty()
                        ? new ArgumentOutOfRangeException($"{parameterName} cannot be greater than {maximum}")
                        : new ArgumentOutOfRangeException(message, parameterName);
            }
        }

        /// <summary>
        /// Guard clause to prevent a value to be outside of a provided range.
        /// The range is inclusive.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="message">The message.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void Between(int parameter, int min, int max, string parameterName, string message = null)
        {
            if (parameter < min || parameter > max)
            {
                throw message.IsNullOrEmpty()
                        ? new ArgumentOutOfRangeException($"{parameterName} must be between {min} or {max}")
                        : new ArgumentOutOfRangeException(message, parameterName);
            }
        }

        public static void NotGuidEmpty(Guid parameter, string parameterName, string message = null)
        {
            if (parameter.Equals(Guid.Empty))
            {
                throw message.IsNullOrEmpty()
                    ? new ArgumentException($"{parameterName} cannot be null or empty")
                    : new ArgumentException(message, parameterName);
            }
        }
    }
}
