using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICC_Profile.DataStruct
{
    public class CLUT8: CLUT
    {
        internal int end;
        private int _dataEntryPoint;
        public CLUT8(byte[] iccData, int v, int inputChannels, int outputChannels, byte[] gridPoint) : this(iccData, v, (byte)inputChannels, (byte)outputChannels, gridPoint, false)
        {
        }

        public CLUT8(byte[] iccData, int index, byte inputChannelCount, byte outputChannelCount, byte[] gridPoint, bool isLittleEndian)
        {
            this.InputChannelCount = inputChannelCount;
            this.OutputChannelCount = outputChannelCount;
            this.GridPointCount = gridPoint;
            //Points
            int l = 0;
            for (int i = 0; i < InputChannelCount; i++) { l += (int)Math.Pow(GridPointCount[i], InputChannelCount) / InputChannelCount; }
            Values = new byte[l][];
            for (int i = 0; i < l; i++)
            {
                Values[i] = new byte[OutputChannelCount];
                for (int o = 0; o < OutputChannelCount; o++) { Values[i][o] = iccData[index + l]; }
            }
            this.end = index + l * OutputChannelCount;
        }

      
        public byte[][] Values { get; private set; }

        //public CLUT8(int idx, int InputChannelCount, int OutputChannelCount, byte[] GridPointCount)
        //{
 
        //}

        public override byte[] GetValue(params byte[] p)
        {
            if (p.Length != InputChannelCount) { throw new ArgumentException("Inputcount does not match channelcount"); }
            double[] pd = new double[p.Length];
            
            for (int i = 0; i < p.Length; i++) { pd[i] = (p[i] != 0) ? (p[i] / 255d) * GridPointCount[i] : 0; }
            double idx = pd[InputChannelCount - 1]; int c = 1;
            for (int i = InputChannelCount - 2; i >= 0; i--) { idx += Math.Round(pd[i]) * (int)Math.Pow(GridPointCount[i], c); c++; }
            return Values[(int)idx];
        }

        public override ushort[] GetValue(params ushort[] p)
        {
            ushort[] tmp = GetValue(p.Select(t => (ushort)(t / 257d)).ToArray());
            ushort[] o = new ushort[tmp.Length];
            for (int i = 0; i < tmp.Length; i++) { o[i] = (ushort)(tmp[i] * 257d); }
            return o;
        }

        public override double[] GetValue(params double[] p)
        {
            byte[] tmp = GetValue(p.Select(t => (byte)(t * 255)).ToArray());
            double[] o = new double[tmp.Length];
            for (int i = 0; i < tmp.Length; i++) { o[i] = tmp[i] / 255d; }
            return o;
        }
    }
}
