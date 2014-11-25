using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wrapper;
using System.Collections.Generic;

namespace TestsJeu
{
    [TestClass]
    public class TestsWrapperLibCpp
    {
        [TestMethod]
        public void WrapperGeneration_NotNull_SizeOk()
        {
            WrapperGenMap genmap = new WrapperGenMap(5, 5);
            List<int> ca = genmap.generateMap(5);

            // Test not null
            Assert.IsNotNull(ca);

            //test size
            Assert.AreEqual(25, ca.Count);
        }
        public void WrapperGeneration_CountainAll_Only()
        {
            WrapperGenMap genmap = new WrapperGenMap(5, 5);
            List<int> ca = genmap.generateMap(5);

            //test values
            int i;
            bool bornes = true;
            for (i = 0; ca[i] != '\0'; i++)
            {
                if (ca[i] > 5 && ca[i] < 0)
                {
                    bornes = false;
                }
            }
            Assert.IsTrue(bornes);

            //Test all types
            Assert.IsTrue(ca.Contains(0));
            Assert.IsTrue(ca.Contains(1));
            Assert.IsTrue(ca.Contains(2));
            Assert.IsTrue(ca.Contains(3));
            Assert.IsTrue(ca.Contains(4));
        }


        [TestMethod]
        public void WrapperSuggestion()
        {
            Assert.Fail();
            /*WrapperGenMap genmap = new WrapperGenMap(5, 5);
            List<int> ca = genmap.generateMap(5);
            List<int> opponents = new List<int>();
            List<int> movesPossibles = new List<int>();
            List<int> prop = genmap.bestMoves();
            Assert.AreEqual(6, prop.Count);*/
        }
    }
}
