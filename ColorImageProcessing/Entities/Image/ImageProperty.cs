using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ColorImageProcessing.Entities.Image
{
    public class ImageProperty
    {
        public int Height;
        public int Width;
        public double DpiX;
        public double DpiY;
        public string Camera;

        public ImageProperty(BitmapImage image)
        {
            Height = image.PixelHeight;
            Width = image.PixelWidth;
            DpiX = image.DpiX;
            DpiY = image.DpiY;
        }
    }
}
