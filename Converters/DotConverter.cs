using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Quality_Control_EF.Converters
{
    public class DotConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString().Replace(".", ",");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString().Replace(",", ".");

            //if (string.IsNullOrEmpty(value.ToString()))
            //{
            //    return "0";
            //}
            //else if (!double.TryParse(value.ToString(), out _))
            //{
            //    _ = MessageBox.Show("Wprowadzona wartość nie jest liczbą! Popraw wartość.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return "0";
            //}
            //else
            //{
            //    return value.ToString().Replace(",", ".");
            //}
        }
    }
}
