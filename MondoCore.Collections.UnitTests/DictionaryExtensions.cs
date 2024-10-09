using Newtonsoft.Json;

namespace MondoCore.Collections.UnitTests
{
    [TestClass]
    public class DictionaryExtensionsTests
    {
        [TestMethod]
        public void DictionaryExtensions_Append_null()
        {
            IDictionary<string, string> dict = new Dictionary<string, string> { {"Make","Chevy" } , { "Model", "Camaro" } };
            
            dict.Append((IDictionary<string, string>)null);

            Assert.AreEqual(2, dict.Count);
        }

        [TestMethod]
        public void DictionaryExtensions_Append_success()
        {
            IDictionary<string, string> dict1 = new Dictionary<string, string> { {"Make","Chevy" } , { "Model", "Camaro" } };
            IDictionary<string, string> dict2 = new Dictionary<string, string> { {"Color","Black" } , { "Year", "1969" } };
            
            dict1.Append(dict2);

            Assert.AreEqual(4, dict1.Count);
        }

        [TestMethod]
        public void DictionaryExtensions_Append_readonly_success()
        {
            IDictionary<string, string> dict1 = new Dictionary<string, string> { {"Make","Chevy" } , { "Model", "Camaro" } };
            IReadOnlyDictionary<string, string> dict2 = new Dictionary<string, string> { {"Color","Black" } , { "Year", "1969" } };
            
            dict1.Append(dict2);

            Assert.AreEqual(4, dict1.Count);
        }

        [TestMethod]
        public void DictionaryExtensions_AppendStrings_success()
        {
            IDictionary<string, string> dict1 = new Dictionary<string, string> { { "Model", "Camaro" } };
            var props = new {Make = "Chevy", Engine = new { Cylinders = 8, Displacement = 350, Piston = new { RodMaterial = "Chrome Moly", Material = "Stainless Steel", Diameter = 9200 } } };
            var dict2 = props.ToReadOnlyDictionary();
            
            dict1.AppendStrings(dict2);

            Assert.AreEqual(7, dict1.Count);

            Assert.AreEqual("Camaro",           dict1["Model"]);
            Assert.AreEqual("Chevy",            dict1["Make"]);
            Assert.AreEqual("8",                dict1["Engine.Cylinders"]);
            Assert.AreEqual("350",              dict1["Engine.Displacement"]);
            Assert.AreEqual("Chrome Moly",      dict1["Engine.Piston.RodMaterial"]);
            Assert.AreEqual("Stainless Steel",  dict1["Engine.Piston.Material"]);
            Assert.AreEqual("9200",             dict1["Engine.Piston.Diameter"]);
        }

        [TestMethod]
        public void DictionaryExtensions_AppendStrings_json()
        {
            IDictionary<string, string> dict1 = new Dictionary<string, string> { { "Model", "Camaro" } };
            var props = new {Make = "Chevy", Engine = new { Cylinders = 8, Displacement = 350, Piston = new { RodMaterial = "Chrome Moly", Material = "Stainless Steel", Diameter = 9200 } } };
            var dict2 = props.ToReadOnlyDictionary();
            
            dict1.AppendStrings(dict2, true);

            Assert.AreEqual(3, dict1.Count);

            Assert.AreEqual("Camaro",           dict1["Model"]);
            Assert.AreEqual("Chevy",            dict1["Make"]);

            var engine = JsonConvert.DeserializeObject<Engine>(dict1["Engine"])!;

            Assert.AreEqual(8,                  engine.Cylinders);
            Assert.AreEqual(350,                engine.Displacement);
            Assert.AreEqual("Chrome Moly",      engine.Piston!.RodMaterial);
            Assert.AreEqual("Stainless Steel",  engine.Piston.Material);
            Assert.AreEqual(9200,               engine.Piston.Diameter);
        }

        private class Engine 
        {
            public int      Cylinders       { get; set; }
            public int      Displacement    { get; set; }
            public Piston?  Piston          { get; set; }
        }

        private class Piston
        { 
            public string? RodMaterial { get; set; } 
            public string? Material    { get; set; } 
            public int     Diameter    { get; set; } 
        }
    }
}