using ColorLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Math;
namespace ChormaticSpace
{
    public class ChormaticAdaption
    {
        public static void GetCATMatrix(Illuminant source, Illuminant dest, ChromaticAdaptionMethod method = ChromaticAdaptionMethod.VonKries)
        {
            GetMatrix(method, ref Ma, ref MaPrime);
            double[] prb_source = Matrix.Dot(Ma, source.GetXYZ());
            double[] prb_dest = Matrix.Dot(Ma, dest.GetXYZ());
            double[,] prb = Matrix.Diagonal(3, Elementwise.Divide(prb_dest, prb_source));
            M = Matrix.Dot(Matrix.Dot(MaPrime, prb),Ma);
            
        }
        public static double[] CATTransform<T>(T[] inputValue) => Matrix.Dot(M, inputValue.Convert(x => (double)((object)x)));
        private static double[,] Ma;
        private static double[,] MaPrime;
        private static double[,] M;
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
                case ChromaticAdaptionMethod.CMCCAT2000:
                    ma = new double[3, 3] { { 0.7982, 0.3389, -0.1371 },
                                           {  -0.5918, 1.5512, 0.0406},
                                           { 0.0008, 0.239, 0.9753 } };
                    map = new double[,]{ { 1.076450, -0.237662, 0.161212 },
                                         {0.410964, 0.554342, 0.034694 },
                                         { -0.010954, -0.013389, 1.024343} };
                    break;
                case ChromaticAdaptionMethod.CAT02:
                    ma = new double[3, 3] { { 0.7328, 0.4296, -0.1624 },
                                           { -0.7036, 1.6975, 0.0061 },
                                           { 0.0030, 0.0136, 0.9834 } }; 
                    map = new double[,]{ { 1.096124, -0.278869, 0.182745 },
                                         { 0.454369, 0.473533, 0.072098 },
                                         { -0.009628, -0.005698, 1.015326 } };
                    break;
                case ChromaticAdaptionMethod.SharpCAT:
                    ma = new double[3, 3] { { 1.2694, -0.0988, - 0.1706 },
                                           { -0.8364, 1.8006, 0.0357},
                                           { 0.0297, -0.0315, 1.0018 } };
                    map = new double[,]{ { 0.8156,0.0472, 0.1372 },
                                         { 0.3791,0.5769, 0.0440 },
                                         { -0.0123, 0.0167, 0.9955 } };
                    break;
            }
        }

    }
    public enum ChromaticAdaptionMethod
    {
        XYZScaling,
        VonKries,
        BradFord,
        CMCCAT2000,
        CAT02,
        SharpCAT

    }
}
