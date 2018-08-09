using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICC_Profile.DataStruct
{
    class MatrixProcessElement : MultiProcessElement
    {
        public override EnumConst.multiProcessElementSignature Signature => throw new NotImplementedException();

        public double[,] MatrixIxO { get; private set; }
        public double[] MatrixOx1 { get; private set; }

        public override void GetElementData(byte[] iccData, int index, int inputChannel, int outputChannel, bool isLittleEndian)
        {
            this.InputChannelCount = (ushort)inputChannel;
            this.OutputChannelCount = (ushort)outputChannel;
            //Matrix IxO (4 bytes each)
            MatrixIxO = HighEndianReader.GetMatrix(iccData, index, inputChannel, outputChannel, true, BitConverter.IsLittleEndian);
            //Matrix Ox1 (4 bytes each)
            
            MatrixOx1 = HighEndianReader.GetMatrix(iccData, index + (4 * inputChannel * outputChannel), outputChannel, true);
        }
        public override double[] GetValue(double[] inColor) => MathsProcess.MatrixAdd(MathsProcess.MatixMultiply(MatrixIxO, inColor), MatrixOx1);

    }
}
