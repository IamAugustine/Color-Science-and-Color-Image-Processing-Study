using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ICC_Profile.EnumConst;
using ICC_Profile.DataStruct;
namespace ICC_Profile.ICC_Tags
{
    class ProfileSequenceDescTag : ICCTagData
    {
        public override TypeSignature Signature => TypeSignature.profileSequenceDesc;

        public override void GetTagData(byte[] iccData, int index, ICCHeader header)
        {
            DescriptionCount = (int)HighEndianReader.GetUint32(iccData, index);
            //Profile description structures
            Descriptions = new ProfileDescription[DescriptionCount];
            int end = index + 4;
            for (int i = 0; i < DescriptionCount; i++)
            { Descriptions[i] = new ProfileDescription(iccData, end, header); end = Descriptions[i].end; }
        }
        public int DescriptionCount { get; private set; }
        public ProfileDescription[] Descriptions { get; private set; }

    }
}
