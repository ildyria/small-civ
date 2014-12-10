using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmallWorld;
using System.Collections;
using System.Collections.Generic;

namespace TestsJeu
{
    [TestClass]
    public class TestUnite
    {
        [TestMethod]
        public void UnitAndFactoryDwarf()
        {
            DwarfFactory df = new DwarfFactory();
            Unit d = df.makeUnit();
            Assert.IsInstanceOfType(d, typeof(Dwarf));
            Plain p = new Plain();

            d.damage();
            Assert.AreEqual(4, d.Life);
            Assert.AreEqual(0, d.scorePoints(p));
            Assert.AreEqual(1, d.moveCost(0, 1, p));
        }

        [TestMethod]
        public void UnitAndFactoryElf()
        {
            ElfFactory df = new ElfFactory();
            Unit d = df.makeUnit();
            Assert.IsInstanceOfType(d, typeof(Elf));
            Tile t1 = new Desert();
            Tile t2 = new Forest();

            d.damage();
            Assert.AreEqual(4, d.Life);
            Assert.AreEqual(4, d.moveCost(0, 1, t1));
            Assert.AreEqual(1, d.moveCost(0, 1, t2));
        }

        [TestMethod]
        public void UnitAndFactoryOrc()
        {
            OrcFactory df = new OrcFactory();
            Unit d = df.makeUnit();
            Assert.IsInstanceOfType(d, typeof(Orc));
            Plain p = new Plain();
            Forest f = new Forest();

            d.damage();
            Assert.AreEqual(4, d.Life);
            Assert.AreEqual(1, d.moveCost(0, 1, p));
            Assert.AreEqual(0, d.scorePoints(f));
        }
        [TestMethod]
        public void CombatRound()
        {
            OrcFactory of = new OrcFactory();
            DwarfFactory df = new DwarfFactory();
            Unit orc = of.makeUnit();
            Unit dw = df.makeUnit();
            orc.setPosition(1, 1);
            Assert.AreEqual(10, orc.Life + dw.Life);
            orc.fightRound(dw);
            Assert.AreEqual(9, orc.Life + dw.Life);
        }
        
        [TestMethod]
        public void CombatKill()
        {
            // Should use a loaded game
            GameMakerNew gmn = new GameMakerNew();
            gmn.setTribes(new UnitType[2] { UnitType.DWARF, UnitType.ORC });
            gmn.setNames(new string[2] { "J1", "J2" });
            gmn.setMapSize(MapSize.DEMO);
            gmn.makeGame();
            OrcFactory of = new OrcFactory();
            DwarfFactory df = new DwarfFactory();
            Unit orc = of.makeUnit();
            Unit dw = df.makeUnit();
            orc.setPosition(0, 1);
            dw.damage();
            dw.damage();
            dw.damage();
            dw.damage();

            Assert.AreEqual(6, orc.Life + dw.Life);
            orc.fight(dw);
            //if we have no luck, the test fail ...
            Assert.AreEqual(0, dw.Life);
        }
        [TestMethod]
        public void BasicMoveOddY()
        {
            // Should use a loaded game
            GameMakerNew gmn = new GameMakerNew();
            gmn.setTribes(new UnitType[2] { UnitType.DWARF, UnitType.ORC });
            gmn.setNames(new string[2] { "J1", "J2" });
            gmn.setMapSize(MapSize.DEMO);
            gmn.makeGame();
            Tile t = new Plain();
            //all test above concerning move should be also there ...
            GameManager.Instance().getCurrentPlayer().getUnits()[0].setPosition(1,1);
            
            Assert.AreEqual(Unit.IMPOSSIBLE_MOVE, GameManager.Instance().getCurrentPlayer().getUnits()[0].moveCost(0, 0, t));

            Assert.AreNotEqual(Unit.IMPOSSIBLE_MOVE, GameManager.Instance().getCurrentPlayer().getUnits()[0].moveCost(0, 1, t));
            Assert.AreEqual(Unit.IMPOSSIBLE_MOVE, GameManager.Instance().getCurrentPlayer().getUnits()[0].moveCost(0, 2, t));
            Assert.AreNotEqual(Unit.IMPOSSIBLE_MOVE, GameManager.Instance().getCurrentPlayer().getUnits()[0].moveCost(1, 0, t));
            Assert.AreNotEqual(Unit.IMPOSSIBLE_MOVE, GameManager.Instance().getCurrentPlayer().getUnits()[0].moveCost(1, 1, t));
            Assert.AreNotEqual(Unit.IMPOSSIBLE_MOVE, GameManager.Instance().getCurrentPlayer().getUnits()[0].moveCost(1, 2, t));
            Assert.AreNotEqual(Unit.IMPOSSIBLE_MOVE, GameManager.Instance().getCurrentPlayer().getUnits()[0].moveCost(2, 0, t));
            Assert.AreNotEqual(Unit.IMPOSSIBLE_MOVE, GameManager.Instance().getCurrentPlayer().getUnits()[0].moveCost(2, 1, t));
            Assert.AreNotEqual(Unit.IMPOSSIBLE_MOVE, GameManager.Instance().getCurrentPlayer().getUnits()[0].moveCost(2, 2, t));
        }

        [TestMethod]
        public void BasicMoveEvenY()
        {
            // Should use a loaded game
            GameMakerNew gmn = new GameMakerNew();
            gmn.setTribes(new UnitType[2] { UnitType.ORC, UnitType.DWARF });
            gmn.setNames(new string[2] { "J1", "J2" });
            gmn.setMapSize(MapSize.DEMO);
            gmn.makeGame();
            Tile t = new Plain();
            //all test above concerning move should be also there ...
            GameManager.Instance().getCurrentPlayer().getUnits()[0].setPosition(1, 2);
            Assert.AreNotEqual(Unit.IMPOSSIBLE_MOVE, GameManager.Instance().getCurrentPlayer().getUnits()[0].moveCost(0, 1, t));
            Assert.AreNotEqual(Unit.IMPOSSIBLE_MOVE, GameManager.Instance().getCurrentPlayer().getUnits()[0].moveCost(0, 2, t));
            Assert.AreNotEqual(Unit.IMPOSSIBLE_MOVE, GameManager.Instance().getCurrentPlayer().getUnits()[0].moveCost(0, 3, t));
            Assert.AreNotEqual(Unit.IMPOSSIBLE_MOVE, GameManager.Instance().getCurrentPlayer().getUnits()[0].moveCost(1, 1, t));
            Assert.AreNotEqual(Unit.IMPOSSIBLE_MOVE, GameManager.Instance().getCurrentPlayer().getUnits()[0].moveCost(1, 2, t));
            Assert.AreNotEqual(Unit.IMPOSSIBLE_MOVE, GameManager.Instance().getCurrentPlayer().getUnits()[0].moveCost(1, 3, t));
            Assert.AreEqual(Unit.IMPOSSIBLE_MOVE, GameManager.Instance().getCurrentPlayer().getUnits()[0].moveCost(2, 1, t));
            Assert.AreNotEqual(Unit.IMPOSSIBLE_MOVE, GameManager.Instance().getCurrentPlayer().getUnits()[0].moveCost(2, 2, t));
            Assert.AreEqual(Unit.IMPOSSIBLE_MOVE, GameManager.Instance().getCurrentPlayer().getUnits()[0].moveCost(2, 3, t));
        }

        [TestMethod]
        public void MountainTeleportDwarfs()
        {
            // Should use a loaded game
            GameMakerNew gmn = new GameMakerNew();
            gmn.setTribes(new UnitType[2] { UnitType.DWARF, UnitType.ORC });
            gmn.setNames(new string[2] { "J1", "J2" });
            gmn.setMapSize(MapSize.DEMO);
            gmn.makeGame();
            //it fail cuz no ameManager is instanciated
            DwarfFactory df = new DwarfFactory();
            Unit dw1 = df.makeUnit();  
            dw1.setPosition(0, 0); // mountain
            Unit dw2 = df.makeUnit();
            dw2.setPosition(0, 1); //plain
            Mountain m = new Mountain();
            Plain p = new Plain();
            List<int> tilelist = new List<int>() {
                (int) TerrainType.MOUNTAIN,  (int) TerrainType.PLAIN, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0
            };
            GameManager.Instance().setMap(new GameMap(6, 6, tilelist));
            

            Assert.AreEqual(Unit.IMPOSSIBLE_MOVE, dw1.moveCost(4, 4, p));
            Assert.AreNotEqual(Unit.IMPOSSIBLE_MOVE, dw1.moveCost(4, 4, m));
            Assert.AreEqual(Unit.IMPOSSIBLE_MOVE, dw2.moveCost(4, 4, p));
            Assert.AreEqual(Unit.IMPOSSIBLE_MOVE, dw2.moveCost(4, 4, m));

            // there is an enemy on the mountain
            GameManager.Instance().opponent().getUnits()[0].setPosition(5, 5);
            Assert.AreEqual(Unit.IMPOSSIBLE_MOVE, dw1.moveCost(5, 5, p));
            Assert.AreEqual(Unit.IMPOSSIBLE_MOVE, dw1.moveCost(5, 5, m));
            Assert.AreEqual(Unit.IMPOSSIBLE_MOVE, dw2.moveCost(5, 5, p));
            Assert.AreEqual(Unit.IMPOSSIBLE_MOVE, dw2.moveCost(5, 5, m));
        }
        [TestMethod]
        public void SurroundedDyingElf()
        {
            Assert.Fail();
        }
    }
}
