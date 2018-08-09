using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICC_Profile.ICC_Tags
{
    class u16Fixed16ArrayTag : ICCTagData
    {
        public override EnumConst.TypeSignature Signature => EnumConst.TypeSignature.u16Fixed16Array;
        public double[] Data { get; private set; }
        public override void GetTagData(byte[] iccData, int index, ICCHeader header)
        {
            int dataLength = (int)(dataSize - 8) / 4;
            Data = new double[dataLength];
            for (int i = 0; i < dataLength; i++)
            {
                int dataIndex = index + i * 4;
                Data[i] = HighEndianReader.GetU16Fixed16NumberToDouble(iccData, dataIndex, IsLittleEndian) ;
            }
        }
    }
}
