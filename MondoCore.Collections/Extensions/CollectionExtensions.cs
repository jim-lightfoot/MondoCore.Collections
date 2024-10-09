using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

using Newtonsoft.Json;

[assembly:InternalsVisibleTo("MondoCore.Collections.UnitTests")]

namespace MondoCore.Collections
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// Translates an object into a dictionary
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static IEnumerable<string> ToStringList(this ICollection coll)
        {
            if (coll == null || coll.Count == 0)
                return Enumerable.Empty<string>();

            if (coll is IEnumerable<string> strEnum)
                return strEnum;

            var result = new List<string>();

            foreach(var item in coll)
                result.Add(item.ToString());

            return result;
        }

        /// <summary>
        /// Translates an object into a dictionary
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static IEnumerable<T> ToList<T>(this ICollection coll)
        {
            if (coll == null || coll.Count == 0)
                return Enumerable.Empty<T>();

            if (coll is IEnumerable<T> tEnum)
                return tEnum;

            var result = new List<T>();

            foreach(var item in coll)
                result.Add((T)item);

            return result;
        }
    }
}
