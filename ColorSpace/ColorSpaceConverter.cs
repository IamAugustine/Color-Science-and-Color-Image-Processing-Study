using Accord.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorLib
{
    public interface IColorSpaceConverter
    {
        double[] ConvertToXYZ<T>(T[] input);
        double[] ConvertToLAB<T>(T[] input);
        double[] ConvertToLUV<T>(T[] input);
        double[] ConvertToSrgb<T>(T[] input);
        double[] ConvertToLinearRgb<T>(T[] input);
        double[] ConvertToLCHuv<T>(T[] input);
        double[] ConvertToLCHab<T>(T[] input);
        double[] ConvertToHSI<T>(T[] input);
        double[] ConvertToCMYK<T>(T[] input);
        double[] ConvertToxyY<T>(T[] input);
        double[] ConvertToYCbCr<T>(T[] input);
    }
    public class sRgbConverter : IColorSpaceConverter
    {
        public Illuminant IlluminationSource = DefaultIlluminant.D65;
        public Illuminant IlluminationDest;
        public double[,] MatrixToXYZ = new double[3, 3] {{ 0.4124564, 0.3575761, 0.1804375 },
                                            { 0.2126729, 0.7151522, 0.0721750 },
                                            { 0.0193339,  0.1191920,  0.9503041 }};
        public double[] ConvertToCMYK<T>(T[] input)
        {
            throw new NotImplementedException();
        }

        public double[] ConvertToHSI<T>(T[] input)
        {
            throw new NotImplementedException();
        }

        public double[] ConvertToLAB<T>(T[] input)
        {
            throw new NotImplementedException();
        }

        public double[] ConvertToLCHab<T>(T[] input)
        {
            throw new NotImplementedException();
        }

        public double[] ConvertToLCHuv<T>(T[] input)
        {
            throw new NotImplementedException();
        }

        public double[] ConvertToLinearRgb<T>(T[] input)
        {
            double[] lRgb = new double[3];
            for (int i = 0; i < 3; i++)
            {
                double sV = Convert.ToDouble(input[i]) / 255d;
                lRgb[i] = sV <= 0.04045 ? sV / 12.92 : Math.Pow((sV + 0.055) / 1.055, 2.4);
            }

            return lRgb;
        }

        public double[] ConvertToLUV<T>(T[] input)
        {
            throw new NotImplementedException();
        }

        public double[] ConvertToSrgb<T>(T[] input)
        {
            throw new NotImplementedException();
        }

        public double[] ConvertToxyY<T>(T[] input)
        {
            throw new NotImplementedException();
        }

        public double[] ConvertToXYZ<T>(T[] input)
        {
            double[] lrgb = ConvertToLinearRgb(input);
            return MatrixToXYZ.Dot(lrgb).Apply(x => x * 100d);
        }

        public double[] ConvertToYCbCr<T>(T[] input)
        {
            throw new NotImplementedException();
        }
    }
    public class LinearRGBConverter : IColorSpaceConverter
    {
        public double[] ConvertToCMYK<T>(T[] input)
        {
            throw new NotImplementedException();
        }

        public double[] ConvertToHSI<T>(T[] input)
        {
            throw new NotImplementedException();
        }

        public double[] ConvertToLAB<T>(T[] input)
        {
            throw new NotImplementedException();
        }

        public double[] ConvertToLCHab<T>(T[] input)
        {
            throw new NotImplementedException();
        }

        public double[] ConvertToLCHuv<T>(T[] input)
        {
            throw new NotImplementedException();
        }

        public double[] ConvertToLinearRgb<T>(T[] input)
        {
            throw new NotImplementedException();
        }

        public double[] ConvertToLUV<T>(T[] input)
        {
            throw new NotImplementedException();
        }

        public double[] ConvertToSrgb<T>(T[] input)
        {
            throw new NotImplementedException();
        }

        public double[] ConvertToxyY<T>(T[] input)
        {
            throw new NotImplementedException();
        }

        public double[] ConvertToXYZ<T>(T[] input)
        {
            throw new NotImplementedException();
        }

        public double[] ConvertToYCbCr<T>(T[] input)
        {
            throw new NotImplementedException();
        }
    }
    public class XYZConverter : IColorSpaceConverter
    {
        public Illuminant IlluminationSource;
        public Illuminant IlluminantDest;
        public Illuminant WhitePoint;
        public double[,] XYZtoSRGBMatrix =  new double[3, 3] {  {3.2404542, -1.5371385, -0.4985314 },
                                                                { -0.9692660,  1.8760108,  0.0415560 },
                                                                { 0.0556434, -0.2040259,  1.0572252 } };
        public double[] ConvertToCMYK<T>(T[] input)
        {
            throw new NotImplementedException();
        }

        public double[] ConvertToHSI<T>(T[] input)
        {
            throw new NotImplementedException();
        }

        public double[] ConvertToLAB<T>(T[] input)
        {
            throw new NotImplementedException();
        }

        public double[] ConvertToLCHab<T>(T[] input)
        {
            throw new NotImplementedException();
        }

        public double[] ConvertToLCHuv<T>(T[] input)
        {
            throw new NotImplementedException();
        }

        public double[] ConvertToLinearRgb<T>(T[] input)
        {
            throw new NotImplementedException();
        }

        public double[] ConvertToLUV<T>(T[] input)
        {
            throw new NotImplementedException();
        }

        public double[] ConvertToSrgb<T>(T[] input)
        {
            double[] lRgb = ConvertToLinearRgb(input.Apply(x => Convert.ToDouble(x) / 100d));
            return lRgb;
        }

        public double[] ConvertToxyY<T>(T[] input)
        {
            throw new NotImplementedException();
        }

        public double[] ConvertToXYZ<T>(T[] input)
        {
            throw new NotImplementedException();
        }

        public double[] ConvertToYCbCr<T>(T[] input)
        {
            throw new NotImplementedException();
        }
    }
    public class AdobeRGBConverter : IColorSpaceConverter
    {
        Illuminant ReferenceWhitePoint = DefaultIlluminant.D65;
        public double[] ConvertToCMYK<T>(T[] input)
        {
            throw new NotImplementedException();
        }

        public double[] ConvertToHSI<T>(T[] input)
        {
            throw new NotImplementedException();
        }

        public double[] ConvertToLAB<T>(T[] input)
        {
            throw new NotImplementedException();
        }

        public double[] ConvertToLCHab<T>(T[] input)
        {
            throw new NotImplementedException();
        }

        public double[] ConvertToLCHuv<T>(T[] input)
        {
            throw new NotImplementedException();
        }

        public double[] ConvertToLinearRgb<T>(T[] input)
        {
            throw new NotImplementedException();
        }

        public double[] ConvertToLUV<T>(T[] input)
        {
            throw new NotImplementedException();
        }

        public double[] ConvertToSrgb<T>(T[] input)
        {
            throw new NotImplementedException();
        }

        public double[] ConvertToxyY<T>(T[] input)
        {
            throw new NotImplementedException();
        }

        public double[] ConvertToXYZ<T>(T[] input)
        {
            throw new NotImplementedException();
        }

        public double[] ConvertToYCbCr<T>(T[] input)
        {
            throw new NotImplementedException();
        }
    }
}
