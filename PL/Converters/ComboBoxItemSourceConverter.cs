using System.Globalization;
using System.Windows.Data;

namespace PL.Converters
{
    public class ComboBoxItemSourceConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length > 1)
            {
                switch ((values[0], values[1]))
                {
                    case (BO.Category.Clothing, BO.Gender.Boys):
                        IEnumerable<BO.Clothing?> Boys = Enum.GetValues(typeof(BO.Clothing)).Cast<BO.Clothing?>();

                        return Boys.Where(item => item is not BO.Clothing.Dresses && item is not BO.Clothing.Skirts);
                    case (BO.Category.Clothing, BO.Gender.Women):

                        return Enum.GetValues(typeof(BO.Clothing));
                    case (BO.Category.Clothing, BO.Gender.Men):
                        IEnumerable<BO.Clothing?> Men = Enum.GetValues(typeof(BO.Clothing)).Cast<BO.Clothing?>();

                        return Men.Where(item => item is not BO.Clothing.Dresses && item is not BO.Clothing.Skirts);
                    case (BO.Category.Clothing, BO.Gender.Girls):

                        return Enum.GetValues(typeof(BO.Clothing));
                    case (BO.Category.Shoes, BO.Gender.Girls):

                        return Enum.GetValues(typeof(BO.Shoes));
                    case (BO.Category.Shoes, BO.Gender.Women):

                        return Enum.GetValues(typeof(BO.Shoes));

                    case (BO.Category.Shoes, BO.Gender.Men):
                        IEnumerable<BO.Shoes?> Men2 = Enum.GetValues(typeof(BO.Shoes)).Cast<BO.Shoes?>();

                        return Men2.Where(item => item is not BO.Shoes.Heels);
                    case (BO.Category.Shoes, BO.Gender.Boys):
                        IEnumerable<BO.Shoes?> Boys2 = Enum.GetValues(typeof(BO.Shoes)).Cast<BO.Shoes?>();

                        return Boys2.Where(item => item is not BO.Shoes.Heels);

                    default:
                        return Enum.GetValues(typeof(BO.Clothing));
                }

            }
            return Enum.GetValues(typeof(BO.Clothing));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
