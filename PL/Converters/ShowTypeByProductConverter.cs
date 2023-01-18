using System.Globalization;
using System.Windows.Data;

namespace PL.Converters
{
    public class ShowTypeByProductConverter : IMultiValueConverter
    {
        /// <summary>
        /// convert from source property type to target property type
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is not null)
                return ((BO.Product)values[0]).Category is BO.Category.Clothing ? ((BO.Product)values[0]).Clothing! :
                        ((BO.Product)values[0]).Shoes!;
            return values[1] is BO.Category.Clothing ? BO.Clothing.Blazers : BO.Shoes.Sneakers;
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
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return targetTypes;
        }
    }
}
