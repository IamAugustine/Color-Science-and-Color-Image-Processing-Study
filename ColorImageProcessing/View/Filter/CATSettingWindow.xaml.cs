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
using System.Windows.Shapes;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ColorLib;
namespace ColorImageProcessing.View.Filter
{
    /// <summary>
    /// Interaction logic for CATSettingWindow.xaml
    /// </summary>
    public partial class CATSettingWindow : Window, INotifyPropertyChanged
    {
        public CATSettingWindow()
        {
            InitializeComponent();
            DataContext = this;

        }
        private Illuminant _destIlluminant;
        public Illuminant DestIlluminant
        {
            get { return _destIlluminant; }
            set { _destIlluminant = value;
                NotifyPropertyChanged("DestIlluminant");
                
            }
        }
        private Illuminant _srcIlluminant;
        public Illuminant SourceIlluminant
        {
            get { return _srcIlluminant; }
            set
            {
                _srcIlluminant = value;
                NotifyPropertyChanged("SourceIlluminant");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void cbBoxFrom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            KnownIlluminant src = (KnownIlluminant)cbBoxFrom.SelectedIndex;
            SourceIlluminant = Illuminant.GetIlluminant(src);
        }

        private void cbBoxTo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            KnownIlluminant dest = (KnownIlluminant)cbBoxTo.SelectedIndex;
            DestIlluminant = Illuminant.GetIlluminant(dest);
        }
    }
}
