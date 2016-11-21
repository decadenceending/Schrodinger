using Microsoft.VisualStudio.TestTools.UnitTesting;
using Schrodinger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schrodinger.Tests
{
    [TestClass()]
    public class SchrodingerPgmTests
    {
        [TestMethod()]
        public void BasisSetTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void BasisSet_FTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void Hamilton_LegendreTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void Hamilton_FourierTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void WaveFunctionTest()
        {
            ///Asses fuintionality of wave function portion

            double time = 2; double T = 2; double waveampexp = 0;

            double waveamp = new SchrodingerPgm().WaveFunction(time, T);

            Assert.AreEqual(waveamp, waveampexp);

        }

        [TestMethod()]
        public void FinalCoeffs_LegendreTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void FinalCoeffs_FourierTest()
        {
            Assert.Fail();
        }
    }
}