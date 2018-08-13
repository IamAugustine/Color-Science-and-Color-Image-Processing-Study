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
using ColorImageProcessing.ToolBar;
using ColorImageProcessing.View.Filter;
using DevZest.Windows.Docking;
using ICC_Profile;
using Microsoft.Win32;

namespace ColorImageProcessing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void menu_OpenImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter =
                    @"Image files(*.jpg, *.png, *.tif, *.bmp, *.gif) | *.jpg; *.png; *.tif; *.bmp; *.gif | JPG files(*.jpg) | *.jpg | PNG files(*.png) | *.png | TIF files(*.tif) | *.tif | BMP files(*.bmp) | *.bmp | GIF files(*.gif) | *.gif",
                Title = @"Open an image",
                Multiselect = false
            };

            if (ofd.ShowDialog() == true)
            {
                ImageContent.ImageContent newDoc = new ImageContent.ImageContent
                {
                    TabText = System.IO.Path.GetFileName(ofd.FileName)
                };
                newDoc.OpenFile(ofd.FileName);
                newDoc.Show(DockControlHost);
            }

        }

        private void Menu_ImageInfo_Click(object sender, RoutedEventArgs e)
        {
            var activeDoc = DockControlHost.ActiveDocument as ImageContent.ImageContent;
            ImageInfoPage infoPage = new ImageInfoPage();
            infoPage.Show(DockControlHost);
        }

        private void Menu_ICCOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "ICC Profile(*.icc)|*.icc";
            if (openFileDialog.ShowDialog() == true)
            {
                ICCProfile profile = new ICCProfile();
                profile.Read(openFileDialog.FileName);
                MessageBox.Show(profile.Header.ProfileSize.ToString());
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_CAT_Click(object sender, RoutedEventArgs e)
        {
            CATSettingWindow catSettingWin = new CATSettingWindow();
            catSettingWin.Show();
        }
    }
}
