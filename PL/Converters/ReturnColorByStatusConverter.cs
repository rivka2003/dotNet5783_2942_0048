using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace PL.Converters
{
    public class ReturnColorByStatusConverter : IValueConverter
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
            switch ((BO.OrderStatus)value)
            {
                case BO.OrderStatus.Confirmed:
                    if((string)parameter is "Confirmed")
                    {
                        return Brushes.Red;
                    }
                    break;
                case BO.OrderStatus.Shipped:
                    if ((string)parameter is "Shipped")
                    {
                        return Brushes.Red;
                    }
                    break;
                case BO.OrderStatus.Delivered:
                    if ((string)parameter is "Delivered")
                    {
                        return Brushes.Red;
                    }
                    break;
                default:
                    return Brushes.Black;
            }
            return Brushes.Black;
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
