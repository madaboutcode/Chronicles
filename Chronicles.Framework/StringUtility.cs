using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Chronicles.Framework
{
    public class StringUtility
    {
        private static readonly Regex titleRegex = new Regex("[^a-zA-Z0-9]+", RegexOptions.Compiled);
        public static String GetNormalizedText(string text, char seperator)
        {
            return titleRegex.Replace(text, seperator.ToString()).Trim(seperator);
        }

        public static String GetNormalizedText(string text)
        {
            return GetNormalizedText(text, '-');
        }
    }
}
