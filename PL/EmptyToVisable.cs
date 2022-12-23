using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace PL
{
    public class EmptyToVisable : IValueConverter
    {
        //convert from source property type to target property type
        int val = 0;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            val++;
            return val < 6 ? Visibility.Hidden :
                   value is string strValue && string.IsNullOrWhiteSpace(strValue)
                   ? Visibility.Visible : Visibility.Hidden;
        }

        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
