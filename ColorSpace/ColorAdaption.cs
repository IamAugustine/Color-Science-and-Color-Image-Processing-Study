using ColorLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChormaticSpace
{
    public class ChormaticAdaption
    {
        public static void GetCATMatrixWithDefinedIlluminant()
        {

        }
        public static double[] CATTransform(double[] inputValue, double[] originWhitePoint, double[] targetWhitePoint, ChromaticAdaptionMethod method = ChromaticAdaptionMethod.VonKries)
        {
            double[] adaptedValue = null;
            GetMatrix(method, ref Ma, ref MaPrime);
            
            return adaptedValue;
        }
        public static double[] CATTransform(double[] inputValue, Illuminant illumSource, Illuminant illumDest, ChromaticAdaptionMethod method = ChromaticAdaptionMethod.VonKries)
        {
            double[] adaptedValue = null;
            GetMatrix(method, ref Ma, ref MaPrime);
            return adaptedValue;
        }
        private static double[,] Ma;
        private static double[,] MaPrime;

        static void GetMatrix(ChromaticAdaptionMethod method, ref double[,] ma, ref double[,] map)
        {
            switch (method)
            {
                case ChromaticAdaptionMethod.BradFord:
                    ma = new double[3, 3] { { 0.8951, 0.2664, -0.1614 },
                                            { -0.7502, 1.7135, 0.0367 },
                                            { 0.0389, -0.0685, 1.0296 } };
                    map = new double[3, 3] { { 0.9869929, -0.1470543, 0.1599627 },
                                             { 0.4323053, 0.5183603, 0.0492912 },
                                             { -0.0085287, 0.0400428, 0.9684867 } };
                    break;
                case ChromaticAdaptionMethod.XYZScaling:
                    ma = new double[3, 3] { { 1.0, 0.0, 0.0 }, 
                                            { 0.0, 1.0, 0.0 }, 
                                            { 0.0, 0.0, 1.0 } };
                    map = new double[3, 3] {{ 1.0, 0.0, 0.0 }, 
                                            { 0.0, 1.0, 0.0 }, 
                                            { 0.0, 0.0, 1.0 } };
                    break;
                case ChromaticAdaptionMethod.VonKries:
                    ma = new double[3,3] { { 0.40024, 0.7076, -0.08081 },
                                           { -0.2263, 1.16532, 0.0457 }, 
                                           { 0.0, 0.0, 0.91822 } };
                    map = new double[,]{ { 1.8599364, -1.1293816, 0.2198974 }, 
                                         { 0.3611914, 0.6388125, -0.0000064 }, 
                                         { 0.0, 0.0, 1.0890636 } };
                    break;

            }
        }

    }
    public enum ChromaticAdaptionMethod
    {
        XYZScaling,
        VonKries,
        BradFord
    }
}
