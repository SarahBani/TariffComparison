using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Linq;

namespace Test.Common
{
    public static class Extentions
    {

        /// <summary>
        /// Get Description attribute of an enum
        /// </summary>
        /// <param name="value">the enum value</param>
        /// <returns></returns>
        public static string GetDescription(this Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            if (fieldInfo == null)
            {
                return null;
            }
            var attribute = (DescriptionAttribute)fieldInfo.GetCustomAttribute(typeof(DescriptionAttribute));
            return attribute.Description;
        }

        /// <summary>
        /// Convert a dictionary to an array with 2 elements including key and value as items
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public static object[] ToObject<T, K>(this IDictionary<T, K> dictionary) =>
            dictionary.Select(q => new object[] { q.Key, q.Value }).ToArray();

        public static string FormatAnnualCosts(this decimal value) =>
            $"{ value.ToString("0.##" )} (€/year)";

        /// <summary>
        /// Get tuple value by index
        /// </summary>
        /// <typeparam name="T">type of items inside tuple</typeparam>
        /// <param name="tuple">a tuple with 2 elements with the same type</param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static T GetValueByIndex<T>(this (T, T) tuple, int index) =>
            (T)tuple.GetType().GetFields()[index].GetValue(tuple);

    }
}
