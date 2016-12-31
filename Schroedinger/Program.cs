using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Factorization;

namespace Schrodinger
{

    public class SchrodingerPgm
    {
        ///Declare the necessary variables

        ///Choice, which is determined by user input,1=Legendre, 2=Fourier

        double choice;

        ///Number of terms for a Legendre Polynomial or Fourier Series, determined by user input

        int bsize;

        ///Result of selecting Legendre polynomial and assigning a coefficient set,
        ///corresponding to the selected number of terms

        double[] Basis_Init;

        ///Coefficients set and V_0(x), defining the Hamiltonian

        double[] Basis_Temp;

        ///For loop counter

        int ecount;

        /// Define c the constant

        double c=1;

        ///time

        double time;



        double x;

        int n;

        ///Define the imaginary number

        double img = 1; ///Math.Sqrt(-1);

        ///Set of coefficients output by applying Hamiltonian on Fourier

        double[,] bicoeff_f;

        double[,] HamPsi;

        double[,] HamPsi_F;

        /// Define the choice of basis set and size

        public double[] BasisSet(int bsize)
        {
            ///Define Legendre coefficients from n=0 to n=12

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

            ///Assigns basis set based on user input of number of terms to use
            ///The values are taken from the universal legendre coefficients set, basis_legendre_all (jagged array)

            Basis_Init = basis_legendre_all[bsize];

            return Basis_Init;
        }

        public double[] BasisSet_F(int bsize, double T, double img)

        {
            double[] Basis_Init = new double[bsize+1];

            ///Assign Fourier Series as the basis set

            for (n = 0; n <= bsize; n++)
            {

                ///Rewrite Fourier in the format of (e^(constant*n))^x,to extract portion without x

                Basis_Init[n] = Math.Exp(-img * 2 * n * Math.PI) / T;
            }

            return Basis_Init;

        }

        ///Finds Laplacian and adds V_0 as the last element, to define full Hamiltonian for Legendre

        public double[,] Hamilton_Legendre(int bsize,double[] Basis_Init, double V_0)
        {
            ///Reverse the array to make it more friendly for finding double derivative, for the chosen method

            Basis_Temp = Basis_Init;

            Array.Reverse(Basis_Temp);

            int rowcoeff = Basis_Temp.GetLength(0)+1;;

            double[,] bicoeff = new double[rowcoeff, rowcoeff];

            ///Loop to define the full Hamiltonian Operator array

            for (ecount=0; ecount<Basis_Temp.GetLength(0); ecount++)
            {
                ///Takes derivative twice, once an array of legendre coefficients is reversed

                bicoeff[ecount,ecount] = c* Basis_Temp[ecount] * (bsize - 2 * ecount) * (bsize - 2 * ecount - 1);
            }

            ///Add the last remaining V_0 since 1 term is always lost during differentiation

            bicoeff[Basis_Temp.GetLength(0), Basis_Temp.GetLength(0)] = V_0;

            return bicoeff;
        }

        ///Finds Laplacian and adds V_0 as the last element, to define full Hamiltonian for Fourier
        ///Multiplication for Fourier differs than Legendre, and therefore V_0 can be attached to every element in the matrix

        public double[,] Hamilton_Fourier(double[] Basis_Init, double V_0,double T)
        {

            double[,] bicoeff_f=new double[Basis_Init.GetLength(0), Basis_Init.GetLength(0)];

            ///Loop to define the full diagonal Hamiltonian Operator Matrix

            for (ecount = 0; ecount < Basis_Init.Length; ecount++)
            {
                ///i^2 yields -1, -*- => no - sign in the double derivative for Laplacian

                bicoeff_f[ecount,ecount] = c*(4 * Math.PI * Math.PI * ecount *ecount* Math.Exp(-img * 2 * ecount * Math.PI / T)) / (T * T)+V_0;
            }

            return bicoeff_f;
        }

        /// Define Wave Function

        public double WaveFunction(double time,double T)
        {
            return Math.Sin((2*Math.PI*time)/T);
        }

        ///Define multiplication of Operated on Coeff's (bicoeff) and the Initial Coeff's (Basis_Init), for Legendre


        public double[,] FinalCoeffs_Legendre(double[,]bicoeff, double[] Basis_Temp)
        {
            double[,] HamPsi = new double[Basis_Temp.GetLength(0), Basis_Temp.GetLength(0)];

            ///Defining array of complex numbers
            ///aicoeff =[(1 + img), (3 * img), 2, (3 + img), (5 + 3 * img), (2 + 3 * img), 7, (1 + 3 * img), (5 + img), (9 + img), (7 * img), 3, 8, (4 + 9 * img), (2 + 8 * img)];

            for (int i = 0; i < Basis_Temp.GetLength(0); i++)
            {
                HamPsi[i,i] = (Basis_Temp[i] * bicoeff[i,i]); ///* aicoeff[i];
            }
            ///Basis_Temp;
            return HamPsi;
        }

        ///Define multiplication of Operated on Coeff's (bicoeff) and the Initial Coeff's (Basis_Init), for Fourier

        public double [,] FinalCoeffs_Fourier(double[,] bicoeff_f, double[] Basis_Init )
        {
            double[,] HamPsi_F = new double[Basis_Init.GetLength(0), Basis_Init.GetLength(0)];

            for (int i = 0; i < Basis_Init.Length; i++)
            {
                HamPsi_F[i,i] = (Basis_Init[i] * bicoeff_f[i,i]); ///* aicoeff[i];
            }

            return HamPsi_F;
        }

        ///Define method to solve eigenvalue problem for Legendre, using Math.NET package and its linear algebra capabilities

        public Vector<double>[] EigenSolution_Legendre(double[,] HamPsi)

        {

            ///Utilize Ham_Psi, the result of Ham applied on Psi and multiplied by Psi, Legendre

            var A = Matrix<double>.Build.Dense(bsize + 1, bsize + 1);

            A.DenseOfMatrix(HamPsi);

            ///Command to find the actual eigen values in a vector "nullspace"

            Vector<double>[] nullspace = A.Kernel();

            return nullspace;

        }

        public Vector<double>[] EigenSolution_Fourier(double[,] Ham_Psi_F)

        {

            ///Utilize Ham_Psi_F, the result of Ham applied on Psi and multiplied by Psi, Fourier

            var A = Matrix<double>.Build.Dense(bsize + 1, bsize + 1);

            A.DenseOfMatrix(HamPsi_F);

            ///Command to find the actual eigen values in a vector "nullspace"

            Vector<double>[] nullspace = A.Kernel();

            return nullspace;

        }

        static void Main(string[] args)
        {
            ///Call the methods and perform the operations to find the result

            ///Prompt user to pick a basis set

            double V_0 = 1;
            double T = 2;
            double img = 2;

            Console.WriteLine("Please select your basis set of choice: 1 for Legendre, 2 for Fourier:");
            double choice = Convert.ToInt32(Console.ReadLine());

            ///Define the size of the basis set

            Console.WriteLine("Please specify the number of terms for your basis set (0<=n<=12, for Legendre, no restrictions for Fourier):");
            int bsize = Convert.ToInt32(Console.ReadLine());

            ///Keep Console open, until user chooses to close it

            Console.ReadLine();

            if (choice==1)
            {
                ///Call Legendre Methods

                double[] Basis_Init= new SchrodingerPgm().BasisSet(bsize);
                double[,] bicoeff = new SchrodingerPgm().Hamilton_Legendre(bsize,Basis_Init,V_0);
                double[,] HamPsi = new SchrodingerPgm().FinalCoeffs_Legendre(bicoeff, Basis_Init);

                Vector<double>[] nullspace = new SchrodingerPgm().EigenSolution_Legendre(HamPsi);

                ///Return the smallest value in nullspace vector to obtain minimum eigen value, which is also ground state energy

                ///Console.WriteLine(nullspace.Min());
            }
            else if (choice==2)
            {
                ///Call Fourier Methods

                double[] Basis_Init = new SchrodingerPgm().BasisSet_F(bsize,T,img);
                double[,] bicoeff_f = new SchrodingerPgm().Hamilton_Fourier(Basis_Init,V_0,T);
                double[,] HamPsi_F = new SchrodingerPgm().FinalCoeffs_Fourier(bicoeff_f, Basis_Init);

                Vector<double>[] nullspace = new SchrodingerPgm().EigenSolution_Fourier(HamPsi_F);

                ///Return the smallest value in nullspace vector to obtain minimum eigen value, which is also ground state energy

                ///Console.WriteLine(nullspace.Min());
            }
        }
    }
}
