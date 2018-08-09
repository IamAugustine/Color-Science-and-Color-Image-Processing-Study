using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ICC_Profile.EnumConst;
namespace ICC_Profile.ICC_Tags
{
    public class uInt64ArrayTag : ICCTagData
    {
        public override TypeSignature Signature => TypeSignature.uInt64Array;
        public ulong[] Data;
        public override void GetTagData(byte[] iccData, int index, ICCHeader header)
        {
            int arrayNumber = (int)(dataSize - 8) / 8;
            Data = new UInt64[arrayNumber]; int c = 0;
            for (int i = 0; i < arrayNumber; i++)
            {
                int dataIndex = index + i * 8;
                Data[i] = HighEndianReader.GetUint64(iccData, dataIndex, IsLittleEndian);
            }
            
        }
    }
}
