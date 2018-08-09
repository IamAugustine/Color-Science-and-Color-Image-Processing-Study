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

namespace ColorImageProcessing.ToolBar
{
    /// <summary>
    /// Interaction logic for ToolbarContainer.xaml
    /// </summary>
    public partial class ToolbarContainer : Page
    {
        public ToolbarContainer()
        {
            InitializeComponent();
            InitializeToolBar();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public void InitializeToolBar()
        {

        }
    }
}
