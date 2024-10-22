using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;
using MondoCore.Collections.Internal;
using Newtonsoft.Json;

namespace MondoCore.Collections
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Appends the values of the second dictionary onto the first
        /// </summary>
        public static IDictionary<K, V>? Append<K, V>(this IDictionary<K, V> dict1, IDictionary<K, V>? dict2)
        {
            if(dict1 != null && dict2 != null)
            {
                foreach(var kv in dict2)
                    dict1[kv.Key] = kv.Value;
            }

            return dict1;
        }

        /// <summary>
        /// Appends the values of the second dictionary onto the first
        /// </summary>
        public static IDictionary<K, V>? Append<K, V>(this IDictionary<K, V> dict1, IReadOnlyDictionary<K, V>? dict2)
        {
            if(dict1 != null && dict2 != null)
            {
                foreach(var kv in dict2)
                    dict1[kv.Key] = kv.Value;
            }

            return dict1;
        }

        /// <summary>
        /// Appends the values of the second dictionary onto the first
        /// </summary>
        public static IDictionary<string, string>? AppendStrings(this IDictionary<string, string> dict1, IReadOnlyDictionary<string, object>? dict2, bool childrenAsJson = false)
        {
            if(dict1 != null && dict2 != null)
            {
                foreach(var kv in dict2)
                    AppendValue(dict1, kv.Key, kv.Value, childrenAsJson);
            }

            return dict1;
        }

        /// <summary>
        /// Merge the two dictionaries
        /// </summary>
        public static IDictionary<K, V> Merge<K, V>(this IDictionary<K, V> dict1, IDictionary<K, V> dict2)
        {
            if(dict2 == null || dict2.Count == 0)
                return dict1;
       
            if(dict1 == null)
                return dict2;
       
            foreach(var kv in dict2)
                dict1[kv.Key] = kv.Value;

            return dict1;
        }

        /// <summary>
        /// Appends the values of the second dictionary onto the first
        /// </summary>
        private static void AppendValue(IDictionary<string, string> dict1, string prefix, object val, bool childrenAsJson)
        {
            if(val.ConvertToString(out string? strVal))
                dict1[prefix] = strVal;
            else if(childrenAsJson)
            {
                dict1[prefix] = JsonConvert.SerializeObject(val);
            }
            else
            {
                var dict2 = val.ToReadOnlyDictionary()!;

                foreach(var kv in dict2)
                    AppendValue(dict1, prefix + "." + kv.Key, kv.Value, false);
            }
        }
    }
}
