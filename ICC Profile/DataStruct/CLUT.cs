using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICC_Profile.DataStruct
{
    public abstract class CLUT // Table 60
    {
        public byte[] GridPointCount { get; protected set; }
        public int InputChannelCount { get; protected set; }
        public int OutputChannelCount { get; protected set; }

        internal int end;

        public static CLUT GetCLUT(byte[] iccData, int idx, bool IsFloat, int InputChannels, int OutputChannels)
        {
            //Number of grid points in each dimension
            byte[] gridPoint = new byte[16];
            Array.Copy(iccData, idx, gridPoint, 0, 16);
            //for (int i = 0; i < 16; i++) { gridPoint[i] = ICCProfile.DataBytes[idx + i]; }
            //Precision of data elements
            if (!IsFloat)
            {
                byte p = iccData[idx + 16];
                if (p == 1)
                {
                    return new CLUT8(iccData, idx + 20, InputChannels, OutputChannels, gridPoint);
                }
                else if (p == 2)
                {
                    return new CLUT16(iccData, idx + 20, InputChannels, OutputChannels, gridPoint);
                }
                else { return null; }
            }
            else
            {
                return new CLUTf32(iccData, idx + 16, InputChannels, OutputChannels, gridPoint);
            }
        }

        public abstract double[] GetValue(params double[] p);
        public abstract ushort[] GetValue(params ushort[] p);
        public abstract byte[] GetValue(params byte[] p);
    }
}
