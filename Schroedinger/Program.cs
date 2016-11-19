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

        ///Need to declare this variable which is a result of converting ecount as a double to integer, to be able to call elements in array

        int arrayi;

        /// Define the potential energy V_0

        double V_0 = 3;

        /// Define c the constant

        double c;

        ///Period

        double T;

        double x;

        int n;

        ///Define the imaginary number

        double img = Math.Sqrt(-1);

        ///Set of coefficients output by applying the laplacian

        double[] bicoeff;

        double[] HamPsi;

        /// Define the choice of basis set and size

        public double[] BasisSet(double choice, int bsize)
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

            if (choice == 1)
            {

                ///Assigns basis set based on user input of number of terms to use
                ///The values are taken from the universal legendre coefficients set, basis_legendre_all (jagged array)

                Basis_Init = basis_legendre_all[bsize];
            }
            else if (choice==2)
            {
                ///Assign Fourier Series as the basis set

                for (n = 0; n < bsize; n++)

                    Basis_Init[n]= Math.Exp(-img * 2 * n * Math.PI * x / T);
            }

            
            return Basis_Init;
        }

        ///Finds Laplacian and adds V_0 as the last element, to define full Hamiltonian for Legendre
        
        public double[] Hamilton_Legendre(double[] Basis_Init, double ecount)
        {
            Basis_Temp = Basis_Init;
            Array.Reverse(Basis_Temp);

            for (ecount=0; ecount<Basis_Temp.Length; ecount++)
            {
                int arrayi = Convert.ToInt32(ecount);

                ///Takes derivative twice, once an array of legendre coefficients is reversed
                
                bicoeff[arrayi] = c* Basis_Temp[arrayi] * (bsize - 2 * arrayi) * (bsize - 2 * arrayi - 1);
            }

            bicoeff[Basis_Temp.Length] = V_0;

            return bicoeff;
        }

        ///Finds Laplacian and adds V_0 as the last element, to define full Hamiltonian for Fourier
        
        public double[] Hamilton_Fourier(double[] Basis_Init, int ecount)
        {
            return bicoeff;
        }




        /// Define Wave Function

        public double WaveFunction(double T)
        {
            return Math.Sin(2*Math.PI/T);
        }

        ///Defining array of complex numbers
        

        public double[] HamiltonianCoeff(double[] aicoeff, double[] bicoeff, double[] Hamiltonian)
        {

            ///aicoeff =[(1 + img), (3 * img), 2, (3 + img), (5 + 3 * img), (2 + 3 * img), 7, (1 + 3 * img), (5 + img), (9 + img), (7 * img), 3, 8, (4 + 9 * img), (2 + 8 * img)];

            for (int i = 0; i <= bsize;i++)
            {
                HamPsi[i] = (Hamiltonian[i] * bicoeff[i]) * aicoeff[i];
            }
            return HamPsi;
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
                ///call Legendgre
                double[] Basis_Init= new SchrodingerPgm().BasisSet(choice,bsize);
                ///new SchrodingerPgm().Hamilton_Legendre()
            }
            else if (choice==2)
            {
                ///call Fourier
                double[] Basis_Init = new SchrodingerPgm().BasisSet(choice,bsize);
                ///new SchrodingerPgm().Hamilton_Fourier()
            }

            ///Call multiplication of coefficients
            ///HamPsi= new SchrodingerPgm().HamiltonianCoeff()
            
            ///Call methods to solve the eigenvalue problem and find ground state energy
        }
    }
}
