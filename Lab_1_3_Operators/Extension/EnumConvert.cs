using System;
using System.Linq;
using System.Collections.Generic;

namespace Lab_1_3_Operators
{
    public static class EnumConvert
    {
        #region Methods

        /// <summary>
        /// convert the string representation of the name or numeric value of one or
        /// more enumerated constants to an equivalent enumerated object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumValue"></param>
        /// <returns>
        /// object of type (T) whose value is represented by value.
        /// </returns>
        public static T ToEnum<T>(this string enumValue)
        {
            return (T)Enum.Parse(typeof(T), enumValue);
        }

        /// <summary>
        /// convert the string representation of the name or numeric value of one or
        /// more enumerated constants to an equivalent enumerated object. The return value indicates
        /// whether the conversion succeeded.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumValue"></param>
        /// <returns>
        /// object of type (T) whose value is represented by value. If the converting was unsuccessful return the default (T)
        /// </returns>
        public static T ToEnumButFirstlyTryParse<T>(this string enumValue) where T : struct
        {
            T tmp = default(T);
            return Enum.TryParse<T>(enumValue, true, out tmp) ? tmp : default(T);
        }

        /// <summary>
        /// convert the IEnumerable collection of strings representation of the name or numeric value of one or
        /// more enumerated constants to an equivalent enumerated object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumValue"></param>
        /// <returns>
        /// the IEnumerable object collections of type (T) whose values is represented by values.
        /// </returns>
        public static IEnumerable<T> ToEnumList<T>(this IEnumerable<string> enumValues)
        {
            ICollection<T> outputList = default(ICollection<T>);
            enumValues
                .ToList()
                .ForEach(e => outputList.Add((T)Enum.Parse(typeof(T), e)));
            return outputList;
        }

        /// <summary>
        /// convert the IEnumerable collection of strings representation of the name or numeric value of one or
        /// more enumerated constants to an equivalent enumerated object. The return value indicates
        /// whether the conversion succeeded.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumValue"></param>
        /// <returns>
        /// the IEnumerable object collections of type (T) whose values is represented by values. 
        /// If the converting was unsuccessful return the empty List collection
        /// </returns>
        public static IEnumerable<T> ToEnumListButFirstlyTryParse<T>(this IEnumerable<string> enumValues) where T : struct
        {
            T tmp = default(T);
            ICollection<T> outputList = new List<T>();
            enumValues
                .ToList()
                .ForEach(e =>
                    {
                        if (Enum.TryParse<T>(e, out tmp))
                            outputList.Add(tmp);
                    });
            return outputList;
        }

        #endregion
    }
}
