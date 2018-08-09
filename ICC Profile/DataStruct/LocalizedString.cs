using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICC_Profile.DataStruct
{
    public class LocalizedString
    {
        public CultureInfo Locale { get; private set; }
        public string Text { get; private set; }

        public LocalizedString(CultureInfo Locale, string Text)
        {
            this.Locale = Locale;
            this.Text = Text;
        }

        internal LocalizedString(string Language, string Country,byte[] iccData, int idx, int length)
        {
            Locale = new CultureInfo(Language + "-" + Country);
            Text = HighEndianReader.GetUnicodeString(iccData, idx, length);
        }
    }
}
