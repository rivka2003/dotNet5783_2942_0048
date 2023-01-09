﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using System.Windows.Controls;

namespace PL.Converters
{
    public class InCorrectPasswordToVisibleConverter : IValueConverter
    {
        int val = 0;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            val++;
            return val == 1 ? Visibility.Hidden :  value is "Fation" ? Visibility.Hidden : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}