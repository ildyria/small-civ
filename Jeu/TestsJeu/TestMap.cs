using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmallWorld;

namespace TestsJeu
{
    [TestClass]
    public class TestMap
    {
        [TestMethod]
        public void MapCreate()
        {
            MapAlgoritms dmap = new DemoMap();
            //MapAlgoritms smap = new SmallMap();
            //MapAlgoritms cmap = new ClassicMap();

            Assert.AreEqual(4, dmap.NbUnitsAdvised);
            Assert.AreEqual(6, dmap.mapSize().Item1);
            Assert.AreEqual(6, dmap.mapSize().Item2);
            Assert.AreEqual(36, dmap.generateMap().Count);
            Assert.AreEqual(5, dmap.NbTurnAdvised);
        }

        [TestMethod]
        public void MapStartingPositions()
        {
            MapAlgoritms dmap = new DemoMap();
            //MapAlgoritms smap = new SmallMap();
            //MapAlgoritms cmap = new ClassicMap();
            //List<Tuple<int, int>> getStartingPositions()
            Assert.Fail();
        }
    }
}
