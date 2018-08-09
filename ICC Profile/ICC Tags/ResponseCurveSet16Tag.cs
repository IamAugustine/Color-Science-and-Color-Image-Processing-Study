using ICC_Profile.DataStruct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ICC_Profile.EnumConst;
namespace ICC_Profile.ICC_Tags
{
    class ResponseCurveSet16Tag : ICCTagData
    {
        public override TypeSignature Signature => TypeSignature.responseCurveSet16;
        //public override TypeSignature Signature { get { return TypeSignature.responseCurveSet16; } }
        public ushort ChannelCount { get; private set; }
        public ushort MeasurmentTypesCount { get; private set; }
        public ResponseCurve[] Curves { get; private set; }
        private int[] Offset;
        public override void GetTagData(byte[] iccData, int index, ICCHeader header)
        {
            //Number of channels (2 bytes)
            ChannelCount = HighEndianReader.GetUInt16(iccData, index, IsLittleEndian);
            //Count of measurement types (2 bytes)
            MeasurmentTypesCount = HighEndianReader.GetUInt16(iccData, index+2, IsLittleEndian);
            //Offsets (4 bytes each)
            Offset = new int[MeasurmentTypesCount];
            int dataEntryPoint;
            for (int i = 0; i < MeasurmentTypesCount; i++)
            {
                dataEntryPoint = index + 4 + i * 4;
                Offset[i] = (int)HighEndianReader.GetUint32(iccData, dataEntryPoint, IsLittleEndian);
            }
            //int end = idx + 4 + 4 * MeasurmentTypesCount; int c = 0;
            //for (int i = idx + 4; i < end; i += 4) { Offset[c] = (int)Helper.GetUInt32(i); c++; }
            //Response curve structures
            Curves = new ResponseCurve[MeasurmentTypesCount];
            for (int i = 0; i < MeasurmentTypesCount; i++)
            {
                dataEntryPoint = index - 8 + Offset[i];
                Curves[i] = new ResponseCurve(iccData, dataEntryPoint, ChannelCount, IsLittleEndian); }
        }
    }
}
