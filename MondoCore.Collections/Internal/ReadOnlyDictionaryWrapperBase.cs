using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MondoCore.Collections.Internal
{
    /****************************************************************************/
    /****************************************************************************/
    /// <summary>
    /// A readonly dictionary that wraps an IDictionary
    /// </summary>
    internal abstract class ReadOnlyDictionaryWrapperBase<KEY, VIN, VOUT> : IReadOnlyDictionary<KEY, VOUT>, IKeyedValues<KEY>
    {
        /****************************************************************************/
        public ReadOnlyDictionaryWrapperBase()
        {
        }

        /****************************************************************************/
        public abstract IEnumerable<KEY>   Keys     { get; }     
        public abstract IEnumerable<VOUT>  Values   { get; }        
        public abstract int                Count    { get; }     

        public abstract object?  GetValue(KEY key, out bool success);
        public abstract bool     ContainsKey(KEY key);

        public abstract IEnumerator<KeyValuePair<KEY, VOUT>> GetEnumerator();
        public abstract IEnumerator GetEnumerableEnumerator();

        protected abstract VOUT Convert(VIN? val);
        protected abstract VOUT ConvertObject(object? val);

        /****************************************************************************/
        public VOUT this[KEY key]
        {
            get
            {
                if(TryGetValue(key, out VOUT val))
                    return val;

                throw new KeyNotFoundException($"A value with the key name of '{key!.ToString()}' was not found");
            }
        }

        /****************************************************************************/
        public virtual bool TryGetValue(KEY key, out VOUT value)
        {
            IKeyedValues<KEY> kv = this;
            var success = kv.TryGetDottedValue(key, out object? rtnVal);

            if(!success)
            {   
                value = default;
                return false;
            }

            value = ConvertObject(rtnVal);
            return true;
        }

        /****************************************************************************/
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (this as IKeyedValues<KEY>).GetEnumerableEnumerator();
        }
    }
}
