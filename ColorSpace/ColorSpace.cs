using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ColorLib.Illuminant;

namespace ColorLib
{
    public abstract class ColorSpace
    {
        protected abstract ColorSpaceEnums SpaceName {  get; }
        public double[] WhitePoint;

        protected ColorSpace()
        {
            ChrmComponent = new double[3];
        }
        public ColorSpace(double v1, double v2, double v3)
        {
            ChrmComponent = new double[3] { v1, v2, v3 };
        }

        protected double[] ChrmComponent;
        public override string ToString()
        {
            return ChrmComponent.ToString();
        }
    }

    public class CIEXYZ : ColorSpace
    {
        protected override ColorSpaceEnums SpaceName { get => ColorSpaceEnums.CIEXYZ;  }
        public double X, Y, Z;
        Illuminant _defaultIlluminant = DefaultIlluminant.D65;
        public CIEXYZ(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
            ChrmComponent = new[] { x, y, z };
        }
        public CIEXYZ(double[] xyz)
        {
            X = xyz[0];
            Y = xyz[1];
            Z = xyz[2];
            ChrmComponent = xyz;
        }
        public CIEXYZ()
        {
            ChrmComponent = new double[3];
        }
    }
    public class CIELAB : ColorSpace
    {
        protected override ColorSpaceEnums SpaceName { get => ColorSpaceEnums.CIELAB; }
        public double L, A, B;
        private Illuminant _whitePoint;
        public CIELAB(double l, double a, double b, Illuminant whitePoint= null)
        {
            L = l;
            A = a;
            B = b;
            ChrmComponent = new[] { l, a, b };
            _whitePoint = whitePoint ?? DefaultIlluminant.D65;
        }
        public CIELAB(double[] lab,  Illuminant whitePoint=null)
        {
            L = lab[0];
            A = lab[1];
            B = lab[2];
            ChrmComponent = lab;
            _whitePoint = whitePoint ?? DefaultIlluminant.D65;
        }
    }
    public class RGB: ColorSpace
    {
        public Illuminant ReferenceIlluminant;
        public IColorSpaceConverter ColorSpaceConverter;

        protected override ColorSpaceEnums SpaceName => throw new NotImplementedException();
    }
    public class AdobeRGB : RGB
    {
        protected override ColorSpaceEnums SpaceName { get => ColorSpaceEnums.AdobeRGB; }
        public AdobeRGB()
        {
            ColorSpaceConverter = new AdobeRGBConverter();
            ReferenceIlluminant = DefaultIlluminant.D65;
        }

    }
   
    public class sRGB : RGB
    {
        protected override ColorSpaceEnums SpaceName { get => ColorSpaceEnums.sRGB; }
        public sRGB()
        {
            ColorSpaceConverter = new sRgbConverter();
            ReferenceIlluminant = DefaultIlluminant.D65;
        }
    }


}
