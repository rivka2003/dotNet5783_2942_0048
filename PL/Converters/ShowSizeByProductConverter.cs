using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace PL.Converters
{
    public class ShowSizeByProductConverter : IValueConverter
    {
        /// <summary>
        /// convert from source property type to target property type
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not null)
                return ((BO.Product)value).Category is BO.Category.Clothing ? ((BO.Product)value).SizeClothing! :
                    System.Convert.ToInt32(((BO.Product)value).SizeShoes!);
            return ((BO.Product)parameter).Category is BO.Category.Clothing ? BO.SizeClothing.XS : 36;
        }

        /// <summary>
        /// convert from target property type to source property type
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
