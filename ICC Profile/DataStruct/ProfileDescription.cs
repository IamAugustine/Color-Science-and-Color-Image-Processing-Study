using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICC_Profile.ICC_Tags;
using static ICC_Profile.EnumConst;

namespace ICC_Profile.DataStruct
{
    public class ProfileDescription
    {
        public uint DeviceManufacturer { get; private set; }
        public uint DeviceModel { get; private set; }
        public DeviceAttribute DeviceAttributes { get; private set; }
        public TagSignature TechnologyInformation { get; private set; }
        public MultiLocalizedUnicodeTag DeviceManufacturerInfo { get; private set; }
        public MultiLocalizedUnicodeTag DeviceModelInfo { get; private set; }
        public int LengthInByte;
        internal int end;
        public ProfileDescription(byte[] iccData, int index, ICCHeader header)
        {
            DeviceManufacturer = HighEndianReader.GetUint32(iccData, index);
            //Device model signature (4 bytes)
            DeviceModel = HighEndianReader.GetUint32(iccData, index + 4);
            //Device attributes (8 bytes)
            DeviceAttributes = new DeviceAttribute(iccData, index + 8);
            //Device technology information (4 bytes)
            TechnologyInformation = (TagSignature)HighEndianReader.GetUint32(iccData, index + 16);
            //Displayable description of device manufacturer (profile's deviceMfgDescTag)
            DeviceManufacturerInfo = new MultiLocalizedUnicodeTag() { IsPlaceHolder = header.DeviceManufacturer == 0 };
            DeviceManufacturerInfo.GetTagData(iccData, index + 20, header);
            //Displayable description of device model (profile's deviceModelDescTag)
            DeviceModelInfo = new MultiLocalizedUnicodeTag() { IsPlaceHolder = header.DeviceModel == 0 };
            DeviceModelInfo.GetTagData(iccData, DeviceManufacturerInfo.LengthInByte+index+20, header);
            //end = DeviceModelInfo.end;
        }
    }
}
