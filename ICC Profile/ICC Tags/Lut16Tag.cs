using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICC_Profile.DataStruct;
using static ICC_Profile.MathsProcess;
using static ICC_Profile.EnumConst;
namespace ICC_Profile.ICC_Tags
{
    public class Lut16Tag : ICCTagData // Table 37
    {
        public override TypeSignature Signature => TypeSignature.lut16;
        public byte InputChannelCount { get; private set; }
        public byte OutputChannelCount { get; private set; }
        public byte CLUTGridPointCount { get; private set; }
        public ushort InputTableEntryCount { get; private set; }
        public ushort OutputTableEntryCount { get; private set; }

        public double[,] Matrix { get; private set; }
        public LUT16[] InputValues { get; private set; }
        public CLUT16 CLUTValues { get; private set; }
        public LUT16[] OutputValues { get; private set; }

        public override void GetTagData(byte[] iccData, int index, ICCHeader header)
        {
            //Channel counts (1 byte each)
            InputChannelCount = iccData[index];
            OutputChannelCount = iccData[index + 1];
            CLUTGridPointCount = iccData[index + 2];
            //1 byte reserved
            //Matrix (4 bytes each)
            Matrix = HighEndianReader.GetMatrix(iccData, index + 4, 3, 3, false, IsLittleEndian);
            //Number of input table entries
            InputTableEntryCount = HighEndianReader.GetUInt16(iccData, index + 40, IsLittleEndian);
            //Number of output table entries
            OutputTableEntryCount = HighEndianReader.GetUInt16(iccData, index + 42, IsLittleEndian);
            //Input
            InputValues = new LUT16[InputChannelCount]; int c = 0;
            for (int i = 0; i < InputChannelCount; i++)
            {
                int dataIndex = InputTableEntryCount * 2 * i + 44 + index;
                InputValues[i] = new LUT16(iccData,dataIndex, InputTableEntryCount, IsLittleEndian);
                c += InputTableEntryCount * 2; }
            //CLUT
            int CLUTLength = (int)Math.Pow(CLUTGridPointCount, InputChannelCount) * OutputChannelCount * 2;
            byte[] tmp = new byte[16];
            for (int i = 0; i < 16; i++) {
                tmp[i] = CLUTGridPointCount; }
            CLUTValues = new CLUT16(iccData, index + 44 + c, InputChannelCount, OutputChannelCount, tmp);
            //Output
            OutputValues = new LUT16[OutputChannelCount];
            c = 0;
            for (int i = 0; i < OutputChannelCount; i++)
            {
                OutputValues[i] = new LUT16(iccData, CLUTValues.end + c, OutputTableEntryCount, IsLittleEndian);
                c += OutputTableEntryCount * 2; }
        }

        public double[] GetValue(double[] inColor)
        {
            if (inColor.Length != InputChannelCount) { throw new ArgumentException("Input color channel count does not match LUT channel count"); }

            //Matrix
            double[] m = MatixMultiply(Matrix, inColor);
            //Input LUT
            double[] ilut = new double[InputChannelCount];
            for (int i = 0; i < InputChannelCount; i++) { ilut[i] = InputValues[i].GetNumber(m[i]); }
            //CLUT
            double[] c = CLUTValues.GetValue(ilut);
            //Output LUT
            double[] olut = new double[OutputChannelCount];
            for (int i = 0; i < OutputChannelCount; i++) { olut[i] = OutputValues[i].GetNumber(c[i]); }

            return olut;
        }
    }
}
