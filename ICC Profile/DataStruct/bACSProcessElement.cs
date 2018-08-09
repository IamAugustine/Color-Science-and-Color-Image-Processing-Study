using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICC_Profile.DataStruct
{
    class bACSProcessElement : MultiProcessElement//Table 61
    {
        public override EnumConst.multiProcessElementSignature Signature => EnumConst.multiProcessElementSignature.bACS;

        public override double[] GetValue(double[] inColor)
        {
            throw new NotImplementedException();
        }
    }
}
