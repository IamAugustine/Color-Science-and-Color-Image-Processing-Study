using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICC_Profile.DataStruct
{
    public class ParametricCurve
    {
        ushort _type;
        int segmentEnd;
        double g, a, b, c, d, e, f;
        public ParametricCurve(ushort type, byte[] iccData, int index, bool isLittleEndian)
        {
            _type = type;
            g = HighEndianReader.GetS15Fixed16NumberToDouble(iccData, index, isLittleEndian);
            segmentEnd = index + 4;
            if (type == 1 || type == 2 || type == 3 || type == 4)
            {
                a = HighEndianReader.GetS15Fixed16NumberToDouble(iccData, segmentEnd, isLittleEndian);
                b = HighEndianReader.GetS15Fixed16NumberToDouble(iccData, segmentEnd+4, isLittleEndian);
                segmentEnd = segmentEnd + 4;
            }
            if (type == 2 || type == 3 || type == 4)
            {
                c = HighEndianReader.GetS15Fixed16NumberToDouble(iccData, segmentEnd, isLittleEndian);
                segmentEnd = segmentEnd + 4;
            }
            if (type == 3 || type == 4)
            {
                d = HighEndianReader.GetS15Fixed16NumberToDouble(iccData, segmentEnd, isLittleEndian);
                segmentEnd = segmentEnd + 4;
            }
            if (type == 4)
            {
                e = HighEndianReader.GetS15Fixed16NumberToDouble(iccData, segmentEnd, isLittleEndian);
                f = HighEndianReader.GetS15Fixed16NumberToDouble(iccData, segmentEnd+4, isLittleEndian);
                //segmentEnd = idx + 28;
            }
        }
        public double Function(double X)
        {
            switch (_type)
            {
                case 0:
                    return Math.Pow(X, g);
                case 1:
                    return (X >= -b / a) ? Math.Pow(a * X + b, g) : 0;
                case 2:
                    return (X >= -b / a) ? Math.Pow(a * X + b, g) + c : c;
                case 3:
                    return (X >= d) ? Math.Pow(a * X + b, g) : c * X;
                case 4:
                    return (X >= d) ? Math.Pow(a * X + b, g) + c : c * X + f;
                default:
                    return -10000;
            }
        }

        public double InverseFunction(double X)
        {
            switch (_type)
            {
                case 0:
                    return Math.Pow(X, 1 / g);
                case 1:
                    return (X >= -b / a) ? (Math.Pow(a, 1 / g) - b) / X : 0;
                case 2:
                    return (X >= -b / a) ? (Math.Pow(X - c, 1 / g) - b) / a : c;
                case 3:
                    return (X >= d) ? (Math.Pow(a, 1 / g) - b) / X : X / c;
                case 4:
                    return (X >= d) ? (Math.Pow(X - c, 1 / g) - b) / a : (X - f) / c;
                default:
                    return -10000;
            }
        }
    }
}
