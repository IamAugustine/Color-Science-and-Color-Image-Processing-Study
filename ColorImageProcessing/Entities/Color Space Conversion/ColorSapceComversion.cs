using Accord.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorImageProcessing.Entities.Color_Space_Conversion
{
    public class ColorSapceComversion
    {
        public static double[] sRGBToXYZ<T>(T[] srgb)
        {
            double[,] M = new double[3, 3] {{ 0.4124564, 0.3575761, 0.1804375 }, 
                                            { 0.2126729, 0.7151522, 0.0721750 }, 
                                            { 0.0193339,  0.1191920,  0.9503041 }};

            return Matrix.Dot(M, new double[3] { Pivot(srgb[0]), Pivot(srgb[1]), Pivot(srgb[2]) }).Apply(x => x * 100);
        }
        private static double Pivot<T>(T v)
        {
            double sV = Convert.ToDouble(v) / 255d;
            return sV <= 0.04045 ? sV / 12.92 : Math.Pow((sV + 0.055) / 1.055, 2.4);

        }
        private static byte InversePivot(double sv)
        {
            double v = sv <= 0.0031308 ? sv * 12.92 *255 : (Math.Pow(sv * 1.055, 1 / 2.4) - 0.055)*255;
            return Math.Round(v) > 255 ? (byte)255 : (byte)Math.Round(v);
        }
        public static byte[] XYZTosRGB<T>(T[] xyz)
        {
            double[,] mp = new double[3, 3] {{3.2404542, -1.5371385, -0.4985314 },
                                            {-0.9692660,  1.8760108,  0.0415560 },
                                            { 0.0556434, -0.2040259,  1.0572252 }};
            double[] rgb = mp.Dot(xyz.Convert(x => Convert.ToDouble(x)));
            return new byte[3] { InversePivot(rgb[0]), InversePivot(rgb[1]), InversePivot(rgb[2]) };
        }
        
    }
}
