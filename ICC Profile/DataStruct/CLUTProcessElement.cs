using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICC_Profile.DataStruct
{
    class CLUTProcessElement : MultiProcessElement
    {
        public override EnumConst.multiProcessElementSignature Signature => throw new NotImplementedException();
        public CLUTf32 CLUTvalues;
        public override void GetElementData(byte[] iccData, int index, int inputChannel, int outputChannel, bool isLittleEndian)
        {
            InputChannelCount = (ushort)inputChannel;
            this.OutputChannelCount = (ushort)outputChannel;
            //CLUT
            CLUTvalues = (CLUTf32)CLUT.GetCLUT(iccData,index, true, InputChannelCount, OutputChannelCount);
        }
        public override double[] GetValue(double[] inColor) => CLUTvalues.GetValue(inColor);
    }
}
