using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using ColorImageProcessing.Core;
using DevZest.Windows.Docking;
using PixelFormat = System.Windows.Media.PixelFormat;
using Point = System.Windows.Point;
using Rectangle = System.Windows.Shapes.Rectangle;

namespace ColorImageProcessing.ImageContent
{
    /// <summary>
    /// Interaction logic for ImageContent.xaml
    /// </summary>
    public partial class ImageContent: DockItem
    {
        public ImageContent()
        {
            InitializeComponent();
        }

        private Bitmap _image;
        public void OpenFile(string fileName)
        {
            BitmapImage image = ImageHelper.LoadImageSourceFromFile(fileName);
            
            _image = ImageHelper.BitmapImage2Bitmap(image);
            imageContainer.Source = image;
            

        }

    }
}
