using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICC_Profile
{
    /// <summary>
    /// A C# wrap for ICC profile (Specification ICC.1: 2010, Profile Version: 4.3.0.0) 
    /// Developed in reference of Johannes Bildstein // https://www.codeproject.com/Articles/613798/Colorspaces-and-Conversions
    /// </summary>

    public class ICCProfile
    {

        
        public ICCHeader Header;

        public ICCTagTable TagTable;

        public List<ICCTagData> TagElementData;

        public void Read(string file)
        {
            byte[] iccProfileBytes = File.ReadAllBytes(file);
            ReadHeader(iccProfileBytes);
            ReadTagTable(iccProfileBytes);
            ReadTagData(iccProfileBytes);
        }

        private void ReadTagData(byte[] prflData)
        {
            TagElementData = new List<ICCTagData>((int)TagTable.TagCount);
            for (int i = 0; i < TagTable.TagCount; i++)
            {
                ICCTagData data = ICCTagData.ReadTagData(prflData, TagTable.Tags[i], Header);
                TagElementData.Add(data);
            }
        }

        private void ReadHeader(byte[] prflData)
        {
            Header = new ICCHeader();
            Header.Read(prflData);
        }

        private void ReadTagTable(byte[] prflData)
        {
            TagTable = new ICCTagTable();
            TagTable.Read(prflData);
        }
    }
}
