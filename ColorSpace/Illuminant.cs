using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorLib
{
    public class Illuminant
    {
        public CIEXYZ XYZ;
        public double[][] RgbConversionMatrix;
        
    }
    public class DefaultIlluminant
    {
        public static readonly Illuminant D65 = new Illuminant() {XYZ = new CIEXYZ(0.95682, 1, 0.92149) };
        Illuminant D50;
        Illuminant D75;
        Illuminant A;

    }
}
