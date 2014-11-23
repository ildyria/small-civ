using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmallWorld;

namespace TestsJeu
{
    [TestClass]
    public class TestMap
    {
        [TestMethod]
        public void TestMapCreate()
        {
            MapAlgoritms dmap = new DemoMap();
            //MapAlgoritms smap = new SmallMap();
            //MapAlgoritms cmap = new ClassicMap();

            Assert.AreEqual(4, dmap.nbUnits());
            Assert.AreEqual(6, dmap.mapSize().Item1);
            Assert.AreEqual(6, dmap.mapSize().Item2);
            Assert.AreEqual(36, dmap.generateMap().Count);
        }
    }
}
