using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICC_Profile.DataStruct;
using static ICC_Profile.EnumConst;

namespace ICC_Profile.ICC_Tags
{
    public class LutBToATag : ICCTagData
    {
        public override EnumConst.TypeSignature Signature => TypeSignature.lutBToA;
        public byte InputChannelCount { get; private set; }
        public byte OutputChannelCount { get; private set; }
        public double[,] Matrix3x3 { get; private set; }
        public double[] Matrix3x1 { get; private set; }
        public CLUT CLUTValues { get; private set; }
        public ICCTagData[] CurveB => curveB;
        public ICCTagData[] CurveM => curveM;
        public ICCTagData[] CurveA => curveA;

        private int BCurveOffset;
        private int MatrixOffset;
        private int MCurveOffset;
        private int CLUTOffset;
        private int ACurveOffset;
        private ICCTagData[] curveB;
        private ICCTagData[] curveM;
        private ICCTagData[] curveA;

        public override void GetTagData(byte[] iccData, int index, ICCHeader header)
        {
            throw new NotImplementedException();
        }
    }
}
