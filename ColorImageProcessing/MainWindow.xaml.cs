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
using ColorImageProcessing.ImageDoc;
using ColorImageProcessing.Entities.Simple_color_processing;
using ColorImageProcessing.Core;
using ColorImageProcessing.Entities.Histogram;

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
        private View.Info.HistogramViewPage histogramViewPage;
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
                ImageDoc.ImageContent newDoc = new ImageContent
                {
                    TabText = System.IO.Path.GetFileName(ofd.FileName)
                };
                newDoc.OpenFile(ofd.FileName);
                newDoc.Show(DockControlHost);
            }

        }

        private void Menu_ImageInfo_Click(object sender, RoutedEventArgs e)
        {
            var activeDoc = DockControlHost.ActiveDocument as ImageContent;
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

        private void toolBar_undo_Click(object sender, RoutedEventArgs e)
        {
            if (DockControlHost.ActiveDocument is ImageContent)
            {
                var Doc = (ImageContent)DockControlHost.ActiveDocument;
                Doc.Undo();
            }
        }

        private void MenuItem_test_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void RunProceesing(IImageProcess process)
        {
            if (DockControlHost.ActiveDocument is ImageContent)
            {
                var Doc = (ImageContent)DockControlHost.ActiveDocument;
                Doc.RunProcessing(process);
            }
        }

        private void toolBar_redo_Click(object sender, RoutedEventArgs e)
        {
            if (DockControlHost.ActiveDocument is ImageContent)
            {
                var Doc = (ImageContent)DockControlHost.ActiveDocument;
                Doc.Redo();
            }
        }

        private void MenuItem_Inverse_Click(object sender, RoutedEventArgs e)
        {
            RunProceesing(new ColorInverse());
        }

        private void MenuItem_HistEqlz_Click(object sender, RoutedEventArgs e)
        {
            RunProceesing(new HistogramEqualization());
        }

        private void Menu_SaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveAsImage();
        }

        private void SaveAsImage()
        {
            if (DockControlHost.ActiveDocument is ImageContent)
            {
                var Doc = (ImageContent)DockControlHost.ActiveDocument;
                SaveFileDialog sfdiag = new SaveFileDialog()
                {
                    Filter =
                    @"Image files(*.jpg, *.png, *.tif, *.bmp, *.gif) | *.jpg; *.png; *.tif; *.bmp; *.gif | JPG files(*.jpg) | *.jpg | PNG files(*.png) | *.png | TIF files(*.tif) | *.tif | BMP files(*.bmp) | *.bmp | GIF files(*.gif) | *.gif",
                    Title = @"Save As",
                };
                if (sfdiag.ShowDialog() == true)
                {
                    string filename = sfdiag.FileName;

                }
            }
        }

        private void Menu_ImageHistogram_Click(object sender, RoutedEventArgs e)
        {
            if (DockControlHost.ActiveDocument is ImageContent)
            {
                var Doc = (ImageContent)DockControlHost.ActiveDocument;
            }
        }
    }
}
