using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ICC_Profile.EnumConst;

namespace ICC_Profile.ICC_Tags
{
    class SignatureTag : ICCTagData
    {
        public override TypeSignature Signature => TypeSignature.signature;
        public string SignatureData { get; private set; }
        public override void GetTagData(byte[] iccData, int index, ICCHeader header) => SignatureData = HighEndianReader.GetASCII(iccData, index, 4);

    }
}
