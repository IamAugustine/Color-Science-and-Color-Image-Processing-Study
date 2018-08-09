using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ICC_Profile.EnumConst;

namespace ICC_Profile.DataStruct
{
    public sealed class FormulaCurveElement : CurveSegment//Table 56
    {
        /// <summary>
        /// The signature of this segment
        /// </summary>
        public override CurveSegmentSignature Signature => CurveSegmentSignature.FormulaCurve; 

        private ushort formula;
        private double gamma;
        private double a;
        private double b;
        private double c;
        private double d;
        private double e;

        public override void GetCurveData(byte[] iccData, int index, bool isLittleEndian)
        {
            //Encoded value of the function type (2 bytes) (plus 2 bytes reserved)
            formula = HighEndianReader.GetUInt16(iccData, index, isLittleEndian);
            if (formula != 1 && formula != 2 && formula != 3) { throw new CorruptProfileException("FormulaCurveElement"); }
            //Parameters (4 bytes each)
            if (formula == 0 || formula == 1) { gamma = HighEndianReader.GetFloat32(iccData, index + 4, isLittleEndian); }
            if (formula == 0 || formula == 1 || formula == 2)
            {
                a = HighEndianReader.GetFloat32(iccData, index + 8, isLittleEndian);
                b = HighEndianReader.GetFloat32(iccData, index + 12, isLittleEndian);
                c = HighEndianReader.GetFloat32(iccData, index + 16, isLittleEndian);
                //end = idx + 20;
                LengthInByte = 20;
            }
            if (formula == 1 || formula == 2) { d = HighEndianReader.GetFloat32(iccData, index + 20, isLittleEndian); ; end = index + 24; LengthInByte = 24; }
            if (formula == 2) { e = HighEndianReader.GetFloat32(iccData, index + 24, isLittleEndian); ; end = index + 24; LengthInByte = 24; }
        }

        //internal FormulaCurveElement(byte[] iccData, int idx, bool isLittleEndian)
        //{

            
        //}

        /// <summary>
        /// Interpolates a given value with the methods of this curve segment
        /// </summary>
        /// <param name="X">The value which will get interpolated</param>
        /// <returns>The interpolated value</returns>
        public override double GetValue(double X)
        {
            if (formula == 0) { return Math.Pow(a * X + b, gamma) + c; }
            else if (formula == 1) { return a * Math.Log10(b * Math.Pow(X, gamma) + c) + d; }
            else if (formula == 2) { return a * Math.Pow(b, c * X + d) + e; }
            else { return -1; }
        }
    }
}
