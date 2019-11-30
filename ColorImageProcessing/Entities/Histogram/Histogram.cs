using ColorImageProcessing.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ColorImageProcessing.Entities.Histogram
{
    public class Histogram
    {
        public Histogram()
        { }
        public Histogram(BitmapImage image) {
            int bytePerPixel = image.Format.BitsPerPixel / BitDepth;
            int stride = bytePerPixel * image.PixelWidth;
            byte[] imageData = new byte[image.PixelHeight * stride];
            image.CopyPixels(imageData, stride, 0);
            GetHistogram(imageData, bytePerPixel);
        }
        public Histogram(byte[] imageData, int bytePerPixel)
        {
            GetHistogram(imageData, bytePerPixel);
        }
        public double[] Data;
        public double[][] RgbData;
        public int BitDepth = 8;
        private void GetHistogram(byte[] imageData, int bytePerPixel)
        {
            RgbData = new double[bytePerPixel][];
            int pixelNumber = imageData.Length / bytePerPixel;

            for (int i = 0; i < bytePerPixel; i++)
            {
                RgbData[i] = new double[(int)Math.Pow(2,BitDepth)];
            }
            Parallel.For(0, pixelNumber, y =>
            {
                int currentIndex = y * bytePerPixel;
                for (int i = 0; i < bytePerPixel; i++)
                {
                    RgbData[i][imageData[currentIndex + i]]++;
                }
            });

        }
    }
}
