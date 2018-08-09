using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICC_Profile.DataStruct;
using static ICC_Profile.EnumConst;

namespace ICC_Profile.ICC_Tags
{
    public class MultiProcessElementsTag : ICCTagData
    {
        public override TypeSignature Signature => TypeSignature.multiProcessElements;

        public override void GetTagData(byte[] iccData, int index, ICCHeader header)
        {
            //Number of input channels (2 bytes)            
            InputChannelCount = HighEndianReader.GetUInt16(iccData, index);
            //Number of output channels (2 bytes)            
            OutputChannelCount = HighEndianReader.GetUInt16(iccData, index + 2);
            //Number of processing elements (4 bytes)            
            ProcessingElementCount = (int)HighEndianReader.GetUint32(iccData, index + 4);
            //Process element positions table (8 bytes each)
            PositionTable = new PositionNumber[ProcessingElementCount];
            //int end = idx + 8 + 8 * ProcessingElementCount; int c = 0;

            for (int i = 0; i < ProcessingElementCount; i++)
            {
                int dataEntryPoint = index + 8 + 8 * i;
                PositionTable[i] = new PositionNumber(iccData, dataEntryPoint);
            }

            //for (int i = idx + 8; i < end; i += 8) { PositionTable[c] = new PositionNumber(i); c++; }
            //Data
            Data = new MultiProcessElement[ProcessingElementCount];
            for (int i = 0; i < ProcessingElementCount; i++)
            {
                Data[i] = MultiProcessElement.CreateElement(iccData, PositionTable[i].Offset);
            }
        }
        public ushort InputChannelCount { get; private set; }
        public ushort OutputChannelCount { get; private set; }
        public int ProcessingElementCount { get; private set; }
        internal PositionNumber[] PositionTable { get; private set; }
        public MultiProcessElement[] Data;
        

        public double[] GetValue(double[] inColor)
        {
            double[] output = inColor;
            for (int i = 0; i < ProcessingElementCount; i++) { output = Data[i].GetValue(output); }
            return output;
        }
    }
}
