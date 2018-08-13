using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ColorLib
{
    public class Illuminant : INotifyPropertyChanged
    {
        public KnownIlluminant Name;
        public double X { set { XYZ.X = value; NotifyPropertyChanged("X"); } get { return XYZ.X; } }
        public double Y { set { XYZ.Y = value; NotifyPropertyChanged("Y"); } get { return XYZ.Y; } }
        public double Z { set { XYZ.Z = value; NotifyPropertyChanged("Z"); } get { return XYZ.Z; } }
        public CIEXYZ XYZ;
        public static Illuminant GetIlluminant(KnownIlluminant name)
        {
            switch (name)
            {
                case KnownIlluminant.A:
                    return DefaultIlluminant.A;
                case KnownIlluminant.B:
                    return DefaultIlluminant.B;
                case KnownIlluminant.C:
                    return DefaultIlluminant.C;
                case KnownIlluminant.D50:
                    return DefaultIlluminant.D50;
                case KnownIlluminant.D55:
                    return DefaultIlluminant.D55;
                case KnownIlluminant.D65:
                    return DefaultIlluminant.D65;
                case KnownIlluminant.D75:
                    return DefaultIlluminant.D75;
                case KnownIlluminant.E:
                    return DefaultIlluminant.E;
                case KnownIlluminant.F2:
                    return DefaultIlluminant.F2;
                case KnownIlluminant.F7:
                    return DefaultIlluminant.F7;
                case KnownIlluminant.F11:
                    return DefaultIlluminant.F11;
                case KnownIlluminant.Custom:
                    return new Illuminant() { Name = KnownIlluminant.Custom, XYZ = new CIEXYZ()};
                default:
                    return DefaultIlluminant.D65;
            }
        }
        public double[][] RgbConversionMatrix;

        public event PropertyChangedEventHandler PropertyChanged;

        public double[] GetXYZ() => new double[3] { XYZ.X, XYZ.Y, XYZ.Z };

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class DefaultIlluminant
    {
        public static readonly Illuminant A = new Illuminant() { XYZ = new CIEXYZ(1.09850,1.00000, 0.35585), Name = KnownIlluminant.A };
        public static readonly Illuminant B = new Illuminant() { XYZ = new CIEXYZ(0.99072,1.00000, 0.85223), Name = KnownIlluminant.B };
        public static readonly Illuminant C = new Illuminant() { XYZ = new CIEXYZ(0.98074,1.00000, 1.18232), Name = KnownIlluminant.C };
        public static readonly Illuminant D50 = new Illuminant() { XYZ = new CIEXYZ(0.96422, 1.00000, 0.82521), Name = KnownIlluminant.D50 };
        public static readonly Illuminant D55 = new Illuminant() { XYZ = new CIEXYZ(0.95682, 1.00000, 0.92149), Name = KnownIlluminant.D55 };
        public static readonly Illuminant D65 = new Illuminant() { XYZ = new CIEXYZ(0.95047, 1.00000, 1.08883), Name = KnownIlluminant.D65 };
        public static readonly Illuminant D75 = new Illuminant() { XYZ = new CIEXYZ(0.94972, 1.00000, 1.22638), Name = KnownIlluminant.D75 };
        public static readonly Illuminant E = new Illuminant() {XYZ = new CIEXYZ(1.0000, 1.00000, 1.0000), Name = KnownIlluminant.E };
        public static readonly Illuminant F2 = new Illuminant() { XYZ = new CIEXYZ(0.99186,  1.00000, 0.67393), Name = KnownIlluminant.F2 };
        public static readonly Illuminant F7 = new Illuminant() { XYZ = new CIEXYZ(0.95041,  1.00000, 1.08747), Name = KnownIlluminant.F7 };
        public static readonly Illuminant F11 = new Illuminant() { XYZ = new CIEXYZ(1.00962, 1.00000, 0.64350), Name = KnownIlluminant.F11 };
    }
    public enum KnownIlluminant
    {
        A = 0,
        B,
        C,
        D50,
        D55,
        D65, 
        D75,
        E,
        F2,
        F7,
        F11,
        Custom

    }
//A 	1.09850	1.00000	0.35585
//B	    0.99072	1.00000	0.85223
//C	    0.98074	1.00000	1.18232
//D50	0.96422	1.00000	0.82521
//D55	0.95682	1.00000	0.92149
//D65	0.95047	1.00000	1.08883
//D75	0.94972	1.00000	1.22638
//E	    1.00000	1.00000	1.00000
//F2	0.99186	1.00000	0.67393
//F7	0.95041	1.00000	1.08747
//F11	1.00962	1.00000	0.64350
}
