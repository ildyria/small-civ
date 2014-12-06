﻿using System;
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

            Assert.AreEqual(4, dmap.getNbUnitsAdvised());
            Assert.AreEqual(6, dmap.mapSize().Item1);
            Assert.AreEqual(6, dmap.mapSize().Item2);
            Assert.AreEqual(36, dmap.generateMap().Count);
        }
    }
}
