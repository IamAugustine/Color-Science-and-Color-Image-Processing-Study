using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICC_Profile.ICC_Tags
{
    public class CurveTag : ICCTagData
    {
        public override EnumConst.TypeSignature Signature => EnumConst.TypeSignature.curve;

        public override void GetTagData(byte[] iccData, int index, ICCHeader header)
        {
            CurvePointCount = HighEndianReader.GetUint32(iccData,index, IsLittleEndian);
            CurveData = new double[CurvePointCount];
            if (CurvePointCount == 1)
            {
                CurveData[0] = HighEndianReader.GetU8Fixed8NumberToDouble(iccData, index + 4, IsLittleEndian);
                //end = index + 6;
            }
            else
            {
                for (int i = 0; i < CurvePointCount; i++)
                {
                    int startIndex = index + 4 + 2 * i;
                    CurveData[i] = HighEndianReader.GetUInt16(iccData, startIndex, IsLittleEndian) / 65535d;
                }
                //int c = 0; end = (idx + 4) + (int)CurvePointCount * 2;
            }
        }
        public uint CurvePointCount { get; private set; }
        public double[] CurveData { get; private set; }
        //internal int end;

        public double GetValue(double number)
        {
            double t = number * (CurveData.Length - 1);
            if (t % 1 != 0)
            {
                int i = (int)Math.Floor(t);
                return CurveData[i] + ((CurveData[i + 1] - CurveData[i]) * (t % 1));
            }
            else { return CurveData[(int)t]; }
        }

        public double GetValueInverted(double number)
        {
            int i = 0;
            while (i < CurveData.Length && number > CurveData[i]) { i++; }
            if (CurveData[i] == number) { return i / CurveData.Length; }
            else if (i > 0) { return (i - 1 + ((CurveData[i] - number) / (CurveData[i] - CurveData[i - 1]))) / CurveData.Length; }
            else { return 0; }
        }
    }
}
