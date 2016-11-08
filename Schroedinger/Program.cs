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
