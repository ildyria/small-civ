using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmallWorld;

namespace TestsJeu
{
    [TestClass]
    public class TestUnite
    {
        [TestMethod]
        public void TestUnitAndFactoryDwarf()
        {
            DwarfFactory df = new DwarfFactory();
            Unit d = df.makeUnit();
            Assert.IsInstanceOfType(d, typeof(Dwarf));
            Plain p = new Plain();

            d.damage();
            Assert.AreEqual(4, d.getLife());
            Assert.AreEqual(0, d.scorePoints(p));
            Assert.AreEqual(1, d.moveCost(1, 1, p));
        }

        [TestMethod]
        public void TestUnitAndFactoryElf()
        {
            ElfFactory df = new ElfFactory();
            Unit d = df.makeUnit();
            Assert.IsInstanceOfType(d, typeof(Elf));
            Tile t1 = new Desert();
            Tile t2 = new Forest();

            d.damage();
            Assert.AreEqual(4, d.getLife());
            Assert.AreEqual(4, d.moveCost(1, 1, t1));
            Assert.AreEqual(1, d.moveCost(1, 1, t2));
        }

        [TestMethod]
        public void TestUnitAndFactoryOrc()
        {
            OrcFactory df = new OrcFactory();
            Unit d = df.makeUnit();
            Assert.IsInstanceOfType(d, typeof(Orc));
            Plain p = new Plain();
            Forest f = new Forest();

            d.damage();
            Assert.AreEqual(4, d.getLife());
            Assert.AreEqual(1, d.moveCost(1, 1, p));
            Assert.AreEqual(0, d.scorePoints(f));
        }
        [TestMethod]
        public void TestCombatRound()
        {
            OrcFactory of = new OrcFactory();
            DwarfFactory df = new DwarfFactory();
            Unit orc = of.makeUnit();
            Unit dw = df.makeUnit();
            orc.setPosition(1, 1);
            Assert.AreEqual(10, orc.getLife() + dw.getLife());
            orc.fightRound(dw);
            Assert.AreEqual(9, orc.getLife() + dw.getLife());
        }
        
        [TestMethod]
        public void TestCombatKill()
        {
            OrcFactory of = new OrcFactory();
            DwarfFactory df = new DwarfFactory();
            Unit orc = of.makeUnit();
            Unit dw = df.makeUnit();
            orc.setPosition(1, 1);
            dw.damage();
            dw.damage();
            dw.damage();
            dw.damage();

            Assert.AreEqual(6, orc.getLife() + dw.getLife());
            orc.fight(dw);
            //if we have no luck, the test fail ...
            Assert.AreEqual(0, dw.getLife());
        }
        [TestMethod]
        public void TestBasicMove()
        {
            //all test above concerning move should be also there ...
            Assert.Fail();
        }

        [TestMethod]
        public void TestMountainTeleportDwarfs()
        {
            //it fail cuz no ameManager is instanciated
            DwarfFactory df = new DwarfFactory();
            Unit dw1 = df.makeUnit();  // he should be on a mountain
            Unit dw2 = df.makeUnit();  // he sould be on a plain
            Mountain m = new Mountain();
            Plain p = new Plain();

            Assert.AreEqual(Unit.IMPOSSIBLE_MOVE, dw1.moveCost(6, 6, p));
            Assert.AreEqual(Unit.DEFAULT_MOVE_COST, dw1.moveCost(6, 6, m));
            Assert.AreEqual(Unit.IMPOSSIBLE_MOVE, dw2.moveCost(6, 6, p));
            Assert.AreEqual(Unit.IMPOSSIBLE_MOVE, dw2.moveCost(6, 6, m));

            // this need another test, when there is an enemy on the mountain
        }
        [TestMethod]
        public void TestSurroundedDyingElf()
        {
            Assert.Fail();
        }
    }
}
