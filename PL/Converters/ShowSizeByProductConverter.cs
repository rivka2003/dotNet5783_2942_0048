using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace PL.Converters
{
    public class ShowSizeByProductConverter : IMultiValueConverter
    {
        private BO.Product product { get; set; }
        /// <summary>
        /// convert from source property type to target property type. resets with a default first value of enum the cb
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            product = (values[0] as BO.Product)!;
            if (product is not null)
                return ((BO.Product)values[0]).Category is BO.Category.Clothing ? ((BO.Product)values[0]).SizeClothing! :
                    System.Convert.ToInt32(((BO.Product)values[0]).SizeShoes!);
            return values[1] is BO.Category.Clothing?  BO.SizeClothing.XS : 36;
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
            int? sizeShoes = value as int?;
            BO.SizeClothing? sizeClothing = value as BO.SizeClothing?;

            if (sizeShoes is not null)
                product.SizeShoes = (BO.SizeShoes)sizeShoes;
            else
                product.SizeClothing = sizeClothing;


            return new object[] { product };
        }
    }
}
