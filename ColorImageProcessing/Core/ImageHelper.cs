using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ColorImageProcessing.Core
{
    public class ImageHelper
    {
        public static Bitmap BitmapImage2Bitmap(BitmapImage bitmapImage)
        {

            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(outStream);

                return new Bitmap(bitmap);
            }
        }
        public static BitmapImage LoadImageSourceFromFile(string fileName)
        {
            var loadedImagebitmap = new BitmapImage();
            FileStream stream = null;

            try
            {
                // read image to temporary memory stream
                // (.NET locks any stream until bitmap is disposed,
                // so that is why this work around is required to prevent file locking)
                stream = File.OpenRead(fileName);
                MemoryStream memoryStream = new MemoryStream();

                byte[] buffer = new byte[10000];
                while (true)
                {
                    int read = stream.Read(buffer, 0, 10000);

                    if (read == 0)
                        break;

                    memoryStream.Write(buffer, 0, read);
                }

                stream.Seek(0, SeekOrigin.Begin);
                loadedImagebitmap.BeginInit();
                loadedImagebitmap.StreamSource = stream;
                loadedImagebitmap.CacheOption = BitmapCacheOption.OnLoad;
                loadedImagebitmap.EndInit();
                loadedImagebitmap.Freeze();
                //loadedImage = (BitmapSource)Bitmap.FromStream(memoryStream);
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                    stream.Dispose();
                }
            }

            return loadedImagebitmap;
            ;
        }
        public static Bitmap LoadImageFromFile(string fileName)
        {
            Bitmap loadedImage = null;
            FileStream stream = null;

            try
            {
                // read image to temporary memory stream
                // (.NET locks any stream until bitmap is disposed,
                // so that is why this work around is required to prevent file locking)
                stream = File.OpenRead(fileName);
                MemoryStream memoryStream = new MemoryStream();

                byte[] buffer = new byte[10000];
                while (true)
                {
                    int read = stream.Read(buffer, 0, 10000);

                    if (read == 0)
                        break;

                    memoryStream.Write(buffer, 0, read);
                }

                loadedImage = (Bitmap)Bitmap.FromStream(memoryStream);
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                    stream.Dispose();
                }
            }

            return loadedImage;
        }
    }
}
