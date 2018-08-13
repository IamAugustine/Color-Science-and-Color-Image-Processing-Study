using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Runtime.CompilerServices;
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


namespace ColorImageProcessing.ImageContent
{
    /// <summary>
    /// Interaction logic for ImageContent.xaml
    /// </summary>
    public partial class ImageContent: DockItem, INotifyPropertyChanged
    {
        public ImageContent()
        {
            InitializeComponent();
            DataContext = this;
        }

        private BitmapImage _image;

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public BitmapImage Image
        {
            get { return _image; }
            set { _image = value;
                NotifyPropertyChanged("Image");
            }
        }
        public void OpenFile(string fileName)
        {
            Image = ImageHelper.LoadImageSourceFromFile(fileName);
            
            //_image = ImageHelper.BitmapImage2Bitmap(image);
            imageContainer.Source = Image;
            
            

        }

    }
}
