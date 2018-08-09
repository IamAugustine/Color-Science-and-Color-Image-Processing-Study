using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICC_Profile.DataStruct
{
    public class CLUT16 : CLUT
    {
        public CLUT16(byte[] iccData, int index, int inputChannels, int outputChannels, byte[] gridPoint, bool isLittleEndian = false)
        {
            this.InputChannelCount = inputChannels;
            this.OutputChannelCount = outputChannels;
            this.GridPointCount = gridPoint;
            //Points
            int l = 0; int k = 0;
            for (int i = 0; i < InputChannelCount; i++)
            {
                l += (int)Math.Pow(GridPointCount[i], InputChannelCount) / InputChannelCount;
            }
            Values = new ushort[l][];
            for (int i = 0; i < l; i++)
            {
                Values[i] = new ushort[OutputChannelCount];
                for (int o = 0; o < OutputChannelCount; o++) { Values[i][o] = HighEndianReader.GetUInt16(iccData,index + k, isLittleEndian); k += 2; }
            }
            this.end = index + l * OutputChannelCount * 2;
        }

        public ushort[][] Values { get; private set; }

        public CLUT16(int idx, int InputChannelCount, int OutputChannelCount, byte[] GridPointCount)
        {

        }

        public override ushort[] GetValue(params ushort[] p)
        {
            if (p.Length != InputChannelCount) { throw new ArgumentException("Inputcount does not match channelcount"); }
            double[] pd = new double[p.Length];
            for (int i = 0; i < p.Length; i++) { pd[i] = (p[i] != 0) ? (p[i] / 65535d) * GridPointCount[i] - 1 : 0; }
            double idx = pd[InputChannelCount - 1]; int c = 1;
            for (int i = InputChannelCount - 2; i >= 0; i--) { idx += Math.Round(pd[i]) * (int)Math.Pow(GridPointCount[i], c); c++; }
            return Values[(int)idx];
        }

        public override byte[] GetValue(params byte[] p)
        {
            ushort[] tmp = GetValue(p.Select(t => (ushort)(t * 257)).ToArray());
            byte[] o = new byte[tmp.Length];
            for (int i = 0; i < tmp.Length; i++) { o[i] = (byte)(tmp[i] / 257d); }
            return o;
        }

        public override double[] GetValue(params double[] p)
        {
            ushort[] tmp = GetValue(p.Select(t => (ushort)(t * 65535)).ToArray());
            double[] o = new double[tmp.Length];
            for (int i = 0; i < tmp.Length; i++) { o[i] = tmp[i] / 65535d; }
            return o;
        }
    }
}
