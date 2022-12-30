using System.Globalization;
using System.Windows.Data;
using System.Windows;
using BO;

namespace PL
{
    public class ChoosByCategory : IValueConverter
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
            if (targetType.GetType() is SizeClothing || targetType.GetType() is SizeShoes)
                return (value is Category.Clothing) ? Enum.GetValues(typeof(BO.SizeClothing)).Cast<BO.SizeClothing>() :
                    new int[] { 36, 37, 38, 39, 40, 41, 42, 43, 44, 45 };
            else
                return (value is Category.Clothing) ? Enum.GetValues(typeof(BO.Clothing)).Cast<BO.Clothing>() :
                Enum.GetValues(typeof(BO.Shoes)).Cast<BO.Shoes>();
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
            throw new NotImplementedException();
        }
    }
}
