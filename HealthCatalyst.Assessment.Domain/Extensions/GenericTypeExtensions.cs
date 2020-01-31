using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace HealthCatalyst.Assessment.Domain.Extensions
{
    /// <summary>
    /// Contains extension methods of generic type.
    /// </summary>
    [DebuggerStepThrough]
    public static class GenericTypeExtensions
    {
        /// <summary>
        /// Determines whether the object is null using the <see cref="Object.ReferenceEquals"/> static method of <see cref="Object"/> class
        /// to avoid using an overwritten operator logic.
        /// </summary>
        /// <typeparam name="TObject">The object type.</typeparam>
        /// <param name="object">The object.</param>
        /// <returns>true if the object is null; otherwise, false.</returns>
        public static bool IsNull<TObject>(this TObject @object)
         where TObject : class
        {
            return Object.ReferenceEquals(@object, null);
        }

        /// <summary>
        /// Determines whether the object is not null using the <see cref="Object.ReferenceEquals"/> static method of <see cref="Object"/> class
        /// to avoid using an overwritten operator logic.
        /// </summary>
        /// <typeparam name="TObject">The object type.</typeparam>
        /// <param name="object">The object.</param>
        /// <returns>true if the object is not null; otherwise, false.</returns>
        public static bool IsNotNull<TObject>(this TObject @object)
            where TObject : class
        {
            return !IsNull(@object);
        }

        /// <summary>
        /// Determines whether the struct value is equals to its defaul value or not.
        /// </summary>
        /// <remarks>
        /// The default value is determined by default(TValue), where TValue is the struct type
        /// </remarks>
        /// <typeparam name="TValue">The struct type.</typeparam>
        /// <param name="value">The struct.</param>
        /// <returns>true if the struct value is equal to its default value; otherwise, false.</returns>
        public static bool IsDefault<TValue>(this TValue value)
            where TValue : struct
        {
            return value.Equals(default(TValue));
        }


        /// <summary>
        /// A delegate manager that delays throwing of any exceptions until after all items in the list have executed.  The exception are then collected
        /// and thrown as an AggregateException 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="list"></param>
        /// <param name="fn"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> DelayedThrow<TSource>(this IEnumerable<TSource> list, Action<TSource> fn)
        {
            var exceptions = new List<Exception>();
            var processed = new List<TSource>();

            foreach (var item in list)
            {
                try
                {
                    fn(item);
                    processed.Add(item);
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            }

            if (exceptions.Count > 0)
                throw new AggregateException(exceptions);

            return processed;
        }
        
    }
}
