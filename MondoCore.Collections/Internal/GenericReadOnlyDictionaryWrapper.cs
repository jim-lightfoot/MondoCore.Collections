using MondoCore.Collections.Internal;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("MondoCore.Collections.UnitTests")]

namespace MondoCore.Collections
{
    /****************************************************************************/
    /****************************************************************************/
    /// <summary>
    /// A readonly dictionary that wraps a generic IDictionary
    /// </summary>
    internal abstract class GenericReadOnlyDictionaryWrapperBase<VIN, VOUT> : ReadOnlyDictionaryWrapperBase<string, VIN, VOUT>
    {
        protected readonly IDictionary<string, VIN> _dict;

        /****************************************************************************/
        public GenericReadOnlyDictionaryWrapperBase(IDictionary<string, VIN> dict)
        {
            _dict = dict!;
        }

        /****************************************************************************/
        public override IEnumerable<string> Keys   => _dict.Keys.ToArray();
        public override IEnumerable<VOUT?>  Values => _dict.Values.Select( i => Convert(i));
        public override int                 Count  => _dict.Count;

        /****************************************************************************/
        public override bool ContainsKey(string key)
        {
            return _dict.ContainsKey(key);
        }
        
        /****************************************************************************/
        public override object? GetValue(string key, out bool success)
        {
            if(_dict.TryGetValue(key, out VIN value))
            {
                success = true;
                return value;
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
        private class MyEnumerator(IDictionary<string, VIN> dict, GenericReadOnlyDictionaryWrapperBase<VIN, VOUT> parent) : IEnumerator<KeyValuePair<string, VOUT?>>
        {
            private readonly IEnumerator<KeyValuePair<string, VIN>> _enum = dict.GetEnumerator();

            /****************************************************************************/
            public KeyValuePair<string, VOUT?> Current 
            {
                get
                { 
                    var current = _enum.Current;

                    if(current is DictionaryEntry entry)
                        return new KeyValuePair<string, VOUT?>(entry.Key.ToString(), parent.ConvertObject(entry.Value));

                    if(current is KeyValuePair<string, VOUT?> currTyped)
                        return currTyped;

                    if(current is KeyValuePair<string, VIN> kv)
                        return new KeyValuePair<string, VOUT?>(kv.Key, parent.Convert(kv.Value));

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
    /// A readonly dictionary that wraps a generic IDictionary
    /// </summary>
    internal class GenericReadOnlyDictionaryWrapper<VIN> : GenericReadOnlyDictionaryWrapperBase<VIN, object>
    {
        /****************************************************************************/
        public GenericReadOnlyDictionaryWrapper(IDictionary<string, VIN> dict) : base(dict)
        {
        }

        /****************************************************************************/
        protected override object? Convert(VIN? val)
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
    /// A readonly dictionary that wraps an IDictionary
    /// </summary>
    internal class GenericReadOnlyStringDictionaryWrapper<VIN>(IDictionary<string, VIN> dict, bool childrenAsJson) : GenericReadOnlyDictionaryWrapperBase<VIN, string>(dict)
    {
        /****************************************************************************/
        protected override string? Convert(VIN? val)
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
