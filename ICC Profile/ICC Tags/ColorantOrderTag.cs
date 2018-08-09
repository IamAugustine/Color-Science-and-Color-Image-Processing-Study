using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ICC_Profile.EnumConst;

namespace ICC_Profile.ICC_Tags
{
    public class ColorantOrderTag: ICCTagData
    {
        public override void GetTagData(byte[] iccData, int index, ICCHeader header)
        {
            ColorantCount = HighEndianReader.GetUint32(iccData, index, IsLittleEndian);
            ColorantNumber = new byte[ColorantCount];
            for (int i = 1; i < ColorantCount; i++)
            {
                int startIndex = index + 4 + i;
                ColorantNumber[i] = iccData[startIndex];
            }
        }
        public uint ColorantCount { get; private set; }
        public byte[] ColorantNumber { get; private set; }
        //private bool isLittleEndian = BitConverter.IsLittleEndian;
        public override TypeSignature Signature => TypeSignature.colorantOrder;
    }

}

