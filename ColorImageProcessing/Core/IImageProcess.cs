using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ColorImageProcessing.Core
{
    public interface IImageProcess
    {
        BitmapImage Apply(BitmapImage image);
        double[][,] ApplyToArray(BitmapImage image); 
    }
}
