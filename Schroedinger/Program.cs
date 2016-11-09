using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schroedinger
{
    class SchroedingerPgm
    {
        /// Define the potential energy V(X)

        /// Define c the constant

        /// Define the size of the basis set

        /// Define the choice of basis set function
        
        public int[] BasisSet(int choice)
        {
            int[] Basis = new int[2];


            ///Prompt user to pick basis set

            Console.WriteLine("Please select your basis set of choice: 1 for Legendre, 2 for Fourier:");
            int choice = Convert.ToInt32(Console.ReadLine());
            int choiceadv = choice * 2;
            Console.WriteLine(choiceadv);
            Console.ReadLine();

            if (choice == 1)
            {
                Basis = new int[2]

                { 1, 3};
            }
            else if (choice==2)
            {
                Basis = new int[2] 

                { 2, 4};
            }

            return Basis;
        }

        /// Define Wave Function
        public double WaveFunction(double B, int n, float x)
        {
            return B*Math.Sin(n*x*Math.PI);
        }

        static void Main(string[] args)
        {
            int n = 1; ///Depends on energy level
            double B = (Math.PI) / 2; ///For Example
            float x = 2;
            double Psi = new SchroedingerPgm().WaveFunction(B,n,x);
        }
    }
}
