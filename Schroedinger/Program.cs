using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schrodinger
{
    class SchrodingerPgm
    {
        /// Define the potential energy V_0

        double V_0 = 1.5;

        /// Define c the constant

        double c;

        ///int[] basis_legendre = { 1, 1, -1, 3, -3, 5, 3, -30, 35, 15, -70, 63, -5, 105, -315, 231, -35, 315, -693, 429, 35, -1260, 6930, -12012, 6435, 315,
        ///    -4620, 18018, -25740, 12155, -63, 3465, -30030, 90090, -109395, 46189, -693, 15015, -90090, 218790, -230945, 88179, 231, -18018, 225225, -1021020, 2078505, -1939938, 676039 };

        /// Define the choice of basis set function

        public int[] BasisSet()
        {
            ///Define Legendre coefficients from n=0 to n=12
            
            int[][] basis_legendre_all = {
                new int[] {1},
                new int[] {1},
                new int[] {-1,3},
                new int[] {-3, 5},
                new int[] {3, -30, 35},
                new int[] {15, -70, 63},
                new int[] {-5, 105, -315, 231},
                new int[] {-35, 315, -693, 429},
                new int[] {35, -1260, 6930, -12012, 6435},
                new int[] {315,-4620, 18018, -25740, 12155},
                new int[] {-63, 3465, -30030, 90090, -109395, 46189},
                new int[] {-693, 15015, -90090, 218790, -230945, 88179},
                new int[] {231, -18018, 225225, -1021020, 2078505, -1939938, 676039}

            };

            ///Prompt user to pick basis set

            Console.WriteLine("Please select your basis set of choice: 1 for Legendre, 2 for Fourier:");
            int choice = Convert.ToInt32(Console.ReadLine());

            /// Define the size of the basis set

            Console.WriteLine("Please speciofy the number of terms for your basis set:");
            int bsize = Convert.ToInt32(Console.ReadLine());

            ///Keep Console open, until user chooses to close it

            Console.ReadLine();

            if (choice == 1)
            {
                int[] Basis = basis_legendre_all[bsize];
                return Basis;
            }
            else if (choice==2)
            {
                int[] Basis = new int[2];
                return Basis;

            }
        }

        /// Define Wave Function
        
        public double WaveFunction(double B, int n, double x)
        {
            return B*Math.Sin(n*x*Math.PI);
        }

        ///Define Hamiltonian
        public double[] Hamiltonian()
        {

            double[] Laplacian = { };
            return -c * Laplacian + V_0;
        }
        ///Defining array of complex numbers
        

        public int[] HamiltonianCoeff(double[] aicoeff, int[] bicoeff, int[] Hamiltonian)
        {
            double img = Math.Sqrt(-1);

            aicoeff =[ 1 + img, 3 * img, 2, 3 + img, 5 + 3 * img, 2 + 3 * img, 7, 1 + 3 * img, 5 + img, 9 + img, 7 * img, 3, 8, 4 + 9 * img, 2 + 8 * img];

            for (int i = bsize; i <= bsize;i++)
            {
                int[] HamPsi[i] = (Hamiltonian[i] * bicoeff[i]) * aicoeff;
            }
            return HamPsi;
        }
            

        static void Main(string[] args)
        {
            ///Depends on energy level int n = 1;
            ///double B = (Math.PI) / 2;
            ///float x = 2;
            ///double Psi = new SchrodingerPgm().WaveFunction(B,n,x);

            ///Call the functions and perform the operations to find the result
            

        }
    }
}
