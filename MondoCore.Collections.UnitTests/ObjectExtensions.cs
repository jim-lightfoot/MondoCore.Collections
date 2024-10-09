using Newtonsoft.Json;

namespace MondoCore.Collections.UnitTests
{
    [TestClass]
    public class ObjectExtensionsTests
    {
        #region ToDictionary 

        [TestMethod]
        public void ObjectExtensions_ToDictionary_null()
        {
            object? src  = null;
            var dict = src?.ToDictionary();

            Assert.IsNull(dict);
        }

        [TestMethod]
        public void ObjectExtensions_ToDictionary_sametype()
        {
            var src  = new Dictionary<string, object> { {"Color", "red" }, { "Make", "Chevy" } };
            var dict = src.ToDictionary();

            Assert.AreEqual(2,          dict.Count);
            Assert.AreEqual("red",  dict["Color"]);
            Assert.AreEqual("Chevy", dict["Make"]);
        }

        [TestMethod]
        public void ObjectExtensions_ToDictionary_diff_dict_type()
        {
            var src  = new Dictionary<string, string> { {"Color", "red" }, { "Make", "Chevy" } };
            var dict = src.ToDictionary();

            Assert.AreEqual(2,       dict.Count);
            Assert.AreEqual("red",   dict["Color"]);
            Assert.AreEqual("Chevy", dict["Make"]);
        }

        [TestMethod]
        public void ObjectExtensions_ToDictionary_diff_dict_type2()
        {
            var src  = new Dictionary<string, Automobile> { {"Chevy", new Automobile { Model = "Camaro" }  }, { "Pontiac", new Automobile { Model = "Firebird" } } };
            var dict = src.ToDictionary();

            Assert.AreEqual(2, dict.Count);
            Assert.AreEqual("Camaro",   (dict["Chevy"] as Automobile)!.Model);
            Assert.AreEqual("Firebird", (dict["Pontiac"] as Automobile)!.Model);
        }

        [TestMethod]
        public void ObjectExtensions_ToDictionary_object()
        {
            var src  = new { Make = "Chevy", Model = "Camaro" , Year = 1969 };
            var dict = src.ToDictionary();

            Assert.AreEqual(3, dict.Count);
            Assert.AreEqual("Chevy", dict["Make"]);
            Assert.AreEqual("Camaro", dict["Model"]);
            Assert.AreEqual(1969, dict["Year"]);
        }

        [TestMethod]
        public void ObjectExtensions_ToDictionary_types()
        {
            var src  = new Automobile { Make = "Chevy", Model = "Camaro" , Year = 1969 };
            var dict = src.ToDictionary();

            Assert.AreEqual(3,          dict.Count);
            Assert.AreEqual("Chevy",    dict["Make"]);
            Assert.AreEqual("Camaro",   dict["Model"]);
            Assert.AreEqual(1969,       dict["Year"]);
        }

        #endregion

        #region ToReadOnlyDictionary 

        [TestMethod]
        public void ObjectExtensions_ToReadOnlyDictionary_null()
        {
            object src  = null;
            var dict = src.ToReadOnlyDictionary();

            Assert.IsNull(dict);
        }

        [TestMethod]
        public void ObjectExtensions_ToReadOnlyDictionary_sametype()
        {
            var src  = new Dictionary<string, object> { {"Color", "red" }, { "Make", "Chevy" } };
            var dict = src.ToReadOnlyDictionary();

            Assert.AreEqual(2, dict.Count);
            Assert.AreEqual("red", dict["Color"]);
            Assert.AreEqual("Chevy", dict["Make"]);
        }

        [TestMethod]
        public void ObjectExtensions_ToReadOnlyDictionary_diff_dict_type()
        {
            var src  = new Dictionary<string, string> { {"Color", "red" }, { "Make", "Chevy" } };
            var dict = src.ToReadOnlyDictionary();

            Assert.AreEqual(2, dict.Count);
            Assert.AreEqual("red", dict["Color"]);
            Assert.AreEqual("Chevy", dict["Make"]);
        }

        [TestMethod]
        public void ObjectExtensions_ToReadOnlyDictionary_diff_dict_type2()
        {
            var src  = new Dictionary<string, Automobile> { {"Chevy", new Automobile { Model = "Camaro" }  }, { "Pontiac", new Automobile { Model = "Firebird" } } };
            var dict = src.ToReadOnlyDictionary();

            Assert.AreEqual(2, dict.Count);
            Assert.AreEqual("Camaro", (dict["Chevy"] as Automobile).Model);
            Assert.AreEqual("Firebird", (dict["Pontiac"] as Automobile).Model);
        }

        [TestMethod]
        public void ObjectExtensions_ToReadOnlyDictionary_object()
        {
            var src  = new { Make = "Chevy", Model = "Camaro" , Year = 1969 };
            var dict = src.ToReadOnlyDictionary();

            Assert.AreEqual(3, dict.Count);
            Assert.AreEqual("Chevy", dict["Make"]);
            Assert.AreEqual("Camaro", dict["Model"]);
            Assert.AreEqual(1969, dict["Year"]);
        }

        [TestMethod]
        public void ObjectExtensions_ToReadOnlyDictionary_types()
        {
            var src  = new Automobile { Make = "Chevy", Model = "Camaro" , Year = 1969 };
            var dict = src.ToReadOnlyDictionary();

            Assert.AreEqual(3, dict.Count);
            Assert.AreEqual("Chevy", dict["Make"]);
            Assert.AreEqual("Camaro", dict["Model"]);
            Assert.AreEqual(1969, dict["Year"]);
        }

        #endregion

        #region ToStringDictionary

        [TestMethod]
        public void ObjectExtensions_ToReadOnlyStringDictionary_null()
        {
            object src  = null;
            var dict = src.ToReadOnlyStringDictionary();

            Assert.IsNull(dict);
        }

        [TestMethod]
        public void ObjectExtensions_ToReadOnlyStringDictionary_sametype()
        {
            var src  = new Dictionary<string, object> { {"Color", "red" }, { "Make", "Chevy" } };
            var dict = src.ToReadOnlyStringDictionary();

            Assert.AreEqual(2,          dict!.Count);
            Assert.AreEqual("red",      dict["Color"]);
            Assert.AreEqual("Chevy",    dict["Make"]);
        }

        [TestMethod]
        public void ObjectExtensions_ToReadOnlyStringDictionary_diff_dict_type()
        {
            var src  = new Dictionary<string, string> { {"Color", "red" }, { "Make", "Chevy" } };
            var dict = src.ToReadOnlyStringDictionary();

            Assert.AreEqual(2,          dict!.Count);
            Assert.AreEqual("red",      dict["Color"]);
            Assert.AreEqual("Chevy",    dict["Make"]);
        }

        [TestMethod]
        public void ObjectExtensions_ToReadOnlyStringDictionary_diff_dict_type2()
        {
            var src  = new Dictionary<string, Automobile> { {"Chevy", new Automobile { Model = "Camaro" }  }, { "Pontiac", new Automobile { Model = "Firebird" } } };
            var dict = src.ToReadOnlyStringDictionary();

            var camaro = dict["Chevy.Model"];

            Assert.AreEqual(2,          dict!.Count);
            Assert.AreEqual("Camaro",   dict["Chevy.Model"]);
            Assert.AreEqual("Firebird", dict["Pontiac.Model"]);
        }

        [TestMethod]
        public void ObjectExtensions_ToReadOnlyStringDictionary_object()
        {
            var src  = new { Make = "Chevy", Model = "Camaro" , Year = 1969 };
            var dict = src.ToReadOnlyStringDictionary();

            Assert.AreEqual(3,          dict!.Count);
            Assert.AreEqual("Chevy",    dict["Make"]);
            Assert.AreEqual("Camaro",   dict["Model"]);
            Assert.AreEqual("1969",     dict["Year"]);
        }

        [TestMethod]
        public void ObjectExtensions_ToReadOnlyStringDictionary_types()
        {
            var src  = new Automobile { Make = "Chevy", Model = "Camaro" , Year = 1969 };
            var dict = src.ToReadOnlyStringDictionary();

            Assert.AreEqual(3,          dict.Count);
            Assert.AreEqual("Chevy",    dict["Make"]);
            Assert.AreEqual("Camaro",   dict["Model"]);
            Assert.AreEqual("1969",     dict["Year"]);
        }

        [TestMethod]
        public void ObjectExtensions_ToStringDictionary_json()
        {
            var src  = new Dictionary<string, Automobile> { {"Chevy", new Automobile { Model = "Camaro" }  }, { "Pontiac", new Automobile { Model = "Firebird" } } };
            var dict = src.ToStringDictionary();

            Assert.AreEqual(2, dict.Count);

            var chevy = JsonConvert.DeserializeObject<Automobile>(dict["Chevy"]);
            var pontiac = JsonConvert.DeserializeObject<Automobile>(dict["Pontiac"]);

            Assert.IsNotNull(chevy);
            Assert.IsNotNull(pontiac);

            Assert.AreEqual("Camaro", chevy.Model);
            Assert.AreEqual("Firebird", pontiac.Model);
        }

        [TestMethod]
        public void ObjectExtensions_ToStringDictionary_dotted()
        {
            var src  = new { Chevy = new { Model = "Camaro" }, Pontiac = new { Model = "Firebird" } };
            var dict = src.ToStringDictionary(false);

            Assert.AreEqual(2, dict.Count);

            Assert.AreEqual("Camaro", dict["Chevy.Model"]);
            Assert.AreEqual("Firebird", dict["Pontiac.Model"]);
        }

        [TestMethod]
        public void ObjectExtensions_ToStringDictionary_dotted2()
        {
            var src  = new { Chevy = new { Model = "Camaro" }, Pontiac = new { Model = "Firebird", Engine = new { Cylinders = 8, Displacement = 350 } } };
            var dict = src.ToStringDictionary(false);

            Assert.AreEqual(4, dict.Count);

            Assert.AreEqual("Camaro", dict["Chevy.Model"]);
            Assert.AreEqual("Firebird", dict["Pontiac.Model"]);
            Assert.AreEqual("8", dict["Pontiac.Engine.Cylinders"]);
            Assert.AreEqual("350", dict["Pontiac.Engine.Displacement"]);
        }

        #endregion

        #region ToStringDictionary

        [TestMethod]
        public void ObjectExtensions_ToReadOnlyStringDictionary_json()
        {
            var src  = new Dictionary<string, Automobile> { {"Chevy", new Automobile { Model = "Camaro" }  }, { "Pontiac", new Automobile { Model = "Firebird" } } };
            var dict = src.ToReadOnlyStringDictionary();

            Assert.AreEqual(2, dict!.Count);

            var chevy = JsonConvert.DeserializeObject<Automobile>(dict["Chevy"]!);
            var pontiac = JsonConvert.DeserializeObject<Automobile>(dict["Pontiac"]!);

            Assert.IsNotNull(chevy);
            Assert.IsNotNull(pontiac);

            Assert.AreEqual("Camaro", chevy.Model);
            Assert.AreEqual("Firebird", pontiac.Model);
        }

        [TestMethod]
        public void ObjectExtensions_ToReadOnlyStringDictionary_dotted()
        {
            var src  = new { Chevy = new { Model = "Camaro" }, Pontiac = new { Model = "Firebird" } };
            var dict = src.ToReadOnlyStringDictionary(false);

            Assert.AreEqual(2, dict!.Count);

            Assert.AreEqual("Camaro",   dict["Chevy.Model"]);
            Assert.AreEqual("Firebird", dict["Pontiac.Model"]);
        }

        [TestMethod]
        public void ObjectExtensions_ToReadOnlyStringDictionary_dotted2()
        {
            var src  = new { Chevy = new { Model = "Camaro" }, Pontiac = new { Model = "Firebird", Engine = new { Cylinders = 8, Displacement = 350 } } };
            var dict = src.ToReadOnlyStringDictionary(false);

            Assert.AreEqual(2, dict!.Count);

            Assert.AreEqual("Camaro",       dict["Chevy.Model"]);
            Assert.AreEqual("Firebird",     dict["Pontiac.Model"]);
            Assert.AreEqual("8",            dict["Pontiac.Engine.Cylinders"]);
            Assert.AreEqual("350",          dict["Pontiac.Engine.Displacement"]);
        }

        #endregion

        [TestMethod]
        public void ObjectExtensions_GetPropertyValue()
        {
            var obj = new { Model = "Camaro", Engine = new { Displacement = 350, Cylinders = 8 } };

            Assert.AreEqual("Camaro", obj.GetPropertyValue("Model", out bool success));
            Assert.AreEqual(350,      obj.GetPropertyValue("Engine.Displacement", out bool success2));

            var result = obj.GetPropertyValue("blah", out bool success3);

            Assert.IsFalse(success3);
        }

        [TestMethod]
        public void ObjectExtensions_GetProperties_success()
        {
            var car1  = new Automobile { Make = "Chevy", Model = "Camaro", Color = "Blue", Year = 1969 };
            var props = car1.GetProperties().ToList();

            Assert.AreEqual(4, props.Count);

            Assert.AreEqual("Make",  props[0].Name);
            Assert.AreEqual("Model",  props[1].Name);
            Assert.AreEqual("Color",  props[2].Name);
            Assert.AreEqual("Year",   props[3].Name);
                                      
            Assert.AreEqual("Chevy",  props[0].Value);
            Assert.AreEqual("Camaro", props[1].Value);
            Assert.AreEqual("Blue",   props[2].Value);
            Assert.AreEqual(1969,     props[3].Value);
        }

        public class Automobile
        {
            public string? Make  {get; set;}
            public string? Model {get; set;}
            public string? Color {get; set;}
            public int     Year  {get; set;}

            public override string ToString()
            {
                return Model!;
            }
        }

        public class Car
        {
            public string? Make      {get; set;}
            public string? Model     {get; set;}
            public string? Color     {get; set;}
            public int     Year      {get; set;}
        }

        public class Car2
        {
            public string? Make      {get; set;}
            public string? Model     {get; set;}
            public string? Color     {get; set;}
            public int     Year      {get; set;}
            public string? Engine    {get; set;}
        }

        public class Car3
        {
            public string? Make      {get; set;}
            public string? Model     {get; set;}
            public string? Color     {get; set;}
            public string? Year      {get; set;}
            public string? Engine    {get; set;}
        }
    }
}