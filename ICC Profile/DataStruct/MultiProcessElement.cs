using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICC_Profile.DataStruct;
using static ICC_Profile.EnumConst;
namespace ICC_Profile.DataStruct
{
    public abstract class MultiProcessElement
    {
        /// <summary>
        /// The signature of this element
        /// </summary>
        public abstract multiProcessElementSignature Signature { get; }
        /// <summary>
        /// Number of input channels
        /// </summary>
        public ushort InputChannelCount { get; protected set; }
        /// <summary>
        /// Number of output channels
        /// </summary>
        public ushort OutputChannelCount { get; protected set; }

        internal static MultiProcessElement CreateElement(byte[] iccData, int idx, bool isLittleEndian = false)
        {
            //Tag signature (byte position 0 to 3) (4 to 7 are zero)
            multiProcessElementSignature t = (multiProcessElementSignature)HighEndianReader.GetUint32(iccData,idx);
            //Number of input channels (2 bytes)
            ushort InputChannelCount = HighEndianReader.GetUInt16(iccData,idx + 8);
            //Number of output channels (2 bytes)
            ushort OutputChannelCount = HighEndianReader.GetUInt16(iccData, idx + 10, isLittleEndian);
            return GetElement(t, iccData, idx + 12, InputChannelCount, OutputChannelCount);
        }

        public virtual void GetElementData(byte[] iccData, int index ,int inputChannel, int outChannle, bool isLittleEndian)
        { }

        private static MultiProcessElement GetElement(multiProcessElementSignature type, byte[] iccData, int idx, ushort InputChannelCount, ushort OutputChannelCount)
        {
            MultiProcessElement elem = null;
            switch (type)
            {
                case multiProcessElementSignature.CurveSet:
                    elem = new CurveSetProcessElement();
                    break;
                case multiProcessElementSignature.CLUT:
                    elem = new CLUTProcessElement();
                    break;
                case multiProcessElementSignature.Matrix:
                    elem = new MatrixProcessElement();
                    break;
                case multiProcessElementSignature.bACS:
                    elem =new bACSProcessElement();
                    break;
                case multiProcessElementSignature.eACS:
                    elem = new eACSProcessElement();
                    break;
                default:
                    throw new CorruptProfileException("MultiProcessElement");
            }
            elem.GetElementData(iccData, idx,InputChannelCount, OutputChannelCount, BitConverter.IsLittleEndian);
            return elem;
        }

        public abstract double[] GetValue(double[] inColor);
    }
}
