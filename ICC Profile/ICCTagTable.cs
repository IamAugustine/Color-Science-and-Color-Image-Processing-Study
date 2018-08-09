using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICC_Profile
{
    public class ICCTagTable
    {
        private int TagOffset = 128;
        public uint TagCount;
        private readonly bool isLittleEndian = BitConverter.IsLittleEndian;
        public List<ICCTag> Tags;
        public void Read(byte[] iccData)
        {
            TagCount = HighEndianReader.GetUint32(iccData, TagOffset, isLittleEndian);
            Tags = new List<ICCTag>((int)TagCount);
            for (int i = 0; i < TagCount; i++)
            {
                int startIndex = TagOffset + 4 + i*12;
                uint sig = HighEndianReader.GetUint32(iccData, startIndex, isLittleEndian);
                uint offset = HighEndianReader.GetUint32(iccData, startIndex + 4, isLittleEndian);
                uint size = HighEndianReader.GetUint32(iccData, startIndex + 8, isLittleEndian);
                ICCTag tag = new ICCTag(sig, offset, size);
                Tags.Add(tag);
            }
        }
        
    }

    public class ICCTag
    {
        public EnumConst.TagSignature Signature;
        public uint Offset;
        public uint Size;

        public ICCTag(uint sig, uint offset, uint size)
        {
            Signature = (EnumConst.TagSignature)sig;
            Offset = offset;
            Size = size;
        }
    }



}
