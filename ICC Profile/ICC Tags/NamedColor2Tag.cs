using Accord.Math;
using ICC_Profile.DataStruct;
namespace ICC_Profile.ICC_Tags
{
    public class NamedColor2Tag : ICCTagData //Table 63
    {
        public override EnumConst.TypeSignature Signature => EnumConst.TypeSignature.namedColor2;

        public override void GetTagData(byte[] iccData, int index, ICCHeader header)
        {
            VendorSpecFlag = iccData.Get(index, index + 4);
            //VendorSpecFlag =  new byte[] { ICCProfile.DataBytes[idx], ICCProfile.DataBytes[idx + 1], ICCProfile.DataBytes[idx + 2], ICCProfile.DataBytes[idx + 3] };
            //Count of named colours (4 bytes)
            NamedColorCount = (int)HighEndianReader.GetUint32(iccData, index + 4, IsLittleEndian);
            //Number of device coordinates (4 bytes)            
            DeviceCoordinateCount = (int)HighEndianReader.GetUint32(iccData, index + 8, IsLittleEndian);
            //Prefix for each colour name
            Prefix = HighEndianReader.GetASCII(iccData, index + 12, 32);
            //Suffix for each colour name
            Suffix = HighEndianReader.GetASCII(iccData, index + 44, 32);
            //Colors
            NamedColors = new NamedColor[NamedColorCount];

            for (int i = 0; i < NamedColorCount; i++)
            {
                int dataEntryIndex = index + 76 + (38 + 2 * DeviceCoordinateCount)*i;
                NamedColors[i] = new NamedColor(iccData, dataEntryIndex, DeviceCoordinateCount, IsLittleEndian); ;
            }
        }
        public byte[] VendorSpecFlag { get; private set; }
        public int NamedColorCount { get; private set; }
        public int DeviceCoordinateCount { get; private set; }
        public string Prefix { get; private set; }
        public string Suffix { get; private set; }
        public NamedColor[] NamedColors { get; private set; }

    }
}
