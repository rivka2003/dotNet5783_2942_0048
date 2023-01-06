using System.Globalization;
using System.Windows.Data;
using System.Windows;
using System.Windows.Controls;

namespace PL
{
    public class EmptyToVisable : IValueConverter
    {
        //convert from source property type to target property type
        int val = 0;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int count = System.Convert.ToInt32(parameter);
            val++;
            //if (value is "tbID")
            //{
            //    return val < count ? Visibility.Hidden : 
            //        value is string strValueId && string.IsNullOrWhiteSpace(strValueId) || ((TextBox)value).Text.Length < 6 ? 
            //        Visibility.Visible : Visibility.Hidden;
            //}
            return val < count ? Visibility.Hidden :
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
