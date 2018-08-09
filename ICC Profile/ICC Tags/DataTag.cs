using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICC_Profile.ICC_Tags
{
    public class DataTag : ICCTagData
    {
        public override EnumConst.TypeSignature Signature => EnumConst.TypeSignature.data;

        public override void GetTagData(byte[] iccData, int index, ICCHeader header)
        {
            uint f = HighEndianReader.GetUInt16(iccData, index, IsLittleEndian);
            IsASCII = f == 0;
            //Data
            Data = new byte[dataSize - 12];
            Array.Copy(iccData, index + 4, Data, 0, dataSize - 12);
        }
        public byte[] Data { get; private set; }
        public bool IsASCII { get; private set; }
    }
}
