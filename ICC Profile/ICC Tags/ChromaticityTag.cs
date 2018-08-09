using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICC_Profile;

namespace ICC_Profile.ICC_Tags
{
    public class ChromaticityTag : ICCTagData
    {
        //private bool isLittleEndian = BitConverter.IsLittleEndian;
        public override EnumConst.TypeSignature Signature => EnumConst.TypeSignature.chromaticity; 

        public ushort ChannelCount { get; private set; }
        public EnumConst.ColorantEncoding ColorantType { get; private set; }
        public double[][] ChannelValues { get; private set; }


        private static double[][] GetChannelValueByEncoding(EnumConst.ColorantEncoding type)
        {
            //Table 31
            double[][] mtrx = {};
            switch (type)
            {
                case EnumConst.ColorantEncoding.EBU_Tech_3213_E:
                    mtrx = new[]
                    {
                        new[] {0.640, 0.330},
                        new[] {0.290, 0.600},
                        new[] {0.150, 0.060},
                    };
                    break;
                case EnumConst.ColorantEncoding.ITU_R_BT_709_2:
                    mtrx = new[]
                    {
                        new[] {0.640, 0.330},
                        new[] {0.300, 0.600},
                        new[] {0.150, 0.060},
                    };
                    break;
                case EnumConst.ColorantEncoding.P22:
                    mtrx = new[]
                    {
                        new[] {0.625, 0.340},
                        new[] {0.280, 0.605},
                        new[] {0.155, 0.070},
                    };
                    break;
                case EnumConst.ColorantEncoding.SMPTE_RP145:
                    mtrx = new[]
                    {
                        new[] {0.630, 0.340},
                        new[] {0.310, 0.595},
                        new[] {0.155, 0.070},
                    };
                    break;
            }
            return mtrx;
        }

        public override void GetTagData(byte[] iccData, int index, ICCHeader header=null)
        {
            ChannelCount = HighEndianReader.GetUInt16(iccData, index, IsLittleEndian);
            ColorantType = (EnumConst.ColorantEncoding)HighEndianReader.GetUInt16(iccData, index + 2, IsLittleEndian);
            if (ColorantType == EnumConst.ColorantEncoding.Unknown)
            {
                ChannelValues = new double[ChannelCount][];
                for (int i = 0; i < ChannelCount; i++)
                {
                    ChannelValues[i] = new double[2];
                    int startIndex = (index + 4) + i * 8;
                    ChannelValues[i][0] = HighEndianReader.GetU16Fixed16NumberToDouble(iccData, startIndex, IsLittleEndian);
                    ChannelValues[i][1] = HighEndianReader.GetU16Fixed16NumberToDouble(iccData, startIndex + 4, IsLittleEndian);
                }
            }
            else
            {
                ChannelValues = GetChannelValueByEncoding(ColorantType);

            }
        }
    }
}
