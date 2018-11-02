using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using ChormaticSpace;
using ColorImageProcessing.Core;
using ColorLib;

namespace ColorImageProcessing.Entities.Adaption
{
    internal class ChromaticAdaption : IImageProcess
    {
        public static Illuminant SourceIlluminat;
        public static Illuminant DestIlluminant;
        public static ChromaticAdaptionMethod Method;
        public BitmapImage Apply(BitmapImage image)
        {
            
            int stride = image.Format.BitsPerPixel / 8 * image.PixelWidth;
            byte[] imageData = new byte[image.PixelHeight * stride];
            image.CopyPixels(imageData, stride, 0);
            byte[] newImageData = new byte[imageData.Length];
            var newImageSource = BitmapSource.Create(image.PixelWidth, image.PixelHeight, image.DpiX, image.DpiY, image.Format, image.Palette, newImageData, stride);

            newImageSource.Freeze();//newImageSource.Freeze();
            return ImageHelper.BitmapSourceToBitmapImage(newImageSource);
        }

        public double[][,] ApplyToArray(BitmapImage image)
        {
            throw new NotImplementedException();
        }
        
    }
}
