using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ICC_Profile.EnumConst;

namespace ICC_Profile.DataStruct
{
    public sealed class CurveSetProcessElement : MultiProcessElement// Table 54
    {
        /// <summary>
        /// The signature of this element
        /// </summary>
        public override multiProcessElementSignature Signature => multiProcessElementSignature.CurveSet;
        /// <summary>
        /// An array with one dimensional curves
        /// </summary>
        public OneDimensionalCurve[] Curves { get; private set; }
        internal PositionNumber[] CurvePositions { get; private set; }

        //internal CurveSetProcessElement(int idx, ushort InputChannelCount, ushort OutputChannelCount)
        //{

        //}

        public override void GetElementData(byte[] iccData, int index, int inputChannel, int outputChannel, bool isLittleEndian)
        {
            this.InputChannelCount = (ushort)inputChannel;
            this.OutputChannelCount = (ushort)outputChannel;
            //Curves
            Curves = new OneDimensionalCurve[InputChannelCount]; int end = index;
            for (int i = 0; i < InputChannelCount; i++)
            {
                Curves[i] = new OneDimensionalCurve(iccData, end);
                end = Curves[i].end + Curves[i].end % 4;
            }
        }

        public override double[] GetValue(double[] inColor)
        {
            if (inColor.Length != InputChannelCount) { throw new ArgumentException("Input color channel count does not match element channel count"); }

            double[] output = new double[InputChannelCount];
            for (int i = 0; i < InputChannelCount; i++) { output[i] = Curves[i].GetValue(inColor[i]); }
            return output;
        }
    }
}
