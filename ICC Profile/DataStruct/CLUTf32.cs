using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICC_Profile.DataStruct
{
    public class CLUTf32 : CLUT
    {
        public CLUTf32(byte[] iccData, int index, int inputChannels, int outputChannels, byte[] gridPoint): this(iccData, index,inputChannels, outputChannels, gridPoint,false)
        {
        }

        public double[][] Values { get; private set; }

        public CLUTf32(byte[] iccData,int idx, int InputChannelCount, int OutputChannelCount, byte[] GridPointCount, bool isLittleEndian)
        {
            this.InputChannelCount = InputChannelCount;
            this.OutputChannelCount = OutputChannelCount;
            this.GridPointCount = GridPointCount;
            //Points
            int l = 0; int k = 0;
            for (int i = 0; i < InputChannelCount; i++) { l += (int)Math.Pow(GridPointCount[i], InputChannelCount) / InputChannelCount; }
            Values = new double[l][];
            for (int i = 0; i < l; i++)
            {
                Values[i] = new double[OutputChannelCount];
                for (int o = 0; o < OutputChannelCount; o++) { Values[i][o] = HighEndianReader.GetFloat32(iccData, idx + k, isLittleEndian); k += 4; }
            }
            this.end = idx + l * OutputChannelCount * 4;
        }

        public override double[] GetValue(params double[] p)
        {
            if (p.Length != InputChannelCount) { throw new ArgumentException("Inputcount does not match channelcount"); }
            double[] pd = new double[p.Length];
            for (int i = 0; i < p.Length; i++) { pd[i] = p[i] * GridPointCount[i]; }
            double idx = pd[InputChannelCount - 1]; int c = 1;
            for (int i = InputChannelCount - 2; i >= 0; i--) { idx += Math.Round(pd[i]) * (int)Math.Pow(GridPointCount[i], c); c++; }
            return Values[(int)idx];
        }

        public override byte[] GetValue(params byte[] p)
        {
            double[] tmp = GetValue(p.Select(t => t / 255d).ToArray());
            byte[] o = new byte[tmp.Length];
            for (int i = 0; i < tmp.Length; i++) { o[i] = (byte)(tmp[i] * 255); }
            return o;
        }

        public override ushort[] GetValue(params ushort[] p)
        {
            double[] tmp = GetValue(p.Select(t => t / 65535d).ToArray());
            ushort[] o = new ushort[tmp.Length];
            for (int i = 0; i < tmp.Length; i++) { o[i] = (ushort)(tmp[i] * 65535d); }
            return o;
        }
    }
}
