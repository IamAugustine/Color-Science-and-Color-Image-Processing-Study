using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICC_Profile.DataStruct;
using static ICC_Profile.EnumConst;
namespace ICC_Profile.ICC_Tags
{
    public class LutAToBTag : ICCTagData
    {
        public override TypeSignature Signature => TypeSignature.lutAToB;
        public byte InputChannelCount { get; private set; }
        public byte OutputChannelCount { get; private set; }
        public double[,] Matrix3x3 { get; private set; }
        public double[] Matrix3x1 { get; private set; }
        public CLUT CLUTValues { get; private set; }
        public ICCTagData[] CurveB => curveB;
        public ICCTagData[] CurveM => curveM;
        public ICCTagData[] CurveA => curveA;

        private int BCurveOffset;
        private int MatrixOffset;
        private int MCurveOffset;
        private int CLUTOffset;
        private int ACurveOffset;
        private ICCTagData[] curveB;
        private ICCTagData[] curveM;
        private ICCTagData[] curveA;

        public override void GetTagData(byte[] iccData, int index, ICCHeader header)
        {
            //Number of Input Channels (1 byte)
            InputChannelCount = iccData[index];
            //Number of Output Channels (1 byte)
            OutputChannelCount = iccData[index +1];
            //Reserved for padding (2 bytes)
            //Offset to first "B" curve (4 bytes)
            BCurveOffset = (int)HighEndianReader.GetUint32(iccData, index + 4, IsLittleEndian);
            //Offset to matrix (4 bytes)
            MatrixOffset = (int)HighEndianReader.GetUint32(iccData, index + 8, IsLittleEndian);
            MCurveOffset = (int)HighEndianReader.GetUint32(iccData, index + 12, IsLittleEndian);
            //Offset to CLUT (4 bytes)
            CLUTOffset = (int)HighEndianReader.GetUint32(iccData, index + 16, IsLittleEndian);
            //Offset to first "A" curve (4 bytes)
            ACurveOffset = (int)HighEndianReader.GetUint32(iccData, index + 20, IsLittleEndian);

            //Curves
            if (BCurveOffset != 0) { GetCurve(iccData, ACurveOffset, ref curveB, index, InputChannelCount); }
            if (MCurveOffset != 0) { GetCurve(iccData, ACurveOffset, ref curveM, index, InputChannelCount); }
            if (CLUTOffset != 0) { CLUTValues = CLUT.GetCLUT(iccData, index - 8 + CLUTOffset, false, InputChannelCount, OutputChannelCount); }
            if (ACurveOffset != 0) { GetCurve(iccData, ACurveOffset, ref curveA, index, InputChannelCount); }

            //Matrix
            if (MatrixOffset != 0)
            {
                int i = MatrixOffset + index - 8;
                //Matrix 3x3 (4 bytes each)
                Matrix3x3 = HighEndianReader.GetMatrix(iccData, 3, 3, i, false, IsLittleEndian);
                //Matrix 3x1 (4 bytes each)
                Matrix3x1 = HighEndianReader.GetMatrix(iccData, 3, i+36, false, IsLittleEndian);
            }
        }

        private static void GetCurve(byte[] iccData, int Offset, ref ICCTagData[] data, int idx, int InputChannelCount)
        {
            //Signature (4 bytes + 4 bytes reserved)
            TypeSignature t = (TypeSignature)HighEndianReader.GetUint32(iccData, Offset, BitConverter.IsLittleEndian);
            if (t != TypeSignature.curve && t != TypeSignature.parametricCurve) { return ; }
            //Curve
            data = new ICCTagData[InputChannelCount];
            int end = idx + Offset;
            for (int i = 0; i < InputChannelCount; i++)
            {
                if (t == TypeSignature.curve)
                {
                    data[i] = new CurveTag();
                    data[i].GetTagData(iccData, idx, null);
                    //end = ((CurveTag)data[i]).end;
                }
                else if (t == TypeSignature.parametricCurve)
                {
                    data[i] = new ParametricCurveTag();
                    data[i].GetTagData(iccData, idx, null);
                    //end = ((ParametricCurveTagDataEntry)data[i]).end;
                }
                end += (end % 4) + 8;
            }
        }

        public double[] GetValue(double[] inColor)
        {
            if (inColor.Length != InputChannelCount) { throw new ArgumentException("Input color channel count does not match LUT channel count"); }

            if (ACurveOffset != 0 && CLUTOffset != 0 && MCurveOffset != 0 && MatrixOffset != 0 && BCurveOffset != 0) { return GetValue_A_CLUT_M_Matrix_B(inColor); }
            else if (ACurveOffset != 0 && CLUTOffset != 0 && BCurveOffset != 0) { return GetValue_A_CLUT_B(inColor); }
            else if (MCurveOffset != 0 && MatrixOffset != 0 && BCurveOffset != 0) { return GetValue_M_Matrix_B(inColor); }
            else if (BCurveOffset != 0) { return GetValue_B(inColor); }
            else { return null; }
        }

        private double[] GetValue_B(double[] inColor)
        {
            double[] output = new double[OutputChannelCount];
            for (int i = 0; i < InputChannelCount; i++)
            {
                if (CurveB[i].Signature == TypeSignature.curve) { output[i] = ((CurveTag)CurveB[i]).GetValue(inColor[i]); }
                else if (CurveB[i].Signature == TypeSignature.parametricCurve) { output[i] = ((ParametricCurveTag)CurveB[i]).Curve.Function(inColor[i]); }
                else { output = null; }
            }
            return output;
        }

        private double[] GetValue_M_Matrix_B(double[] inColor)
        {
            //M Curves
            double[] m = new double[InputChannelCount];
            for (int i = 0; i < InputChannelCount; i++)
            {
                if (CurveM[i].Signature == TypeSignature.curve) { m[i] = ((CurveTag)CurveM[i]).GetValue(inColor[i]); }
                else if (CurveM[i].Signature == TypeSignature.parametricCurve) { m[i] = ((ParametricCurveTag)CurveM[i]).Curve.Function(inColor[i]); }
                else { return null; }
            }

            //Matrix
            double[] t = MathsProcess.MatixMultiply(Matrix3x3, m);

            //B Curves
            double[] output = new double[OutputChannelCount];
            for (int i = 0; i < OutputChannelCount; i++)
            {
                if (CurveB[i].Signature == TypeSignature.curve) { output[i] = ((CurveTag)CurveB[i]).GetValue(t[i]); }
                else if (CurveB[i].Signature == TypeSignature.parametricCurve) { output[i] = ((ParametricCurveTag)CurveB[i]).Curve.Function(t[i]); }
                else { return null; }
            }
            return output;
        }

        private double[] GetValue_A_CLUT_B(double[] inColor)
        {
            //A Curves
            double[] a = new double[InputChannelCount];
            for (int i = 0; i < InputChannelCount; i++)
            {
                if (CurveA[i].Signature == TypeSignature.curve) { a[i] = ((CurveTag)CurveA[i]).GetValue(inColor[i]); }
                else if (CurveA[i].Signature == TypeSignature.parametricCurve) { a[i] = ((ParametricCurveTag)CurveA[i]).Curve.Function(inColor[i]); }
                else { return null; }
            }

            //CLUT
            double[] c = CLUTValues.GetValue(a);

            //B Curves
            double[] output = new double[OutputChannelCount];
            for (int i = 0; i < OutputChannelCount; i++)
            {
                if (CurveB[i].Signature == TypeSignature.curve) { output[i] = ((CurveTag)CurveB[i]).GetValue(c[i]); }
                else if (CurveB[i].Signature == TypeSignature.parametricCurve) { output[i] = ((ParametricCurveTag)CurveB[i]).Curve.Function(c[i]); }
                else { return null; }
            }
            return output;
        }

        private double[] GetValue_A_CLUT_M_Matrix_B(double[] inColor)
        {
            //A Curves
            double[] a = new double[InputChannelCount];
            for (int i = 0; i < InputChannelCount; i++)
            {
                if (CurveA[i].Signature == TypeSignature.curve) { a[i] = ((CurveTag)CurveA[i]).GetValue(inColor[i]); }
                else if (CurveA[i].Signature == TypeSignature.parametricCurve) { a[i] = ((ParametricCurveTag)CurveA[i]).Curve.Function(inColor[i]); }
                else { return null; }
            }

            //CLUT
            double[] c = CLUTValues.GetValue(a);

            //M Curves
            double[] m = new double[OutputChannelCount];
            for (int i = 0; i < OutputChannelCount; i++)
            {
                if (CurveM[i].Signature == TypeSignature.curve) { m[i] = ((CurveTag)CurveM[i]).GetValue(c[i]); }
                else if (CurveM[i].Signature == TypeSignature.parametricCurve) { m[i] = ((ParametricCurveTag)CurveM[i]).Curve.Function(c[i]); }
                else { return null; }
            }

            //Matrix
            double[] t = MathsProcess.MatixMultiply(Matrix3x3, m);

            //B Curves
            double[] output = new double[OutputChannelCount];
            for (int i = 0; i < OutputChannelCount; i++)
            {
                if (CurveB[i].Signature == TypeSignature.curve) { output[i] = ((CurveTag)CurveB[i]).GetValue(t[i]); }
                else if (CurveB[i].Signature == TypeSignature.parametricCurve) { output[i] = ((ParametricCurveTag)CurveB[i]).Curve.Function(t[i]); }
                else { return null; }
            }
            return output;
        }
    }
}
