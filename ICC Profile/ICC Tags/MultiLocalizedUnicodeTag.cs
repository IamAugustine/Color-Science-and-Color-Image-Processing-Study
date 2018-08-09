using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ICC_Profile.EnumConst;
using ICC_Profile.DataStruct;
namespace ICC_Profile.ICC_Tags
{
    public class MultiLocalizedUnicodeTag : ICCTagData
    {
        public override TypeSignature Signature => TypeSignature.multiLocalizedUnicode;
        public int RecordCount { get; private set; }
        public LocalizedString[] Text { get; private set; }
        public bool IsPlaceHolder;
        private int RecordSize;
        private string[] LanguageCode;  //ISO 639-1
        private string[] CountryCode;   //ISO 3166-1
        private int[] StringLength;

        //public MultiLocalizedUnicodeTag(bool isPlaceHolder)
        //{
        //    IsPlaceHolder = isPlaceHolder;
        //}
        public override void GetTagData(byte[] iccData, int index, ICCHeader header)
        {
            if (!IsPlaceHolder)
            {
                //Number of records (4 bytes)
                RecordCount = (int)HighEndianReader.GetUint32(iccData, index, IsLittleEndian);
                //Record size (has to be 12 as for V4.3) (4 bytes)
                RecordSize = (int)HighEndianReader.GetUint32(iccData, index+4, IsLittleEndian);
                //Records
                LanguageCode = new string[RecordCount];
                CountryCode = new string[RecordCount];
                StringLength = new int[RecordCount];
                StringOffset = new int[RecordCount];
                //int end = idx + 8 + RecordCount * RecordSize; int c = 0;

                for (int i = 0; i < RecordCount; i++)
                {
                    int dataEntryPoint = index + 8 + RecordSize * i;
                    LanguageCode[i] = HighEndianReader.GetASCII(iccData, dataEntryPoint, 2);
                    //Country Code (2 bytes)
                    CountryCode[i] = HighEndianReader.GetASCII(iccData, dataEntryPoint+2, 2);
                    //Record string length (4 bytes)
                    StringLength[i] = (int)HighEndianReader.GetUint32(iccData, dataEntryPoint+4, IsLittleEndian);
                    //Record offset (4 bytes)
                    StringOffset[i] = (int)HighEndianReader.GetUint32(iccData, dataEntryPoint + 8, IsLittleEndian); ;
                }
                //The strings
                Text = new LocalizedString[RecordCount];
                for (int i = 0; i < RecordCount; i++)
                {
                    Text[i] = new LocalizedString(LanguageCode[i], CountryCode[i], iccData, index - 8 + StringOffset[i], StringLength[i]);
                }
            }
        }
        private int[] StringOffset;
        //internal int end;
    }
}
