using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICC_Profile.DataStruct
{
    public class NamedColor
    {


        public string ColorRootName { get; private set; }
        public ushort[] PCScoordinates { get; private set; }
        public ushort[] DeviceCoordinates { get; private set; }

        internal NamedColor(int idx, int DeviceCoordinateCount)
        {

        }

        public NamedColor(byte[] iccData, int index, int deviceCoordinateCount, bool isLittleEndian)
        {
            //Root name (32 bytes)
            ColorRootName = HighEndianReader.GetASCII(iccData, index, 32);
            //PCS coordinates (6 bytes) (2 bytes each)
            PCScoordinates = new ushort[3];
            PCScoordinates[0] = HighEndianReader.GetUInt16(iccData, index + 32, isLittleEndian);
            PCScoordinates[1] = HighEndianReader.GetUInt16(iccData, index + 34, isLittleEndian);
            PCScoordinates[2] = HighEndianReader.GetUInt16(iccData, index + 36, isLittleEndian);
            //Device coordinates (2 bytes each)
            if (deviceCoordinateCount > 0)
            {
                DeviceCoordinates = new ushort[deviceCoordinateCount];
                for (int i = 0; i < 2 * deviceCoordinateCount; i++)
                {
                    int dataEntryIndex = index + 38 + i*2; ;
                    DeviceCoordinates[i] = HighEndianReader.GetUInt16(iccData, dataEntryIndex, isLittleEndian);
                }
                //int end = (idx + 38) + 2 * deviceCoordinateCount; int c = 0;
                //for (int i = idx + 38; i < end; i++) { DeviceCoordinates[c] = Helper.GetUInt16(i); c++; }
            }
        }
    }
}
