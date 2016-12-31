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

          int bsize=2;double[] Basis_Init_Exp={-1,3};


          double[][] basis_legendre_all = {
                new double[] {1},
                new double[] {1},
                new double[] {-1,3},
                new double[] {-3, 5},
                new double[] {3, -30, 35},
                new double[] {15, -70, 63},
                new double[] {-5, 105, -315, 231},
                new double[] {-35, 315, -693, 429},
                new double[] {35, -1260, 6930, -12012, 6435},
                new double[] {315,-4620, 18018, -25740, 12155},
                new double[] {-63, 3465, -30030, 90090, -109395, 46189},
                new double[] {-693, 15015, -90090, 218790, -230945, 88179},
                new double[] {231, -18018, 225225, -1021020, 2078505, -1939938, 676039}

            };

            bool Expected=true; bool Actual;

          double[] Basis_Init=new SchrodingerPgm().BasisSet(bsize);

          if (Basis_Init_Exp[0]==Basis_Init[0] && Basis_Init_Exp[1]==Basis_Init[1])
          {
            Actual=true;
          }
          else
          {
            Actual=false;
          }

          Assert.AreEqual(Actual, Expected);
        }

        [TestMethod()]
        public void BasisSet_FTest()
        {
          double T=2; int n=2;int bsize=2; double Basis_Init_Exp=3;

          bool Expected=true;bool Actual;

          double[] Basis_Init = new SchrodingerPgm().BasisSet_F(bsize);

          if (Basis_Init.Length==Basis_Init_Exp)
          {
            Actual=true;
          }
          else
          {
            Actual=false;
          }
          Assert.AreEqual(Actual, Expected);
        }

        [TestMethod()]
        public void Hamilton_LegendreTest()
        {
            int bsize = 2; double[] Basis_Init = { -1, 3 }; double V_0 = 1;

            double [,]bicoeff= new SchrodingerPgm().Hamilton_Legendre(bsize,Basis_Init, V_0);

          Assert.Fail();
        }

        [TestMethod()]
        public void Hamilton_FourierTest()
        {
            double[] Basis_Init = { -1, 3 }; double V_0 = 1;

            double [,]bicoeff_f= new SchrodingerPgm().Hamilton_Fourier(Basis_Init,V_0);

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
          double[] Basis_Temp= { 2, 4, 6 };double[,]bicoeff={{1,0,0},{0,1,0},{0,0,1}};

          double [,]HamPsi_Exp={{2,0,0},{0,4,0},{0,0,6}}; bool Expected=true;

          bool Actual;

          double[,]HamPsi= new SchrodingerPgm().FinalCoeffs_Legendre(bicoeff,Basis_Temp);

          if (HamPsi[1,1]==HamPsi_Exp[1,1] && HamPsi[2,2]==HamPsi_Exp[2,2] && HamPsi[3,3]==HamPsi_Exp[3,3])
          {
            Actual=true;
          }

          else
          {
            Actual=false;
          }

          Assert.AreEqual(Actual, Expected);
        }

        [TestMethod()]
        public void FinalCoeffs_FourierTest()
        {
          double[] Basis_Init= { 2, 4, 6 };double[,]bicoeff_f={{1,0,0},{0,1,0},{0,0,1}};

          double [,]HamPsi_F_Exp={{2,0,0},{0,4,0},{0,0,6}}; bool Expected=true;

          bool Actual;

          double[,]HamPsi_F= new SchrodingerPgm().FinalCoeffs_Fourier(bicoeff_f,Basis_Init);

          if (HamPsi_F[1,1]==HamPsi_F_Exp[1,1] && HamPsi_F[2,2]==HamPsi_F_Exp[2,2] && HamPsi_F[3,3]==HamPsi_F_Exp[3,3])
          {
            Actual=true;
          }

          else
          {
            Actual=false;
          }

          Assert.AreEqual(Actual, Expected);
        }
    }
}
