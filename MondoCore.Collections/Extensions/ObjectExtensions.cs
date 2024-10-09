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
    public static class ObjectExtensions
    {
        /// <summary>
        /// Translates an object into a dictionary
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static IDictionary<string, object> ToDictionary(this object obj)
        {
            if (obj == null)
                return null;

            if (obj is IDictionary<string, object> dict)
                return dict;

            var result = new Dictionary<string, object>();

            if(obj is IDictionary dict2)
            {
                foreach(var key in dict2.Keys)
                    result.Add(key.ToString(), dict2[key]);
            }
            else if(obj is IEnumerable list)
            {
                foreach(var val in list)
                    result.Add(val.ToString(), val);
            }
            else
            {
                var properties = obj.GetProperties();

                foreach(var property in properties)
                {
                    result.Add(property.Name, property.Value);
                }
            }

            return result;
        }

        /// <summary>
        /// Translates an object into a dictionary
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static IReadOnlyDictionary<string, object>? ToReadOnlyDictionary(this object obj)
        {
            if (obj == null)
                return null;

            if (obj is IReadOnlyDictionary<string, object> dict)
                return dict;

            if (obj is IDictionary<string, object> sdict)
                return new GenericReadOnlyDictionaryWrapper<object>(sdict);

            if (obj is IDictionary<string, string> sdict2)
                return new GenericReadOnlyDictionaryWrapper<string>(sdict2);

            if(obj is IDictionary dict2)
                return new NonGenericReadOnlyDictionaryWrapper(dict2);
            
            var result = new Dictionary<string, object>();

            if(obj is IEnumerable list)
            {
                foreach(var val in list)
                    result.Add(val.ToString(), val);
            }
            else
            {
                var properties = obj.GetProperties();

                foreach(var property in properties)
                    result.Add(property.Name, property.Value);
            }

            return result;
        }

        /// <summary>
        /// Translates an object into a string dictionary
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static IDictionary<string, string>? ToStringDictionary(this object obj, bool childrenAsJson = true)
        {
            if (obj == null)
                return null;

            if (obj is IDictionary<string, string> dict)
                return dict;

            var result = new Dictionary<string, string>();

            AppendValue(result, "", obj, childrenAsJson);

            return result;
        }

        /// <summary>
        /// Translates an object into a dictionary
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static IReadOnlyDictionary<string, string?>? ToReadOnlyStringDictionary(this object obj, bool childrenAsJson = true)
        {
            if (obj == null)
                return null;

            if (obj is IReadOnlyDictionary<string, string?> dict)
                return dict;

            if (obj is IReadOnlyDictionary<string, string> dictro)
                return dictro;

            if (obj is IReadOnlyDictionary<string, object> objDict)
                return new ReadOnlyReadOnlyStringDictionaryWrapper<object>(objDict);

            if (obj is IDictionary<string, object> sdict)
                return new GenericReadOnlyStringDictionaryWrapper<object>(sdict, childrenAsJson);

            if (obj is IDictionary<string, string> sdict2)
                return new GenericReadOnlyStringDictionaryWrapper<string>(sdict2, childrenAsJson);

            if(obj is IDictionary dict2)
                return new NonGenericReadOnlyStringDictionaryWrapper(dict2, childrenAsJson);
            
            var result = new Dictionary<string, object>();

            if(obj is IEnumerable list)
            {
                foreach(var val in list)
                    result.Add(val.ToString(), val);
            }
            else
            {
                var properties = obj.GetProperties();

                foreach(var property in properties)
                    result.Add(property.Name, property.Value);
            }

            return new GenericReadOnlyStringDictionaryWrapper<object>(result, childrenAsJson);
        }

        /// <summary>
        /// Creates an enumerable from the public properties of an object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>An enumerable to iterate over the list of properties</returns>
        public static IEnumerable<(string Name, object Value)> GetProperties(this object obj)
        {
            // Get public instance properties only
            var properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where( p=> p.CanRead );

            foreach(var property in properties)
            {
                object? value = null;

                try
                { 
                    value = property.GetValue(obj);
                }
                catch
                { 
                    // Just ignore it
                }

                // Add property name and value to dictionary
                if (value != null)
                    yield return (property.Name, value);
            }
        }
        
        /// <summary>
        /// Converts an object to a serializable string
        /// </summary>
        /// <param name="val">The value to convert</param>
        /// <param name="str">The result string</param>
        /// <returns></returns>
        public static bool ConvertToString(this object? val, out string? str)
        {
            if(val == null)
            { 
                str = null;
                return true;
            }

            if(val is DateTime dtVal)
            {
                str = dtVal.ToString("s");
                return true;
            }

            if(val is DateTimeOffset dtoVal)
            {
                str = dtoVal.ToString("s");
                return true;
            }

            if(val!.GetType().IsPrimitive || val is string)
            {
                str = val.ToString();
                return true;
            }

            str = null;
            return false;
        }

        /****************************************************************************/
        /// <summary>
        /// Get the "dotted" value of an object
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        internal static object? GetPropertyValue(this object obj, string propertyName, out bool success)
        {
            var type = obj.GetType();

            var index = propertyName.IndexOf(".");

            if(index == -1)
            {
                var property = type.GetProperty(propertyName);

                if(property == null)
                { 
                    success = false;
                    return null;
                }

                success = true;
                return property.GetValue(obj);
            }

            var firstName = propertyName.Substring(0, index);
            var firstProp = type.GetProperty(firstName);

            if(firstProp == null)
            { 
                success = false;
                return null;
            }

            success = true;

            var val = firstProp.GetValue(obj);

            if(val == null)
                return null;

            return val.GetPropertyValue(propertyName.Substring(index+1), out success);

        }

        #region Private 

        private static void AppendValue(IDictionary<string, string> dict, string prefix, object obj, bool childrenAsJson)
        {
            if (obj == null)
                return;

            var objDict = obj.ToDictionary();

            foreach(var key in objDict.Keys)
            { 
                var val = objDict[key];

                if(val is DateTime dtVal)
                {
                    dict.Add(prefix + key, dtVal.ToString("s"));
                    continue;
                }

                if(val is DateTimeOffset dtoVal)
                {
                    dict.Add(prefix + key, dtoVal.ToString("s"));
                    continue;
                }

                if(val.GetType().IsPrimitive || val is string)
                {
                    dict.Add(prefix + key, val.ToString());
                    continue;
                }

                if(childrenAsJson)
                { 
                    var json = JsonConvert.SerializeObject(val);

                    dict.Add(prefix + key, json);

                    continue;
                }

                AppendValue(dict, prefix + key + ".", val, false);
            }

            return;
        }

        #endregion
    }
}
