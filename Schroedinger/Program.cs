using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schrodinger
{

    public class SchrodingerPgm
    {
        ///Declare the necessary variables

        ///Choice, which is determined by user input,1=legendre, 2=Fourier
        
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


        /// Define the potential energy V_0

        double V_0 = 3;

        /// Define c the constant

        double c=1;

        ///time

        double time;

        ///Period

        double T=2;

        double x;

        int n;

        ///Define the imaginary number

        double img = Math.Sqrt(-1);

        ///Set of coefficients output by applying the laplacian

        double[] bicoeff;

        double[,] bicoeff_f;

        double[] HamPsi;

        double[,] HamPsi_F;

        double[] Calc_Array;

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

        public double[] BasisSet_F(int bsize)

        {
            ///Assign Fourier Series as the basis set

            for (n = 0; n < bsize; n++)
            {

                ///Rewrite Fourier in the format of (e^(constant*n))^x,to extract portion without x

                Basis_Init[n] =Math.Exp(-img * 2 * n * Math.PI/ T);
            }
        
            return Basis_Init;

        }

        ///Finds Laplacian and adds V_0 as the last element, to define full Hamiltonian for Legendre
        
        public double[] Hamilton_Legendre(int bsize,double[] Basis_Init)
        {
            ///Reverse the array to make it more friendly for finding double derivative, for the chosen method

            Basis_Temp = Basis_Init;
            Array.Reverse(Basis_Temp);

            ///Loop to define the full Hamiltonian Operator array

            for (ecount=0; ecount<Basis_Temp.Length; ecount++)
            {
                ///Takes derivative twice, once an array of legendre coefficients is reversed
                
                bicoeff[ecount] = c* Basis_Temp[ecount] * (bsize - 2 * ecount) * (bsize - 2 * ecount - 1);
            }

            bicoeff[Basis_Temp.Length] = V_0;

            return bicoeff;
        }

        ///Finds Laplacian and adds V_0 as the last element, to define full Hamiltonian for Fourier
        ///Multiplication for Fourier differs than Legendre, and therefore V_0 can be attached to every element in the matrix
        
        public double[,] Hamilton_Fourier(double[] Basis_Init)
        {
 
            double[,] bicoeff_f=new double[Basis_Init.Length, Basis_Init.Length];
            
            ///Loop to define the full diagonal Hamiltonian Operator Matrix
        
            for (ecount = 0; ecount < Basis_Init.Length; ecount++)
            {
                ///i^2 yields -1, -*- => no - sign in the double derivative for Laplacian 

                //bicoeff_f[ecount,ecount] = c*(4 * Math.PI * Math.PI * ecount *ecount* Math.Exp(-img * 2 * ecount * Math.PI / T)) / (T * T)+V_0;
            }

            return bicoeff_f;
        }




        /// Define Wave Function

        public double WaveFunction(double time,double T)
        {
            return Math.Sin(2*Math.PI*time/T);
        }

        ///Defining array of complex numbers
        

        public double[] FinalCoeffs_Legendre(double[] bicoeff, double[] Basis_Init)
        {

            ///aicoeff =[(1 + img), (3 * img), 2, (3 + img), (5 + 3 * img), (2 + 3 * img), 7, (1 + 3 * img), (5 + img), (9 + img), (7 * img), 3, 8, (4 + 9 * img), (2 + 8 * img)];

            for (int i = 0; i <= Basis_Init.Length; i++)
            {
                HamPsi[i] = (Basis_Init[i] * bicoeff[i]); ///* aicoeff[i];
            }
            ///Basis_Temp;
            return HamPsi;
        }

        public double [,] FinalCoeffs_Fourier(double[,] bicoeff_f, double[] Basis_Init )
        {
            for (int i = 0; i <= Basis_Init.Length; i++)
            {
                HamPsi_F[i,i] = (Basis_Init[i] * bicoeff_f[i,i]); ///* aicoeff[i];
            }

            return HamPsi_F;
        }
            

        static void Main(string[] args)
        {
            ///Call the methods and perform the operations to find the result

            ///Prompt user to pick basis set

            Console.WriteLine("Please select your basis set of choice: 1 for Legendre, 2 for Fourier:");
            double choice = Convert.ToInt32(Console.ReadLine());

            ///Define the size of the basis set

            Console.WriteLine("Please specify the number of terms for your basis set (0<=n<=12, for Legendre, no restrictions for Fourier):");
            int bsize = Convert.ToInt32(Console.ReadLine());

            ///Keep Console open, until user chooses to close it

            Console.ReadLine();

            if (choice==1)
            {
                ///call Legendre Methods
                
                double[] Basis_Init= new SchrodingerPgm().BasisSet(bsize);
                double[] bicoeff = new SchrodingerPgm().Hamilton_Legendre(bsize,Basis_Init);
                double[] HamPsi = new SchrodingerPgm().FinalCoeffs_Legendre(bicoeff, Basis_Init);
            }
            else if (choice==2)
            {
                ///call Fourier Methods
                
                double[] Basis_Init = new SchrodingerPgm().BasisSet_F(bsize);
                double[,] bicoeff_f = new SchrodingerPgm().Hamilton_Fourier(Basis_Init);
                double[,] HamPsi_F = new SchrodingerPgm().FinalCoeffs_Fourier(bicoeff_f, Basis_Init);
            }

            ///HamPsi= new SchrodingerPgm().HamiltonianCoeff()
            
            ///Call methods to solve the eigenvalue problem and find ground state energy
        }
    }
}
