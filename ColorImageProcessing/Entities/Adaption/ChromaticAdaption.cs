using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using ChormaticSpace;
using ColorImageProcessing.Core;
using ColorLib;

namespace ColorImageProcessing.Entities.Adaption
{
    internal class ChromaticAdaption : IImageProcess
    {
        public Illuminant SourceIlluminat;
        public Illuminant DestIlluminant;
        public ChromaticAdaptionMethod Method;
        public BitmapImage Apply(BitmapImage image)
        {
            int bytePerPixel = image.Format.BitsPerPixel;
            return null;
        }

        public double[][,] ApplyToArray(BitmapImage image)
        {
            throw new NotImplementedException();
        }
    }
}
