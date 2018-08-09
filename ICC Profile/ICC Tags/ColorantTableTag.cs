using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ICC_Profile.EnumConst;

namespace ICC_Profile.ICC_Tags
{
    public class ColorantTableTag: ICCTagData
    {
        public uint ColorantCount { get; private set; }
        public ColorantData[] ColorantData { get; private set; }
        
        public override TypeSignature Signature => TypeSignature.colorantTable;



        public override void GetTagData(byte[] iccData, int index, ICCHeader header)
        {
            ColorantCount = HighEndianReader.GetUint32(iccData, index, IsLittleEndian);
            //Number of the colorant to be printed first
            ColorantData = new ColorantData[ColorantCount];
            for (int i = 0; i < ColorantCount; i++)
            {
                //Colorant name (32 bytes)
                int startIndex = index + 4 + i * 38;
                string name = HighEndianReader.GetASCII(iccData, startIndex, 32);
                //PCS values (6 bytes (2 bytes each))
                ushort pcs1 = HighEndianReader.GetUInt16(iccData, startIndex + 32, IsLittleEndian);
                ushort pcs2 = HighEndianReader.GetUInt16(iccData,startIndex+34, IsLittleEndian);
                ushort pcs3 = HighEndianReader.GetUInt16(iccData, startIndex + 36, IsLittleEndian);
            }
        }
    }
    public class ColorantData
    {
        public string Name { get; private set; }
        public ushort[] PCSValue { get; private set; }

        internal ColorantData(string Name, ushort[] PCSValue)
        {
            this.Name = Name;
            this.PCSValue = PCSValue;
        }
    }
}
