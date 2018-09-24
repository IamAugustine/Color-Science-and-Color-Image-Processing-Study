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
using ICC_Profile;

namespace ColorImageProcessing.View.ICC
{
    /// <summary>
    /// Interaction logic for ProfilerInspector.xaml
    /// </summary>
    public partial class ProfilerInspector : Window, INotifyPropertyChanged
    {
        private ICCProfile _iccProfile;

        public ProfilerInspector()
        {
            InitializeComponent();
        }
        public ProfilerInspector(ICCProfile profile)
        {
            InitializeComponent();
            DataContext = this;
            ICCProfile = profile;
        }
        public ICC_Profile.ICCProfile ICCProfile
        {
            get { return _iccProfile; }
            set { _iccProfile = value;
                NotifyPropertyChanged("ICCProfile");
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
    }

}
