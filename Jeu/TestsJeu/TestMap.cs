using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
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
            List<int> noGoList = new List<int>();
            MapAlgoritms dmap = new DemoMap();
            List<int> map = dmap.generateMap();
            List<Tuple<int, int>> startPos = dmap.getStartingPositions();
            //MapAlgoritms smap = new SmallMap();
            //MapAlgoritms cmap = new ClassicMap();
            //List<Tuple<int, int>> getStartingPositions()
            foreach (Tuple<int, int> pos in startPos)
            {
                int posint = pos.Item2 * dmap.mapSize().Item1 + pos.Item1;
                Assert.IsTrue(posint < map.Count);
                Assert.IsTrue(posint >= 0);
                Assert.IsFalse(noGoList.Exists(i => i == map[pos.Item2 * dmap.mapSize().Item1 + pos.Item1]));
            }
            
        }
    }
}
