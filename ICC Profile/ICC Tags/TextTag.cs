using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICC_Profile.ICC_Tags
{
    public class TextTag : ICCTagData
    {
        public override EnumConst.TypeSignature Signature => EnumConst.TypeSignature.text;
        public string Text { get; private set; }
        public override void GetTagData(byte[] iccData, int index, ICCHeader header) => Text = HighEndianReader.GetString(iccData, index, (int)dataSize - 8);
    }
}
