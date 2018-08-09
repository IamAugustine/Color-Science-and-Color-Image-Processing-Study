using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ICC_Profile.EnumConst;

namespace ICC_Profile.DataStruct
{
    public abstract class CurveSegment
    {
        /// <summary>
        /// The signature of this segment
        /// </summary>
        public abstract CurveSegmentSignature Signature { get; }
        internal int end;
        public int LengthInByte;
        public virtual void GetCurveData(byte[] iccData, int index, bool isLittleEndian)
        {

        }

        internal static CurveSegment GetCurve(byte[] iccData, int idx)
        {
            //Tag signature (4 bytes) (plus 4 bytes reserved)
            CurveSegment curve;
            CurveSegmentSignature t = (CurveSegmentSignature)HighEndianReader.GetUint32(iccData, idx);
            if (t == CurveSegmentSignature.FormulaCurve) {
                curve = new FormulaCurveElement();
                curve.GetCurveData(iccData, idx + 8, false);
                
                return curve;
            }
            else if 
                (t == CurveSegmentSignature.SampledCurve)
            {
                curve = new SampledCurveElement();
                curve.GetCurveData(iccData, idx + 8, false);
                return curve;
            }
            else { throw new CorruptProfileException("CurveSegment"); }
        }

        /// <summary>
        /// Interpolates a given value with the methods of this curve segment
        /// </summary>
        /// <param name="X">The value which will get interpolated</param>
        /// <returns>The interpolated value</returns>
        public abstract double GetValue(double X);
    }
}
