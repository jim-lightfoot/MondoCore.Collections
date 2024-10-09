using MondoCore.Collections.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MondoCore.Collections.UnitTests
{
    [TestClass]
    public class ReadOnlyDictionaryCollectionTests
    {
        [TestMethod]
        public void ReadOnlyDictionaryCollection_success()
        {
            ReadOnlyDictionaryCollection<string, string> all = new();
            
            all.Add(new Dictionary<string, string> { { "City", "San Francisco" }, { "State", "California" }});
            all.Add(new Dictionary<string, string> { { "County", "King" }, { "Street", "4th" }});

            Assert.AreEqual("San Francisco", all["City"]);
            Assert.AreEqual("4th", all["Street"]);
        }

        [TestMethod]
        public void ReadOnlyDictionaryCollection_notfound()
        {
            ReadOnlyDictionaryCollection<string, string> all = new();
            
            all.Add(new Dictionary<string, string> { { "City", "San Francisco" }, { "State", "California" }});
            all.Add(new Dictionary<string, string> { { "County", "King" }, { "Street", "4th" }});

            Assert.ThrowsException<KeyNotFoundException>( ()=> all["PhoneNumber"]);
        }

        [TestMethod]
        public void ReadOnlyDictionaryCollection_Keys_success()
        {
            ReadOnlyDictionaryCollection<string, string> all = new();
            
            all.Add(new Dictionary<string, string> { { "City", "San Francisco" }, { "State", "California" }});
            all.Add(new Dictionary<string, string> { { "County", "King" }, { "Street", "4th" }});

           var keys = all.Keys;

           Assert.AreEqual(4, keys.Count());
        }

        [TestMethod]
        public void ReadOnlyDictionaryCollection_GetEnumerator_success()
        {
            ReadOnlyDictionaryCollection<string, string> all = new();
            
            all.Add(new Dictionary<string, string> { { "City", "San Francisco" }, { "State", "California" }});
            all.Add(new Dictionary<string, string> { { "County", "King" }, { "Street", "4th" }});

            var keys = new List<string>();

            foreach(var kv in all)
            {
                keys.Add(kv.Key);
            }

           Assert.AreEqual(4, keys.Count);
           Assert.AreEqual("City", keys[0]);
           Assert.AreEqual("State", keys[1]);
           Assert.AreEqual("County", keys[2]);
           Assert.AreEqual("Street", keys[3]);
        }
    }
}
