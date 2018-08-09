using ICC_Profile.DataStruct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ICC_Profile.EnumConst;
namespace ICC_Profile 
{
    public class ICCHeader
    {
        public uint ProfileSize { get; private set; }
        public string CMMType { get; private set; }
        public VersionNumber ProfileVersionNumber { get; private set; }
        public ProfileClassName ProfileClass { get; private set; }
        public ColorSpaceType DataColorspace { get; private set; }
        public ColorSpaceType PCS { get; private set; }
        public DateTime CreationDate { get; private set; }
        public string Magic { get; private set; }
        public PrimaryPlatformName PrimaryPlatformSignature { get; private set; }
        public ProfileFlag ProfileFlags { get; private set; }
        public TagSignature DeviceManufacturer { get; private set; }
        public TagSignature DeviceModel { get; private set; }
        public DeviceAttribute DeviceAttributes { get; private set; }
        public RenderingIntentName RenderingIntent { get; private set; }
        public XYZNumber PCSIlluminant { get; private set; }
        public uint ProfileCreatorSignature { get; private set; }
        public string ProfileID { get; private set; }

        public void Read(byte[] iccProfileBytes)
        {
            bool isLittleEndian = BitConverter.IsLittleEndian;

            ProfileSize = HighEndianReader.GetUint32(iccProfileBytes, 0, isLittleEndian);
            CMMType = HighEndianReader.GetASCII(iccProfileBytes, 4, 4);
            ProfileVersionNumber = new DataStruct.VersionNumber(iccProfileBytes[8], iccProfileBytes[9]);
            ProfileClass =
                (ProfileClassName) HighEndianReader.GetUint32(iccProfileBytes, 12, isLittleEndian);
            DataColorspace =
                (ColorSpaceType) HighEndianReader.GetUint32(iccProfileBytes, 16, isLittleEndian);
            //PCS field (bytes 20 to 23)
            PCS = (ColorSpaceType) HighEndianReader.GetUint32(iccProfileBytes, 20, isLittleEndian);
            //Date and time field (bytes 24 to 35)
            CreationDate = HighEndianReader.GetDateTime(iccProfileBytes, 24, isLittleEndian);
            //Profile file signature field (bytes 36 to 39)
            Magic = HighEndianReader.GetASCII(iccProfileBytes, 36, 4);
            //Primary platform field (bytes 40 to 43)
            PrimaryPlatformSignature =
                (EnumConst.PrimaryPlatformName)
                HighEndianReader.GetUint32(iccProfileBytes, 40, isLittleEndian);
            ProfileFlags = new DataStruct.ProfileFlag(iccProfileBytes[44], iccProfileBytes[44 + 1],
                iccProfileBytes[44 + 2], iccProfileBytes[44 + 3]);
            //Device manufacturer field (bytes 48 to 51)
            DeviceManufacturer =
                (TagSignature)
                HighEndianReader.GetUint32(iccProfileBytes, 48, isLittleEndian);
            //Device model field (bytes 52 to 55)
            DeviceModel =
                (TagSignature)
                HighEndianReader.GetUint32(iccProfileBytes, 52, isLittleEndian);
            //Device attributes field (bytes 56 to 63)
            DeviceAttributes = new DeviceAttribute(iccProfileBytes, 56);
            //Rendering intent field (bytes 64 to 67) (66 and 67 are zero)
            RenderingIntent =
                (EnumConst.RenderingIntentName)
                HighEndianReader.GetUint32(iccProfileBytes, 64, isLittleEndian);
            //PCS illuminant field (Bytes 68 to 79)
            PCSIlluminant = new XYZNumber(iccProfileBytes, 68, isLittleEndian);
            //Profile creator field (bytes 80 to 83)
            ProfileCreatorSignature = HighEndianReader.GetUint32(iccProfileBytes, 80, isLittleEndian);
            //Profile ID field (bytes 84 to 99)
            ProfileID = HighEndianReader.GetString(iccProfileBytes, 84, 16);

        }

        private const int ProfileSizeIndex = 0;
        private const int CMMTypeIndex = 4;
        private const int VersionMajorIndex = 8;
        private const int VersionMinorIndex = 9;
        private const int ProfileClassIndex = 12;
        private const int DataColorspaceIndex = 16;

    }
}
