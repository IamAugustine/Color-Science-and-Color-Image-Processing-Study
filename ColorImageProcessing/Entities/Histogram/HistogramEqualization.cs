using ColorImageProcessing.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ColorImageProcessing.Entities.Histogram
{
    public class HistogramEqualization : IImageProcess
    {
        //const int ColorDepth = 256;
        public BitmapImage Apply(BitmapImage image)
        {
            int bytePerPixel = image.Format.BitsPerPixel / 8;
            int stride = bytePerPixel * image.PixelWidth;
            byte[] imageData = new byte[image.PixelHeight * stride];
            image.CopyPixels(imageData, stride, 0);

            byte[] inverseImageData = HistEq(imageData, image.PixelHeight, image.PixelWidth, bytePerPixel);
            var newImageSource = BitmapSource.Create(image.PixelWidth, image.PixelHeight, image.DpiX, image.DpiY, image.Format, image.Palette, inverseImageData, stride);

            newImageSource.Freeze();//newImageSource.Freeze();
            return ImageHelper.BitmapSourceToBitmapImage(newImageSource);
        }
        private byte[] HistEq(byte[] imageData, int height, int width, int bytePerPixel)
        {
            int pixelNumber = height * width;
            byte[] newImageData = new byte[imageData.GetLength(0)];
            unsafe
            {
                Histogram histogram = new Histogram(imageData, bytePerPixel);
                double[] histgBlue = histogram.RgbData[(int)GlobalVaribles.RGBChannels.B];
                double[] histgGreen = histogram.RgbData[(int)GlobalVaribles.RGBChannels.G];
                double[] histgRed = histogram.RgbData[(int)GlobalVaribles.RGBChannels.R];

                int ColorDepth = (int)Math.Pow(2, histogram.BitDepth);

                byte[] newRed = new byte[ColorDepth];
                byte[] newGreen = new byte[ColorDepth];
                byte[] newBlue = new byte[ColorDepth];

                double sumBlue, sumGreen, sumRed;
                sumBlue = sumGreen = sumRed = 0;
                double crfScale = ((double)ColorDepth - 1) / pixelNumber;
                for (int i = 0; i < ColorDepth; i++)
                {
                    sumBlue += histgBlue[i];
                    sumGreen += histgGreen[i];
                    sumRed += histgRed[i];
                    newBlue[i] = (byte)(sumBlue * crfScale);
                    newGreen[i] = (byte)(sumGreen * crfScale);
                    newRed[i] = (byte)(sumRed * crfScale);
                }
                Array.Copy(imageData, newImageData, imageData.Length);
                Parallel.For(0, height * width, y =>
                {
                    int currentIndex = y * bytePerPixel;
                    newImageData[currentIndex + 0] = newBlue[imageData[currentIndex + 0]];
                    newImageData[currentIndex + 1] = newGreen[imageData[currentIndex + 1]];
                    newImageData[currentIndex + 2] = newRed[imageData[currentIndex + 2]];
                });
            }
            return newImageData;
        }
        public double[][,] ApplyToArray(BitmapImage image)
        {
            throw new NotImplementedException();
        }
    }
}
