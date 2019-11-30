using DevZest.Windows.Docking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ColorImageProcessing.Entities;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ColorImageProcessing.View.Info
{
    /// <summary>
    /// Interaction logic for HistogramViewPage.xaml
    /// </summary>
    public partial class HistogramViewPage : DockItem, INotifyPropertyChanged
    {
        public HistogramViewPage()
        {
            InitializeComponent();
        }
        public List<string> ColorChannels = new List<string> { "RGB", "R", "G", "B", "Color" };
        public BitmapImage Image { set { _image = value; UpdateHistogram(_image); } get { return _image; } }
        private BitmapImage _image;

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void UpdateHistogram(BitmapImage image)
        {
            Entities.Histogram.Histogram h = new Entities.Histogram.Histogram(image);
        }

    }
}
