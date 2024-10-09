using MondoCore.Collections.Internal;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("MondoCore.Collections.UnitTests")]

namespace MondoCore.Collections
{
    /****************************************************************************/
    /****************************************************************************/
    /// <summary>
    /// A readonly dictionary that wraps an IDictionary
    /// </summary>
    internal abstract class NonGenericReadOnlyDictionaryWrapperBase<VOUT> : ReadOnlyDictionaryWrapperBase<string, object, VOUT?>
    {
        protected readonly IDictionary _dict;

        /****************************************************************************/
        public NonGenericReadOnlyDictionaryWrapperBase(IDictionary dict)
        {
            _dict = dict!;
        }

        public override IEnumerable<string> Keys   => _dict.Keys.ToStringList();
        public override IEnumerable<VOUT?>  Values => _dict.Values.ToList<VOUT>();
        public override int                 Count  => _dict.Count;

        /****************************************************************************/
        public override bool ContainsKey(string key)
        {
            return _dict.Contains(key);
        }
        
        /****************************************************************************/
        public override object? GetValue(string key, out bool success)
        {
            if(_dict.Contains(key))
            {
                success = true;
                return _dict[key];
            }

            success = false;
            return default;
        } 

        /****************************************************************************/
        public override IEnumerator<KeyValuePair<string, VOUT?>> GetEnumerator()        
        {
            return new MyEnumerator(_dict, this);
        }

        /****************************************************************************/
        public override IEnumerator GetEnumerableEnumerator()
        {
            return _dict.GetEnumerator();
        }

        /****************************************************************************/
        /****************************************************************************/
        private class MyEnumerator(IDictionary dict, NonGenericReadOnlyDictionaryWrapperBase<VOUT> parent) : IEnumerator<KeyValuePair<string, VOUT?>>
        {
            private readonly IDictionaryEnumerator _enum = dict.GetEnumerator();

            /****************************************************************************/
            public KeyValuePair<string, VOUT?> Current 
            {
                get
                { 
                    var current = _enum.Current;

                    if(current is DictionaryEntry entry)
                        return new KeyValuePair<string, VOUT?>(entry.Key.ToString(), parent.ConvertObject(entry.Value));

                    return new KeyValuePair<string, VOUT?>();
                }
            }

            /****************************************************************************/
            object IEnumerator.Current => _enum.Current;

            /****************************************************************************/
            public void Dispose()
            {
            }

            /****************************************************************************/
            public bool MoveNext()
            {
                return _enum.MoveNext();
            }

            /****************************************************************************/
            public void Reset()
            {
                _enum.Reset();
            }
        }
    }

    /****************************************************************************/
    /****************************************************************************/
    /// <summary>
    /// A readonly dictionary that wraps an IDictionary
    /// </summary>
    internal class NonGenericReadOnlyDictionaryWrapper : NonGenericReadOnlyDictionaryWrapperBase<object>
    {
        /****************************************************************************/
        public NonGenericReadOnlyDictionaryWrapper(IDictionary dict) : base(dict)
        {
        }

        /****************************************************************************/
        protected override object? Convert(object? val)
        {
            return val;
        }

        /****************************************************************************/
        protected override object? ConvertObject(object? val)
        {
            return val;
        }             
    }
    
   /****************************************************************************/
    /****************************************************************************/
    /// <summary>
    /// A readonly string dictionary that wraps an IDictionary
    /// </summary>
    internal class NonGenericReadOnlyStringDictionaryWrapper(IDictionary dict, bool childrenAsJson) : NonGenericReadOnlyDictionaryWrapperBase<string>(dict)
    {
        /****************************************************************************/
        protected override string? Convert(object? val)
        {
            return ConvertObject(val as object);
        }

        /****************************************************************************/
        protected override string? ConvertObject(object? val)
        {
            if(val.ConvertToString(out string? str))
                return str;

            if(childrenAsJson)
                return JsonConvert.SerializeObject(val);

            return val?.ToString();
        }
    }
}
