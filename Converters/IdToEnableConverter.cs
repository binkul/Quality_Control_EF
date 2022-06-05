using System;
using System.Globalization;
using System.Windows.Data;

namespace Quality_Control_EF.Converters
{
    public class IdToEnableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return false;
            else
            {
                long number = (long)value;
                return number > 0 ? true : false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
