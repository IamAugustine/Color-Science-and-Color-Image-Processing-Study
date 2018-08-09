using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICC_Profile.DataStruct
{
    public class XYZNumber
    {
        public double[] XYZ;
        public double  X, Y, Z;
        public XYZNumber(byte[] arr, int index, bool isLittleEndian)
        {
            X = HighEndianReader.GetS15Fixed16NumberToDouble(arr, index, isLittleEndian);
            Y = HighEndianReader.GetS15Fixed16NumberToDouble(arr, index + 4, isLittleEndian);
            Z = HighEndianReader.GetS15Fixed16NumberToDouble(arr, index + 8, isLittleEndian);
            XYZ = new double[3] { X, Y, Z };
        }
    }
}
