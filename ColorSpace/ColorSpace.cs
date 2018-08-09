using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorLib
{
    public abstract class ColorSpace
    {
        protected abstract ColorSpaceEnums SpaceName { set; get; }
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
        protected override ColorSpaceEnums SpaceName { get => ColorSpaceEnums.CIEXYZ; set => SpaceName = value; }
        public double X, Y, Z;
        Illuminant _defaultIlluminant = DefaultIlluminant.D65;
        public CIEXYZ(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
            ChrmComponent = new[] { x, y, z };
        }
        //public static CIELAB ConvertToCIELAB(Illuminant illuminant)
        //{
        //    return ConvertToCIELAB(this, illuminant);
        //}
        //public static CIELAB ConvertToCIELAB(CIEXYZ xyz, Illuminant illuminant)
        //{
        //    double[] lab = 
        //}

        
    }
    public class CIELAB : ColorSpace
    {
        protected override ColorSpaceEnums SpaceName { get => ColorSpaceEnums.CIELAB; set => SpaceName = value; }
        public double L, A, B;
        private Illuminant WhitePoint;
        public CIELAB(double l, double a, double b, Illuminant whitePoint= null)
        {
            L = l;
            A = a;
            B = b;
            ChrmComponent = new[] { l, a, b };
            WhitePoint = whitePoint ?? DefaultIlluminant.D65;
        }
        public CIELAB(double[] lab,  Illuminant whitePoint=null)
        {
            L = lab[0];
            A = lab[1];
            B = lab[2];
            ChrmComponent = lab;
            WhitePoint = whitePoint ?? DefaultIlluminant.D65;
        }
    }
}
