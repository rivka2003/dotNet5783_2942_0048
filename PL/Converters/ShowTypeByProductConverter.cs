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
        /// 
        private BO.Product product { get; set; }
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            product = (values[0] as BO.Product)!;
            if (product is not null)
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
            BO.Shoes? Shoes = value as BO.Shoes?;
            BO.Clothing? Clothing = value as BO.Clothing?;

            if (Shoes is not null)
                product.Shoes =Shoes;
            else
                product.Clothing = Clothing;


            return new object[] { product };
        }
    }
}
