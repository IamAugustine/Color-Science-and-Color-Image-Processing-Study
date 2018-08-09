using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ICC_Profile.EnumConst;
namespace ICC_Profile.ICC_Tags
{
    public class uInt8ArrayTag : ICCTagData // Table 80
    {
        public override TypeSignature Signature => TypeSignature.uInt8Array;
        public byte[] Data;
        public override void GetTagData(byte[] iccData, int index, ICCHeader header)
        {
            Data = new byte[(int)dataSize - 8];
            Array.Copy(iccData, index, Data, 0, dataSize - 8);
        }
    }
}
