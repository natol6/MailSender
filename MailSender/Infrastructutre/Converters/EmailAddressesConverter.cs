using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace MailSender.Infrastructutre.Converters
{
    class EmailAddressesConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string[] addresses = ((string)value).Split("; ");
            string addressConv = string.Empty;
            foreach (string adr in addresses)
            {
                if (adr != string.Empty)
                    if (addressConv == string.Empty)
                        addressConv += adr;
                    else addressConv += $"\n{adr}";
            }
            return addressConv;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
