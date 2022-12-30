using System.Globalization;
using System.Windows.Data;
using System.Windows;
using BO;

namespace PL
{
    public class ChoosByCategory : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType.GetType() is SizeClothing || targetType.GetType() is SizeShoes)
                return (value is Category.Clothing) ? Enum.GetValues(typeof(BO.SizeClothing)).Cast<BO.SizeClothing>() :
                    new int[] { 36, 37, 38, 39, 40, 41, 42, 43, 44, 45 };
            else
                return (value is Category.Clothing) ? Enum.GetValues(typeof(BO.Clothing)).Cast<BO.Clothing>() :
                Enum.GetValues(typeof(BO.Shoes)).Cast<BO.Shoes>();
        }

        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
