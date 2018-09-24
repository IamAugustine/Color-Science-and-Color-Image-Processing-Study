using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using ColorImageProcessing.Core;
using static ColorImageProcessing.Core.GlobalVaribles;
using Accord.Math;
using System.Windows.Media;
using System.IO;
using System.Runtime.InteropServices;

namespace ColorImageProcessing.Entities.Simple_color_processing
{
    //class BasicProcess
    //{
    //}
    public class ColorInverse : IImageProcess
    {
        public BitmapImage Apply(BitmapImage image)
        {
            int stride = image.Format.BitsPerPixel/8 * image.PixelWidth;
            byte[] imageData = new byte[image.PixelHeight * stride];
            image.CopyPixels(imageData, stride, 0);
            byte[] inverseImageData = imageData.Convert(x => (byte)(255 - x));
            var newImageSource = BitmapSource.Create(image.PixelWidth, image.PixelHeight, image.DpiX, image.DpiY, image.Format, image.Palette, inverseImageData, stride);

            newImageSource.Freeze();//newImageSource.Freeze();
            return ImageHelper.BitmapSourceToBitmapImage(newImageSource);
        }

        public double[][,] ApplyToArray(BitmapImage image)
        {
            throw new NotImplementedException();
        }
    }
    public class HistogramEqualization : IImageProcess
    {
        const int ColorDepth = 256;
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
                double[] histgRed = new double[ColorDepth];
                double[] histgGreen = new double[ColorDepth];
                double[] histgBlue = new double[ColorDepth];


                Parallel.For(0, pixelNumber, y =>
                {
                    int currentIndex = y * bytePerPixel;
                    histgBlue[imageData[currentIndex + 0]]++;
                    histgGreen[imageData[currentIndex + 1]]++;
                    histgRed[imageData[currentIndex + 2]]++;
                });

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
