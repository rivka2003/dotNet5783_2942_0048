using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace PL.Converters
{
    public class UpdateToChooseSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            BO.Product product = (BO.Product)value;
            if (product.ID > 0)
            {
                return product.Clothing is not null ? product.SizeClothing! : product.SizeShoes!;
            }
            return BO.SizeClothing.XS;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
