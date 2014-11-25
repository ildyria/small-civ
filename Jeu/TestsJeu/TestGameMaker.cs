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
            string[] names = new string[2] { "J1", "J2" };
            UnitType[] tribes = new UnitType[2] { UnitType.DWARF, UnitType.ORC };
            gmk.setTribes(tribes);
            gmk.setNames(names);
            GameManager gmg = gmk.makeGame();
            Assert.IsNotNull(gmg);
            Assert.AreEqual(0, gmg.getTurnCurrent());
            Assert.AreEqual(10, gmg.getTurnNumber());
            Assert.AreEqual(0, gmg.getPlayerTurn());
            Unit u = gmg.getPlayer1().unitsAt(0, 0)[0];
            Assert.IsInstanceOfType(u, typeof(Dwarf));
        }

        //Need a savemanager to be implemented
        public void GameMakerLoadCreate()
        {
            //Need to change to GameMaker
            GameMakerLoad gmk = new GameMakerLoad();
            GameManager gmg = gmk.makeGame();
            Assert.IsNotNull(gmg);
            Assert.AreEqual(0, gmg.getTurnCurrent());
            Assert.AreEqual(10, gmg.getTurnNumber());
            Assert.AreEqual(0, gmg.getPlayerTurn());
            Unit u = gmg.getPlayer1().unitsAt(0, 0)[0];
            Assert.IsInstanceOfType(u, typeof(Dwarf));
            Assert.Fail();
        }
    }
}
