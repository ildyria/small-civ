using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmallWorld;

namespace TestsJeu
{
    [TestClass]
    public class TestGameMaker
    {
        [TestMethod]
        public void GameMakerNewCreate()
        {
            //Need to change to GameMaker
            GameMakerNew gmk = new GameMakerNew();
            gmk.setTribes(new UnitType[2] { UnitType.DWARF, UnitType.ORC });
            gmk.setNames(new string[2] { "J1", "J2" });
            gmk.setMapSize(MapSize.DEMO);
            GameManager gmg = gmk.makeGame();

            Assert.IsNotNull(gmg);
            Assert.AreEqual(1, gmg.TurnCurrent);
            Assert.AreEqual(5, gmg.TurnNumber);
            Assert.AreEqual(0, gmg.PlayerTurn);
            Unit u = gmg.getPlayer(1).unitsAt(0, 0)[0];
            Assert.IsInstanceOfType(u, typeof(Dwarf));
        }

        //Need a savemanager to be implemented
        public void GameMakerLoadCreate()
        {
            //Need to change to GameMaker
            GameMakerLoad gmk = new GameMakerLoad();
            //gmk.setSaveFile();
            GameManager gmg = gmk.makeGame();
            Assert.IsNotNull(gmg);
            Assert.AreEqual(0, gmg.TurnCurrent);
            Assert.AreEqual(10, gmg.TurnNumber);
            Assert.AreEqual(0, gmg.PlayerTurn);
            Unit u = gmg.getPlayer(1).unitsAt(0, 0)[0];
            Assert.IsInstanceOfType(u, typeof(Dwarf));
            Assert.Fail();
        }
    }
}
