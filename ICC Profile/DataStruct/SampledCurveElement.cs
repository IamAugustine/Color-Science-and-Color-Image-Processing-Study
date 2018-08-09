using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ICC_Profile.EnumConst;

namespace ICC_Profile.DataStruct
{
    public sealed class SampledCurveElement : CurveSegment//Table 58
    {
        /// <summary>
        /// The signature of this segment
        /// </summary>
        public override CurveSegmentSignature Signature => CurveSegmentSignature.SampledCurve;

        /// <summary>
        /// Number of entries
        /// </summary>
        public int EntryCount { get; private set; }
        /// <summary>
        /// The curve entries
        /// </summary>
        public double[] CurveEntries { get; private set; }
        public override void GetCurveData(byte[] iccData, int index, bool isLittleEndian)
        {
            //The number of entries (4 bytes)
            EntryCount = (int)HighEndianReader.GetUint32(iccData, index, BitConverter.IsLittleEndian);
            //Curve entries (4 bytes each)
            CurveEntries = new double[EntryCount];
            end = index + 4 + 4 * EntryCount;
            for (int i = 0; i < EntryCount; i++)
            {
                int dataEntryPoint = index + 4 + i * 4;
                CurveEntries[i] = HighEndianReader.GetFloat32(iccData, dataEntryPoint, BitConverter.IsLittleEndian);
            }
            LengthInByte = EntryCount * 4 + 4;
        }
        //internal SampledCurveElement(int idx)
        //{

        //}
        //internal SampledCurveElement(byte[] iccData,int idx)
        //{
        //    bool isLittleEndian = BitConverter.IsLittleEndian;
        //    //The number of entries (4 bytes)
        //    EntryCount = (int)HighEndianReader.GetUint32(iccData,idx, isLittleEndian);
        //    //Curve entries (4 bytes each)
        //    CurveEntries = new double[EntryCount];
        //    end = idx + 4 + 4 * EntryCount; 
        //    for (int i = 0; i < EntryCount; i++)
        //    {
        //        int dataEntryPoint = idx + 4 + i * 4;
        //        CurveEntries[i] = HighEndianReader.GetFloat32(iccData, dataEntryPoint, isLittleEndian);
        //    }
        //    LengthInByte = EntryCount * 4 + 4;
        //}
        /// <summary>
        /// Interpolates a given value with the methods of this curve segment
        /// </summary>
        /// <param name="X">The value which will get interpolated</param>
        /// <returns>The interpolated value</returns>
        public override double GetValue(double X)
        {
            double t = X * (CurveEntries.Length - 1);
            if (t % 1 != 0)
            {
                int i = (int)Math.Floor(t);
                return CurveEntries[i] + ((CurveEntries[i + 1] - CurveEntries[i]) * (t % 1));
            }
            else { return CurveEntries[(int)t]; }
        }
    }
}
