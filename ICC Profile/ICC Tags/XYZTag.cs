using ICC_Profile.DataStruct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ICC_Profile.EnumConst;
namespace ICC_Profile.ICC_Tags
{
    public class XYZTag : ICCTagData //Table 82
    {
        public override TypeSignature Signature => TypeSignature.XYZ;
        public XYZNumber[] XYZ;
        public override void GetTagData(byte[] iccData, int index, ICCHeader header)
        {
            int arrayNumber = (int)(dataSize - 8) / 3 / 4;
            XYZ = new XYZNumber[(arrayNumber) / 12]; int c = 0;
            for (int i = 0; i < arrayNumber; i++)
            {
                int dataIndex = index + 12 * i;
                XYZ[i] = new XYZNumber(iccData, dataIndex, IsLittleEndian);
            }
            
        }
    }
}
