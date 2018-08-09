using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICC_Profile.DataStruct;
using static ICC_Profile.EnumConst;
namespace ICC_Profile.DataStruct
{
    class ResponseCurve
    {
        public CurveMeasurementEncodings CurveType { get; private set; }
        public int[] MeasurmentCounts { get; private set; }
        public XYZNumber[] XYZvalues { get; private set; }
        public Response16Number[] ResponseArrays { get; private set; }

        internal ResponseCurve(byte[] iccData, int idx, int ChannelCount,bool isLittleEndian)
        {
            int dataEntryPoint = 0;
            //Measurement unit signature (4 bytes)
            CurveType = (CurveMeasurementEncodings)HighEndianReader.GetUint32(iccData, idx, isLittleEndian);
            //Counts of measurements in response arrays
            MeasurmentCounts = new int[ChannelCount];
            //int end = idx + 4 + 4 * ChannelCount; int c = 0;
            //for (int i = idx + 4; i < end; i += 4)
            //{ MeasurmentCounts[c] = (int)Helper.GetUInt32(i); c++; }

            for (int i = 0; i < ChannelCount; i++)
            {
                dataEntryPoint = idx + 4 + i * 4;
                MeasurmentCounts[i] = (int)HighEndianReader.GetUint32(iccData, dataEntryPoint, isLittleEndian);
            }
            //PCSXYZ values
            XYZvalues = new XYZNumber[ChannelCount];
            for (int i = 0; i < ChannelCount; i++)
            {
                dataEntryPoint = idx + 4 + ChannelCount * 4 + i * 4;
                XYZvalues[i] = new XYZNumber(iccData, dataEntryPoint, isLittleEndian);
            }
            //int start = end; end += 12 * ChannelCount; c = 0;
            //for (int i = start; i < end; i += 12) { XYZvalues[c] = new XYZNumber(i); c++; }
            //Response arrays
            int p = MeasurmentCounts.Sum();
            ResponseArrays = new Response16Number[p];
            for (int i = 0; i < p; i++)
            {
                dataEntryPoint += 8;
                ResponseArrays[i] = new Response16Number(iccData, dataEntryPoint, isLittleEndian);
            }
            //start = end; end += 8 * p; c = 0;
            //for (int i = start; i < end; i += 8) { ResponseArrays[c] = new Response16Number(i); c++; }
        }
    }
    public sealed class Response16Number
    {
        public ushort DeviceCode { get; private set; }
        public double MeasurmentValue { get; private set; }
        public Response16Number(byte[] iccData, int index, bool isLittleEndian)
        {

        }
    }
}
