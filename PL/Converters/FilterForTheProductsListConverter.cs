using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;

namespace PL.Converters
{
    public class FilterForTheProductsListConverter : IMultiValueConverter
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
            if(values.Length > 1)
            {
                if ((bool)values[6])
                {
                    if (((BO.Category)values[1]) is BO.Category.Clothing)
                        return ((ObservableCollection<BO.ProductForList>)values[0]).Where(item => item.Category ==
                        (BO.Category)values[1] && item.Gender == (BO.Gender)values[2] && item.Clothing ==
                        (BO.Clothing)values[3] && item.SizeClothing == (BO.SizeClothing)values[4] &&
                        item.Color == (BO.Color)values[5]);
                    else
                        return ((ObservableCollection<BO.ProductForList>)values[0]).Where(item => item.Category ==
                        (BO.Category)values[1] && item.Gender == (BO.Gender)values[2] && item.Shoes ==
                        (BO.Shoes)values[3] && item.SizeShoes == (BO.SizeShoes)values[4] &&
                        item.Color == (BO.Color)values[5]);
                }
                else
                    return new ObservableCollection<BO.ProductForList>((BO.ProductForList)values[0]);
            }
            return 0;
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
            throw new NotImplementedException();
        }
    }
}
