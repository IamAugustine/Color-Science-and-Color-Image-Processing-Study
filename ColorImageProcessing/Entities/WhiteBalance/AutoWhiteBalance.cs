using ColorImageProcessing.Core;
using ColorLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ColorImageProcessing.Entities.WhiteBalance
{
    public abstract class AutoWhiteBalance
    {
        public abstract byte[] WhiteBalance(byte[] imageData, List<object> processParam = null);
    }
    public class GreyWorld : AutoWhiteBalance, IImageProcess
    {
        public BitmapImage Apply(BitmapImage image)
        {
            int stride = image.Format.BitsPerPixel / 8 * image.PixelWidth;
            byte[] imageData = new byte[image.PixelHeight * stride];
            image.CopyPixels(imageData, stride, 0);
            byte[] inverseImageData = WhiteBalance(imageData);
            var newImageSource = BitmapSource.Create(image.PixelWidth, image.PixelHeight, image.DpiX, image.DpiY, image.Format, image.Palette, inverseImageData, stride);

            newImageSource.Freeze();//newImageSource.Freeze();
            return ImageHelper.BitmapSourceToBitmapImage(newImageSource);
        }

        public double[][,] ApplyToArray(BitmapImage image)
        {
            throw new NotImplementedException();
        }

        public override byte[] WhiteBalance(byte[] imageData, List<object> param = null)
        {
            throw new NotImplementedException();
            
        }
    }
}
