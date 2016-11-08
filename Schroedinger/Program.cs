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
        public double WaveFunction(float B, int n, float x)
        {
            return B*Math.Sin(n*x*Math.PI);
        }
        static void Main(string[] args)
        {
            int y = 2;
            double x = new SchroedingerPgm().WaveFunction(y);
        }
    }
}
