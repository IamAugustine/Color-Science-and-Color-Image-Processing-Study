using ICC_Profile.DataStruct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ICC_Profile.EnumConst;

namespace ICC_Profile.ICC_Tags
{
    class MeasurementTag : ICCTagData
    {
        public override TypeSignature Signature => TypeSignature.measurement;

        public override void GetTagData(byte[] iccData, int index, ICCHeader header)
        {
            Observer = (StandardObserver)HighEndianReader.GetUint32(iccData, index, IsLittleEndian);
            //nCIEXYZ tristimulus values for measurement backing
            XYZBacking = new XYZNumber(iccData, index + 4, IsLittleEndian);
            //Measurement geometry (4 bytes)
            Geometry = (MeasurementGeometry)HighEndianReader.GetUint32(iccData, index, IsLittleEndian);
            //Measurement flare (4 bytes)
            Flare = HighEndianReader.GetU16Fixed16NumberToDouble(iccData, index + 24, IsLittleEndian);
            //Standard illuminant (4 bytes)
            Illuminant = (StandardIlluminant)HighEndianReader.GetUint32(iccData,  index+ 24, IsLittleEndian);
        }

        
        public StandardObserver Observer { get; private set; }
        public XYZNumber XYZBacking { get; private set; }
        public MeasurementGeometry Geometry { get; private set; }
        public double Flare { get; private set; }
        public StandardIlluminant Illuminant { get; private set; }


    }
}
