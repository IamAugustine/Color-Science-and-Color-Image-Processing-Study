using Accord.Math;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICC_Profile.DataStruct
{
    public sealed class DeviceAttribute
    {
        public EnumConst.DeviceAttributeName Attribute1
        {
            get
            {
                return att1
                    ? EnumConst.DeviceAttributeName.Transparency
                    : EnumConst.DeviceAttributeName.Reflective;
            }
        }

        public EnumConst.DeviceAttributeName Attribute2
        {
            get
            {
                return att2 ? EnumConst.DeviceAttributeName.Matte : EnumConst.DeviceAttributeName.Glossy;
            }
        }

        public EnumConst.DeviceAttributeName MediaPolarity
        {
            get
            {
                return att3 ? EnumConst.DeviceAttributeName.Negative : EnumConst.DeviceAttributeName.Positive;
            }
        }

        public EnumConst.DeviceAttributeName Media
        {
            get
            {
                return att4 ? EnumConst.DeviceAttributeName.BlackWhite : EnumConst.DeviceAttributeName.Color;
            }
        }

        public byte[] VendorData { get; private set; }

        private bool att1;
        private bool att2;
        private bool att3;
        private bool att4;

        internal DeviceAttribute(byte[] arr, int index)
        {
            BitArray a = new BitArray(arr.Get(index, index + 3));
            att1 = a[0];
            att2 = a[1];
            att3 = a[2];
            att4 = a[3];
            VendorData = arr.Get(index + 4, index + 7);
        }

    }
}
