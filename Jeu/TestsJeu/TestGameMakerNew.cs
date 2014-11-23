using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmallWorld;

namespace TestsJeu
{
    [TestClass]
    public class TestGameMakerNew
    {
        [TestMethod]
        public void TestGameMakerNewCreate()
        {
            //Need to change to GameMaker
            GameMakerNew gmk = new GameMakerNew();
            string[] names = new string[2] { "J1", "J2" };
            UnitType[] tribes = new UnitType[2] { UnitType.DWARF, UnitType.ORC };
            gmk.setTribes(tribes);
            gmk.setNames(names);
            GameManager gmg = gmk.makeGame();
            Unit u = gmg.getPlayer1().unitsAt(0, 0)[0];
            Assert.IsNotNull(gmg);
            Assert.AreEqual(10, gmg.getTurn());
            Assert.AreEqual(0, gmg.getPlayerTurn());
            Assert.IsInstanceOfType(u, typeof(Dwarf));
        }
    }
}
