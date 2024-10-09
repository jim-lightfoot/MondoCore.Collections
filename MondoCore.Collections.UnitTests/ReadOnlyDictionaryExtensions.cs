using MondoCore.Collections.Internal;
using Newtonsoft.Json;

namespace MondoCore.Collections.UnitTests
{
    [TestClass]
    public class ReadOnlyDictionaryExtensionsTests
    {
        [TestMethod]
        public void ReadOnlyDictionaryExtensions_Merge_success()
        {
            var dict1 = new Dictionary<string, string> { { "City", "San Francisco" }, { "State", "California" }};
            var dict2 = new Dictionary<string, string> { { "County", "King" }, { "Street", "4th" }};
            var all = dict1.Merge(dict2);

            Assert.AreEqual("San Francisco", all["City"]);
            Assert.AreEqual("California",    all["State"]);
            Assert.AreEqual("King",          all["County"]);
            Assert.AreEqual("4th",           all["Street"]);
        }

        [TestMethod]
        public void ReadOnlyDictionaryExtensions_MergeData_success()
        {
            var dict1 = new Dictionary<string, object> { { "City", "San Francisco" }, { "State", "California" }};
            var ex = new Exception("hello");

            ex.Data.Add("County", "King");
            ex.Data.Add("Street", "4th");

            var all = (dict1 as IReadOnlyDictionary<string, object>).MergeData(ex)!;

            Assert.AreEqual("San Francisco", all["City"]);
            Assert.AreEqual("California",    all["State"]);
            Assert.AreEqual("King",          all["County"]);
            Assert.AreEqual("4th",           all["Street"]);
        }
    }
}