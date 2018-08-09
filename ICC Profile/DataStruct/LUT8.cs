using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICC_Profile.DataStruct
{
    public class LUT8
    {
        public byte[] Values { get; private set; }
        public const int Lut8Length = 256;
        public double GetNumber(byte number)
        {
            double t = (number / 255d) * (Values.Length - 1);
            t = (t > Values.Length - 1) ? Values.Length - 1 : (t < 0) ? 0 : t;
            if (t % 1 != 0)
            {
                int i = (int)Math.Floor(t);
                if (i + 1 < Values.Length) return (Values[i] + ((Values[i + 1] - Values[i]) * (t % 1))) / (double)byte.MaxValue;
                else return Values[i] / (double)byte.MaxValue;
            }
            else { return Values[(int)t] / (double)byte.MaxValue; }
        }

        public double GetNumber(double number)
        {
            number *= (Values.Length - 1);
            number = (number > Values.Length - 1) ? Values.Length - 1 : (number < 0) ? 0 : number;
            if (number % 1 != 0)
            {
                int i = (int)Math.Floor(number);
                if (i + 1 < Values.Length) return Values[i] + ((Values[i + 1] - Values[i]) * (number % 1)) / (double)byte.MaxValue;
                else return Values[i] / (double)byte.MaxValue;
            }
            else { return Values[(int)number] / (double)byte.MaxValue; }
        }

        public LUT8(byte[] iccData, int index)
        {
            Values = new byte[Lut8Length];
            Array.Copy(iccData, index, Values, 0, Lut8Length);
        }
    }
}
