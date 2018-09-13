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
        public Illuminant IlluminationSource;

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
    public class SrgbConverter : IColorSpaceConverter
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
        private double PivotRgb(double n) => n > 0.0405 ? Math.Pow((n + 0.055) / 1.055, 2.4) : n / 12.92;
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
