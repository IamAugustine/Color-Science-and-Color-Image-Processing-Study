using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICC_Profile.DataStruct
{
    class PositionNumber
    {
        public int Offset { get; private set; }
        public int Size { get; private set; }

        internal PositionNumber(byte[] iccData, int index, bool isLittleEndian = false)
        {
            this.Offset = (int)HighEndianReader.GetUint32(iccData, index, isLittleEndian);
            this.Size = (int)HighEndianReader.GetUint32(iccData, index+4, isLittleEndian);
        }
    }
}
