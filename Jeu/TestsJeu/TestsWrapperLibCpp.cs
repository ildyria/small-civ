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
        unsafe public void TestGeneration()
        {
            WrapperGenMap genmap = new WrapperGenMap(5, 5);

            List<int> ca = genmap.generateMap(5);
            //Assert.IsTrue(60 < ca[0] && ca[0] < 80);
            int i;
            bool bornes = true;
            for (i = 0; ca[i] != '\0'; i++)
            {
                if (ca[i] > 5 && ca[i] < 0)
                {
                    bornes = false;
                }
            }
            //test valeurs
            Assert.IsTrue(bornes);
            //test taille
            Assert.AreEqual(25, ca.Count);
            //Test tout les types
            Assert.IsTrue(ca.Contains(0));
            Assert.IsTrue(ca.Contains(1));
            Assert.IsTrue(ca.Contains(2));
            Assert.IsTrue(ca.Contains(3));
            Assert.IsTrue(ca.Contains(4));
        }
    }
}
