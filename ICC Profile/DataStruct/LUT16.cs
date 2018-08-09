using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICC_Profile.DataStruct
{
    public class LUT16
    {
        public ushort[] Values { get; private set; }

        public LUT16(byte[] iccData, int index, int ValueCount, bool isLittleEndian)
        {
            Values = new ushort[ValueCount];
            for (int i = 0; i < ValueCount; i++)
            {
                int dataIndex = index + i * 2;
                Values[i] = HighEndianReader.GetUInt16(iccData, dataIndex, isLittleEndian);
            }
        }
        public double GetNumber(ushort number)
        {
            double t = (number / 65535d) * (Values.Length - 1);
            t = (t > Values.Length - 1) ? Values.Length - 1 : (t < 0) ? 0 : t;
            if (t % 1 != 0)
            {
                int i = (int)Math.Floor(t);
                if (i + 1 < Values.Length) return (Values[i] + ((Values[i + 1] - Values[i]) * (t % 1))) / (double)ushort.MaxValue;
                else return Values[i] / (double)ushort.MaxValue;
            }
            else { return Values[(int)t] / (double)ushort.MaxValue; }
        }

        public double GetNumber(double number)
        {
            number *= (Values.Length - 1);
            number = (number > Values.Length - 1) ? Values.Length - 1 : (number < 0) ? 0 : number;
            if (number % 1 != 0)
            {
                int i = (int)Math.Floor(number);
                if (i + 1 < Values.Length) return (Values[i] + ((Values[i + 1] - Values[i]) * (number % 1))) / (double)ushort.MaxValue;
                else return Values[i] / (double)ushort.MaxValue;
            }
            else { return Values[(int)number] / (double)ushort.MaxValue; }
        }
    }
}

