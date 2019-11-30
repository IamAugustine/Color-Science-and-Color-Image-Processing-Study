using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorImageProcessing.Core
{
    public class GlobalVaribles
    {
        public enum RGBChannels
        {
            B,
            G,
            R,
            Alpah,
        }
        public enum BitDepth
        {
            Bit8 = 8,
            Bit9 = 9,
            Bit10 = 10,
            Bit11 = 11,
            Bit12 = 12,
            Bit13 = 13,
            Bit14 = 14,
            Bit15 = 15,
            Bit16 = 16,
        }
    }
}
