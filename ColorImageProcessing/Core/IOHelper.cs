using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ColorImageProcessing.Core
{
    public class IOHelper
    {
        public static void SaveArray<T>(T[] data, string fileanme)
        {
            FileStream fs = new FileStream(fileanme, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            for (int i = 0; i < data.Length; i++)
            {
                sw.WriteLine(data[i]);
            }
            sw.Close();
            fs.Close();

        }
        public static void SaveArray<T>(T[] data, int bytePerPixel,string fileanme)
        {
            FileStream fs = new FileStream(fileanme, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            for (int i = 0; i < data.Length/bytePerPixel; i++)
            {
                string s = "";
                for (int j = 0; j < bytePerPixel; j++)
                {
                    s += (data[i * bytePerPixel + j] + "\t");
                }
                sw.WriteLine(s);
            }
            sw.Close();
            fs.Close();

        }
    }
}
