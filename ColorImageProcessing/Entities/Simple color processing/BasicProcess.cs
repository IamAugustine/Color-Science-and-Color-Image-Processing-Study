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

namespace ColorImageProcessing.Entities.Simple_color_processing
{
    class BasicProcess
    {
    }
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
}
