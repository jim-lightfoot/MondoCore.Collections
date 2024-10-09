using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Text;

using MondoCore.Collections.Internal;

namespace MondoCore.Collections
{
    public static class ReadOnlyDictionaryExtensions
    {
        /// <summary>
        /// Merges the contents of two dictionaries into a single dictionary
        /// </summary>
        public static IReadOnlyDictionary<K, V> Merge<K, V>(this IReadOnlyDictionary<K, V> dict1, IReadOnlyDictionary<K, V> dict2)
        {
            if(dict1 == null)
                return dict2;

            if(dict2 == null)
                return dict1;

            if (dict1 is ReadOnlyDictionaryCollection<K, V> coll)
            {
                coll.Add(dict2);
                return dict1;
            }

            var result = new ReadOnlyDictionaryCollection<K, V>();

            result.Add(dict1);
            result.Add(dict2);

            return result;
        }
        
        /// <summary>
        /// Merges the contents of the exceptions data dictionary into a single dictionary 
        /// </summary>
        public static IReadOnlyDictionary<string, object>? MergeData(this IReadOnlyDictionary<string, object> dict, Exception ex)
        {
            if(ex.Data.Count == 0)
                return dict;
                
             dict = dict.Merge(ex.Data.ToReadOnlyDictionary()!);

            if(ex.InnerException != null)
                dict = dict.MergeData(ex.InnerException)!;

            if(ex is AggregateException aex)
            {
                foreach(var innerException in aex.InnerExceptions)
                    dict = dict.MergeData(innerException)!;
            }

            return dict;
        }    
    }
}
