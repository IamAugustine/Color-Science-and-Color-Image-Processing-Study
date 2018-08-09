using ICC_Profile.DataStruct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ICC_Profile.EnumConst;

namespace ICC_Profile.ICC_Tags //Table 81
{
    public class ViewingConditionTag : ICCTagData
    {
        public override TypeSignature Signature => TypeSignature.viewingConditions;

        public XYZNumber UnnomalizedIlluminant { get; private set; }
        public XYZNumber UnnomalizedSurround { get; private set; }
        public StandardIlluminant Illuminant { get; private set; }
        public override void GetTagData(byte[] iccData, int index, ICCHeader header)
        {
            UnnomalizedIlluminant = new XYZNumber(iccData, index, IsLittleEndian);
            //Un-normalized CIEXYZ values for surround (12 bytes)
            UnnomalizedSurround = new XYZNumber(iccData, index+12, IsLittleEndian);
            //Standard illuminant (4 bytes)
            Illuminant = (StandardIlluminant)HighEndianReader.GetUint32(iccData, index + 24, IsLittleEndian);
        }
    }
}
