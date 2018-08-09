using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICC_Profile.DataStruct
{
    public class VersionNumber
    {
        public int Major;
        public int Minor;

        public VersionNumber(byte major, byte minor)
        {
            Major = major;
            Minor = minor;
        }
    }
}
