using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ICC_Profile.EnumConst;
namespace ICC_Profile.ICC_Tags
{
    class uInt16ArrayTag : ICCTagData // Table 77
    {
        public override TypeSignature Signature => throw new NotImplementedException();


        public ushort[] Data;
        public override void GetTagData(byte[] iccData, int index, ICCHeader header)
        {
            int arrayNumber = (int)(dataSize - 8) / 2;
            Data = new UInt16[arrayNumber];
            for (int i = 0; i < arrayNumber; i++)
            {
                int dataIndex = index + i * 2;
                Data[i] = HighEndianReader.GetUInt16(iccData, dataIndex, IsLittleEndian);
            }

        }
    }
}
