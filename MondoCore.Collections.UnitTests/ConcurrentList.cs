using MondoCore.Collections.Concurrent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MondoCore.Collections.UnitTests
{
    [TestClass]
    public class ConcurrentListTests
    {
        [TestMethod]
        public void ConcurrentList_Add_success()
        {
            var list = new ConcurrentList<Automobile>();

            list.Add(new Automobile { Make = "Chevy", Model = "Corvette" });
        }
        
        [TestMethod]
        public void ConcurrentList_index_op_get_success()
        {
            var list = new ConcurrentList<Automobile>();

            list.Add(new Automobile { Make = "Chevy", Model = "Corvette" });

            Assert.AreEqual("Chevy", list[0].Make);
        }
        
        [TestMethod]
        public void ConcurrentList_index_op_set_success()
        {
            var list = new ConcurrentList<Automobile>();

            list.Add(new Automobile { Make = "Chevy", Model = "Corvette" });
            list[0] = new Automobile { Make = "Audi", Model = "RS5" };

            Assert.AreEqual("Audi", list[0].Make);
        }
        
        [TestMethod]
        public void ConcurrentList_GetEnumerator_success()
        {
            var list = new ConcurrentList<Automobile>();

            list.Add(new Automobile { Make = "Chevy", Model = "Corvette" });
            list.Add(new Automobile { Make = "Audi", Model = "RS5" });

            int index = 0;

            foreach(var item in list)
            { 
                if(index++ == 0)
                    Assert.AreEqual("Chevy", item.Make);
                else if(index == 1)
                    Assert.AreEqual("Audi", item.Make);
            }
        }
        
        
        [TestMethod]
        public void ConcurrentList_Clear_Count_success()
        {
            var list = new ConcurrentList<Automobile>();

            list.Add(new Automobile { Make = "Chevy", Model = "Corvette" });
            list.Clear();

            Assert.AreEqual(0, list.Count);
        }
        
        public class Automobile
        {
            public string? Make  { get; set; }
            public string? Model { get; set; }
            public string? Color { get; set; } = "Black";
            public int     Year  { get; set; } = 1969;
        }
    }
}
