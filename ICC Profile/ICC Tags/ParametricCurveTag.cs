using ICC_Profile.DataStruct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ICC_Profile.EnumConst;

namespace ICC_Profile.ICC_Tags
{
    class ParametricCurveTag : ICCTagData
    {
        public override TypeSignature Signature => TypeSignature.parametricCurve;
        public ParametricCurve Curve { get; private set; }

        private ushort FunctionType;
        //internal int end;
        public override void GetTagData(byte[] iccData, int index, ICCHeader header)
        {
            FunctionType = HighEndianReader.GetUInt16(iccData, index, IsLittleEndian);
            //2 bytes reserved
            Curve = new ParametricCurve(FunctionType, iccData, index, IsLittleEndian);
        }
    }
}
