using ColorImageProcessing.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ColorImageProcessing.Entities.Auto_adjustment
{
    public class AutoContrast : IImageProcess
    {
        public BitmapImage Apply(BitmapImage image)
        {
            int bytePerPixel = image.Format.BitsPerPixel / 8;
            int stride = bytePerPixel * image.PixelWidth;
            byte[] imageData = new byte[image.PixelHeight * stride];
            image.CopyPixels(imageData, stride, 0);

            byte[] inverseImageData = ContrastEnhance(imageData, image.PixelHeight, image.PixelWidth, bytePerPixel);
            var newImageSource = BitmapSource.Create(image.PixelWidth, image.PixelHeight, image.DpiX, image.DpiY, image.Format, image.Palette, inverseImageData, stride);

            newImageSource.Freeze();//newImageSource.Freeze();
            return ImageHelper.BitmapSourceToBitmapImage(newImageSource);
        }
        private byte[] ContrastEnhance(byte[] imageData, int height, int width, int bytePerPixel, double lowPercentile = 0.05, double highPercentile = 0.95)
        {
            
            int pixelNumber = height * width;
            byte[] newImageData = new byte[imageData.GetLength(0)];
            Parallel.For(0, pixelNumber, y =>
            {
            });
            return newImageData;
        }
        public double[][,] ApplyToArray(BitmapImage image)
        {
            throw new NotImplementedException();
        }
    }
}
