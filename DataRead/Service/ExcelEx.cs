using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Collections.ObjectModel;

namespace DataRead.Service
{
    internal static class ExcelEx
    {
        public static string Value(this ReadOnlyCollection<OpenXmlAttribute> Attributes, string Name)
        {
            string result = null;

            foreach (var attribute in Attributes)
                if (attribute.LocalName == Name)
                    result = attribute.Value;

            return result;
        }
    }
}
