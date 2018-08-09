using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ICC_Profile.EnumConst;

namespace ICC_Profile.ICC_Tags
{
    class uInt32ArrayTag : ICCTagData
    {
        public override TypeSignature Signature => TypeSignature.uInt32Array;

        public uint[] Data;
        public override void GetTagData(byte[] iccData, int index, ICCHeader header)
        {
            int arrayNumber = (int)(dataSize - 8) / 4;
            Data = new UInt32[arrayNumber]; 
            for (int i = 0; i < arrayNumber; i++)
            {
                int dataIndex = index + i * 4;
                Data[i] = HighEndianReader.GetUint32(iccData, dataIndex, IsLittleEndian);
            }

        }
    }
}
