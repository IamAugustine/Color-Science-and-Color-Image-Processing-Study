using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ICC_Profile.EnumConst;

namespace ICC_Profile.ICC_Tags
{
    class s15Fixed16ArrayTag : ICCTagData
    {
        public override TypeSignature Signature => TypeSignature.s15Fixed16Array;
        public double[] Data { get; private set; }
        public override void GetTagData(byte[] iccData, int index, ICCHeader header)
        {
            int dataLength = (int)(dataSize - 8) / 4;
            Data = new double[dataLength]; 
            for (int i = 0; i < dataLength; i ++)
            {
                int dataIndex = index + i * 4;
                Data[i] = HighEndianReader.GetS15Fixed16NumberToDouble(iccData, dataIndex, IsLittleEndian) / 256d;
            }
        }
    }
}
