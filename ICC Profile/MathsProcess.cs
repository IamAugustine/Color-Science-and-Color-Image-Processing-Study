using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Math.Metrics;
namespace ICC_Profile
{
    internal class MathsProcess
    {
        public static double[] MatixMultiply(double[,] m1, double[] m2) => Accord.Math.Matrix.Dot(m1, m2);
        public static double[] MatrixAdd(double[] m1, double[] m2) => Accord.Math.Elementwise.Add(m1, m2);
    }
}
