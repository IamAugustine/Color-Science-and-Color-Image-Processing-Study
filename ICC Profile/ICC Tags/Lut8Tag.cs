using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ICC_Profile.EnumConst;
using ICC_Profile.DataStruct;

namespace ICC_Profile.ICC_Tags
{
    public class Lut8Tag : ICCTagData //Table 41
    {
        public override TypeSignature Signature => TypeSignature.lut8;
        public byte InputChannelCount { get; private set; }// i
        public byte OutputChannelCount { get; private set; }//o
        public byte CLUTGridPointCount { get; private set; }//g
        private const int Lut8Length = 256;
        public double[,] Matrix { get; private set; }// e1- e9
        public LUT8[] InputValues { get; private set; }
        public CLUT8 CLUTValues { get; private set; }
        public LUT8[] OutputValues { get; private set; }
        public override void GetTagData(byte[] iccData, int index, ICCHeader header)
        {
            InputChannelCount = iccData[index];
            OutputChannelCount = iccData[index+1];
            CLUTGridPointCount = iccData[index+2];
            //1 byte reserved
            //Matrix (4 bytes each)
            Matrix = HighEndianReader.GetMatrix(iccData, index+4, 3, 3,  false, IsLittleEndian);
            //Input
            InputValues = new LUT8[InputChannelCount]; int c = 0;
            for (int i = 0; i < InputChannelCount; i++)
            {
                int dataIndex = index + 40 + Lut8Length * i;
                InputValues[i] = new LUT8(iccData, dataIndex); ;
            }
            //CLUT
            byte[] tmp = new byte[16];

            for (int i = 0; i < 16; i++)
            {
                tmp[i] = CLUTGridPointCount;
            }
            CLUTValues = new CLUT8(iccData, index + 40 + (256 * InputChannelCount), InputChannelCount, OutputChannelCount, tmp, IsLittleEndian);
            //Output
            int clut8Length = (int)Math.Pow(CLUTGridPointCount, InputChannelCount) * OutputChannelCount;
            OutputValues = new LUT8[OutputChannelCount]; c = 0;
            for (int i = 0; i < OutputChannelCount; i++)
            {
                OutputValues[i] = new LUT8(iccData, CLUTValues.end + c);
                c += 256;
            }
        }
    }
}
