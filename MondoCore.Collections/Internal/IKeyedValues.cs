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
    internal interface IKeyedValues<KEY>
    {
        object? GetValue(KEY key, out bool success);
        IEnumerator GetEnumerableEnumerator();

        #region Default Methods

        /****************************************************************************/
        public bool TryGetDottedValue(KEY key, out object? value)
        {
            var index = key!.GetType() == typeof(string) ? key.ToString().IndexOf(".") : -1;

            if(index == -1)
            { 
                var rawVal = this.GetValue(key, out bool success);

                if(!success)
                {
                    value = default;
                    return false;
                }

                value = rawVal;
                return true;

            }

            var newKey = (KEY)Convert.ChangeType(key.ToString().Substring(0, index), typeof(KEY));
            var rawVal2 = this.GetValue(newKey, out bool success2);

            if(!success2 || rawVal2 == null)
            {
                value = default;
                return false;
            }

            value = rawVal2!.GetPropertyValue(key.ToString().Substring(index+1), out bool success3);

            return success3;
        }

        #endregion
    }
}
