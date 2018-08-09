using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICC_Profile.DataStruct;
using static ICC_Profile.EnumConst;
namespace ICC_Profile.ICC_Tags
{
    public class ProfileSequenceIdentifierTag : ICCTagData
    {
        public override TypeSignature Signature => TypeSignature.profileSequenceIdentifier;
        public int NumberCount { get; private set; }
        internal PositionNumber[] PositionTable { get; private set; }
        public string ProfileID { get; private set; }
        public MultiLocalizedUnicodeTag ProfileDescription { get; private set; }
        public override void GetTagData(byte[] iccData, int index, ICCHeader header)
        {
            //Count, specifying number of structures in the array (4 bytes)
            NumberCount = (int)HighEndianReader.GetUint32(iccData, index, IsLittleEndian);
            //Positions table for profile identifiers
            PositionTable = new PositionNumber[NumberCount];
            int dataEntryPoint = 0; ;
            for (int i = 0; i < NumberCount; i++)
            {
                dataEntryPoint = index + 4 + 8 * i;
                PositionTable[i] = new PositionNumber(iccData, i, IsLittleEndian);
            }
            //Profile ID (16 bytes)
            dataEntryPoint += 8;
            ProfileID = HighEndianReader.GetProfileID(iccData, dataEntryPoint);
            dataEntryPoint += 16;
            //Profile description
            ProfileDescription = new MultiLocalizedUnicodeTag() { IsPlaceHolder = false};
            ProfileDescription.GetTagData(iccData, dataEntryPoint, header);
            //ICCTagData t = new ICCTagData();


        }
    }
}
