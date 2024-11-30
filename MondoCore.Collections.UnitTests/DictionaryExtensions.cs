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
        public void DictionaryExtensions_AppendStrings_wTransform_Success()
        {
            var make  = "Chevy";
            var model = "Camaro";
            var color = "Black";

            var props = new { make, model, color };
            var dict1 = new Dictionary<string, string>();
            var dict2 = props.ToReadOnlyDictionary();
            
            dict1.AppendStrings(dict2, transformKey: (name)=> name?.Capitalize() ?? "");

            Assert.AreEqual(3, dict1.Count);

            Assert.AreEqual("Camaro",  dict1["Model"]);
            Assert.AreEqual("Chevy",   dict1["Make"]);
            Assert.AreEqual("Black",   dict1["Color"]);
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

        [TestMethod]
        public void DictionaryExtensions_Merge()
        {
            IDictionary<string, string> dict1  = new Dictionary<string, string> { { "Make", "Chevy" }, { "Model", "Camaro" } };
            IDictionary<string, string> dict2  = new Dictionary<string, string> { { "Year", "1969" },  { "Color", "Blue" } };
            var result = dict1!.Merge(dict2);
            
            Assert.AreEqual(4,        result.Count());
            Assert.AreEqual("Chevy",  result["Make"]);
            Assert.AreEqual("Camaro", result["Model"]);
            Assert.AreEqual("1969",   result["Year"]);
            Assert.AreEqual("Blue",   result["Color"]);
        }

        [TestMethod]
        public void DictionaryExtensions_Merge_overlap()
        {
            IDictionary<string, string> dict1  = new Dictionary<string, string> { { "Make", "Chevy" }, { "Model", "Camaro" }, { "Year", "1970" } };
            IDictionary<string, string> dict2  = new Dictionary<string, string> { { "Year", "1969" },  { "Color", "Blue" } };
            var result = dict1.Merge(dict2);
            
            Assert.AreEqual(4,        result.Count);
            Assert.AreEqual("Chevy",  result["Make"]);
            Assert.AreEqual("Camaro", result["Model"]);
            Assert.AreEqual("1969",   result["Year"]);
            Assert.AreEqual("Blue",   result["Color"]);
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

    public static class Extensions
    {
        public static string Capitalize(this string? str)
        {
            if(string.IsNullOrWhiteSpace(str))
                return "";

            if(str.Length == 1)
                return str.ToUpper();

            return str.Substring(0, 1).ToUpper() + str.Substring(1);
        }
    }

}