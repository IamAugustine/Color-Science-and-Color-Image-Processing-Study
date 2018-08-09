using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICC_Profile.ICC_Tags
{
    class DateTimeTag : ICCTagData
    {
        public override EnumConst.TypeSignature Signature => EnumConst.TypeSignature.dateTime;
        public DateTime DateAndTime { get; private set; }
        public override void GetTagData(byte[] iccData, int index, ICCHeader header) => DateAndTime = HighEndianReader.GetDateTime(iccData, index, IsLittleEndian);
    }
}
