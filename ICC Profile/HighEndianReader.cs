using Accord.Math;
using System;
using System.Text;

namespace ICC_Profile
{
    public static class HighEndianReader
    {
        // the length of type
        private const int Uint32Length = 4;
        private const int Uint16Length = 2;
        private const int Uint64Length = 8;
        private const int Float32Length = 4;
        private const int DateTimeLength = 6;
        private const int S15Fixed16Number = 8;

        public static bool IsLittleEndian = BitConverter.IsLittleEndian;
        public static uint GetUint32(byte[] iccData, int index, bool isLittleEndian = false)
        {
            byte[] temp = iccData.Get(index, index + Uint32Length);
            byte[] byteToRead = isLittleEndian ? ByteReverse(temp) : temp;
            return BitConverter.ToUInt32(byteToRead,0);

        }

        internal static ulong GetUint64(byte[] iccData, int index, bool isLittleEndian)
        {
            byte[] temp = iccData.Get(index, index + Uint64Length);
            byte[] byteToRead = isLittleEndian ? ByteReverse(temp) : temp;
            return BitConverter.ToUInt64(byteToRead, 0);
        }

        internal static double GetU8Fixed8NumberToDouble(byte[] iccData, int index, bool isLittleEndian = true)
        {
            int itgr = iccData[index];
            double dcml = iccData[index] / 256d;
            return itgr + dcml;
        }

        public static string GetASCII(byte[] iccData, int index, int length) => Encoding.ASCII.GetString(iccData.Get(index, index + length));

        public static string GetString(byte[] iccData, int index, int length ) => BitConverter.ToString(iccData, index, length);

        public static string GetProfileID(byte[] iccData, int index) => BitConverter.ToString(iccData, index, 16);

        private static byte[] ByteReverse(byte[] dataIn) => dataIn.Reversed();
 
        public static double[,] GetMatrix(byte[] iccData, int index, int width, int height,  bool isFloating, bool isLittleEndian)
        {
            double[,] Matrix = new double[height, width]; //int c = 0;
            for (int y = 0; y < width; y++)
            {
                for (int x = 0; x < height; x++)
                {
                    int dataIndex = index + (x * width + y) * Float32Length;
                    if (isFloating)
                    {
                        Matrix[x, y] = GetFloat32(iccData, dataIndex, isLittleEndian);
                    }
                    else
                    {
                        Matrix[x, y] = GetS15Fixed16NumberToDouble(iccData, dataIndex, isLittleEndian) / 256d;
                    }
                   // c += 4;
                }
            }
            return Matrix;
        }
        public static double[] GetMatrix(byte[] iccData, int index, int length, bool isFloat, bool isLittleEndian = false)
        {
            //Matrix 1, y (4 bytes each)
            double[] Matrix = new double[length]; int c = 0;
            for (int i = 0; i < length; i++)
            {
                int dataEntryPoint = index + 4 * i;
                Matrix[i] = isFloat? GetFloat32(iccData, dataEntryPoint): GetS15Fixed16NumberToDouble(iccData, dataEntryPoint) / 256d; 
            }
            return Matrix;
        }
        public static DateTime GetDateTime(byte[] iccData, int index, bool isLittleEndian)
        {
            uint year = GetUInt16(iccData, index, isLittleEndian);
            uint month = GetUInt16(iccData, index+2, isLittleEndian);
            uint day = GetUInt16(iccData, index+4, isLittleEndian);
            uint hour = GetUInt16(iccData, index+6, isLittleEndian);
            uint minute = GetUInt16(iccData, index+8, isLittleEndian);
            uint second = GetUInt16(iccData, index+10, isLittleEndian);

            return new DateTime((int)year, (int)month, (int)day, (int)hour, (int)minute, (int)second);
        }

        public static ushort GetUInt16(byte[] iccData, int index, bool isLittleEndian = true)
        {
            byte[] temp = iccData.Get(index, index + Uint16Length);
            byte[] byteToRead = isLittleEndian ? ByteReverse(temp) : temp;
            return BitConverter.ToUInt16(byteToRead, 0);
        }
        public static int GetInt16(byte[] iccData, int index, bool isLittleEndian = true)
        {
            byte[] temp = iccData.Get(index, index + Uint16Length);
            byte[] byteToRead = isLittleEndian ? ByteReverse(temp) : temp;
            return BitConverter.ToUInt16(byteToRead, 0);
        }
        public static double GetS15Fixed16NumberToDouble(byte[] iccData, int index, bool isLittleEndian = true)
        {
            byte[] buffer = isLittleEndian? new[] { iccData[index+1], iccData[index] }:new[] { iccData[index], iccData[index + 1] };
            int itgr = BitConverter.ToInt16(buffer,0);
            double dcml = GetUInt16(iccData, index + 2, isLittleEndian)/65536d;
            return itgr + Math.Sign(itgr)*dcml;

        }
        public static double GetU15Fixed16NumberToDouble(byte[] iccData, int index, bool isLittleEndian = true)
        {
            int itgr = GetUInt16(iccData, index + 2, isLittleEndian);
            double dcml = GetUInt16(iccData, index + 2, isLittleEndian)/66536d;
            return itgr + dcml;
        }

        public static double GetU16Fixed16NumberToDouble(byte[] iccData, int index, bool isLittleEndian = true)
        {
            int itgr = GetUInt16(iccData, index, isLittleEndian);
            double dcml = GetUInt16(iccData, index + 2, isLittleEndian)/65536d;
            return itgr + dcml;
        }

        public static double GetFloat32(byte[] iccData, int index, bool isLittleEndian = true)
        {
            byte[] temp = iccData.Get(index, index + Uint32Length);
            byte[] byteToRead = isLittleEndian ? ByteReverse(temp) : temp;
            return BitConverter.ToSingle(byteToRead, 0);
        }
        internal static string GetUnicodeString(byte[] iccData, int idx, int length) => Encoding.BigEndianUnicode.GetString(iccData, idx, length);
    }
}
