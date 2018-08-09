using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICC_Profile.DataStruct
{
    public sealed class OneDimensionalCurve// Table 55
    {
        /// <summary>
        /// Number of curve segments
        /// </summary>
        public ushort SegmentCount { get; private set; }
        /// <summary>
        /// Breakpoints separate two curve segments
        /// </summary>
        public double[] BreakPoints { get; private set; }
        /// <summary>
        /// An array of curve segments
        /// </summary>
        public CurveSegment[] Segments { get; private set; }
        internal int end;

        internal OneDimensionalCurve(byte[] iccData,int idx)
        {
            bool isLittleEndian = BitConverter.IsLittleEndian;
            //Number of segments (2 bytes) (plus 2 bytes reserved)
            SegmentCount = HighEndianReader.GetUInt16(iccData, idx, isLittleEndian);
            //Break points (4 bytes each)
            BreakPoints = new double[SegmentCount - 1];
            for (int i = 0; i < SegmentCount-1; i++)
            {
                int dataEntryPoint = idx + 4 + 4 * i;
                BreakPoints[i] = HighEndianReader.GetFloat32(iccData, dataEntryPoint, isLittleEndian);
            }
            int dataEntryForSegments = idx + 4 + SegmentCount * 4; int c = 0;
            int lastSegmentLength = 0;
            //for (int i = idx + 4; i < iend; i += 4) { BreakPoints[c] = HighEndianReader.GetFloat32(iccData,i, isLittleEndian); c++; }
            //Segments
            Segments = new CurveSegment[SegmentCount];
            //int start = iend; iend += 1; c = 0;
            for (int i = 0; i < SegmentCount; i++)
            {
                dataEntryForSegments += lastSegmentLength;
                Segments[i] = CurveSegment.GetCurve(iccData, dataEntryForSegments);
                lastSegmentLength = Segments[i].LengthInByte;
            }
            //for (int i = start; i < SegmentCount; i++) { Segments[i] = CurveSegment.GetCurve(iccData,iend); iend += Segments[i].LengthInByte;  }
            //end += iend;
        }
        public double GetValue(double input)
        {
            int idx = -1;
            if (Segments.Length != 1)
            {
                for (int i = 0; i < BreakPoints.Length; i++) { if (input <= BreakPoints[i]) { idx = i; break; } }
                if (idx == -1) { idx = Segments.Length - 1; }
            }
            else { idx = 0; }

            return Segments[idx].GetValue(input);
        }
    }
}
