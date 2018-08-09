using ICC_Profile.ICC_Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ICC_Profile.EnumConst;

namespace ICC_Profile
{
    public abstract class ICCTagData
    {
        //private static bool isLittleEndian = BitConverter.IsLittleEndian;
        protected static uint dataSize;
        public abstract TypeSignature Signature { get; }
        public static bool IsLittleEndian = BitConverter.IsLittleEndian;
        public int LengthInByte;
        public static ICCTagData ReadTagData(byte[] iccData, ICCTag tag, ICCHeader header)
        {
            dataSize = tag.Size;
            TypeSignature tagType =
                (TypeSignature)HighEndianReader.GetUint32(iccData, (int)tag.Offset, IsLittleEndian);
            return ReadTagData(tagType, iccData, (int)tag.Offset + 8, header);
        }

        public virtual void GetTagData(byte[] iccData, int index, ICCHeader header)
        { }

        private static ICCTagData ReadTagData(TypeSignature type, byte[] iccData, int index,
            ICCHeader header)
        {
            ICCTagData t = null;  
            switch (type)
            {
                case TypeSignature.chromaticity:
                    t = new ChromaticityTag();
                    break;
                case TypeSignature.colorantOrder:
                    t =new ColorantOrderTag();
                    break;
                case TypeSignature.colorantTable:
                    t =new ColorantTableTag();
                    break;
                case TypeSignature.curve:
                    t =new CurveTag();
                    break;
                case TypeSignature.data:
                    t =new DataTag();
                    break;
                case TypeSignature.dateTime:
                    t =new DateTimeTag();
                    break;
                case TypeSignature.lut16:
                    t =new Lut16Tag();
                    break;
                case TypeSignature.lut8:
                    t =new Lut8Tag();
                    break;
                case TypeSignature.lutAToB:
                    t =new LutAToBTag();
                    break;
                case TypeSignature.lutBToA:
                    t =new LutBToATag();
                    break;
                case TypeSignature.measurement:
                    t =new MeasurementTag();
                    break;
                case TypeSignature.multiLocalizedUnicode:
                    t =new MultiLocalizedUnicodeTag() {IsPlaceHolder = false};
                    
                    break;
                case TypeSignature.multiProcessElements:
                    t =new MultiProcessElementsTag();
                    break;
                case TypeSignature.namedColor2:
                    t =new NamedColor2Tag();
                    break;
                case TypeSignature.parametricCurve:
                    t =new ParametricCurveTag();
                    break;
                case TypeSignature.profileSequenceDesc:
                    t =new ProfileSequenceDescTag();
                    break;
                case TypeSignature.profileSequenceIdentifier:
                    t =new ProfileSequenceIdentifierTag();
                    break;
                case TypeSignature.responseCurveSet16:
                    t =new ResponseCurveSet16Tag();
                    break;
                case TypeSignature.s15Fixed16Array:
                    t =new s15Fixed16ArrayTag();
                    break;
                case TypeSignature.signature:
                    t =new SignatureTag();
                    break;
                case TypeSignature.text:
                    t =new TextTag();
                    break;
                case TypeSignature.u16Fixed16Array:
                    t =new u16Fixed16ArrayTag();
                    break;
                case TypeSignature.uInt16Array:
                    t =new uInt16ArrayTag();
                    break;
                case TypeSignature.uInt32Array:
                    t =new uInt32ArrayTag();
                    break;
                case TypeSignature.uInt64Array:
                    t =new uInt64ArrayTag();
                    break;
                case TypeSignature.uInt8Array:
                    t =new uInt8ArrayTag();
                    break;
                case TypeSignature.viewingConditions:
                    t =new ViewingConditionTag();
                    break;
                case TypeSignature.XYZ:
                    t =new XYZTag();
                    break;
            }
            t.GetTagData(iccData, index, header);
            return t;
        }
    }
}
