using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICC_Profile.DataStruct
{
    public sealed class ProfileFlag
    {
        public byte[] Data { get; private set; }

        public bool IsEmbedded { get; private set; }
        public bool IsIndependent { get; private set; }

        internal ProfileFlag(byte data1, byte data2, byte isEmbedded, byte isIndependent)
        {
            this.Data = new byte[2] { data1, data2 };
            //BitArray a = new BitArray(new byte[] { ICCProfile.DataBytes[idx + 2], ICCProfile.DataBytes[idx + 3] });
            IsEmbedded = Convert.ToBoolean(isEmbedded);
            IsIndependent = !Convert.ToBoolean(isIndependent);
        }
    }
}
