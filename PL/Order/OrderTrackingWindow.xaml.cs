using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for OrderTrackingWindow.xaml
    /// </summary>
    public partial class OrderTrackingWindow : Window
    {

        public OrderTrackingWindow()
        {
            
            InitializeComponent();
            btnORDERDETAILS.IsEnabled = false;
            btnORDERTRACKING.IsEnabled = false;

        }
        private void PreviewTextInputDigitsIDInStock(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void tbID_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbID.Text.Length >= 6 || tbID.Text.Length <= 8)
            {
                btnORDERDETAILS.Visibility = Visibility.Visible;
                btnORDERTRACKING.Visibility = Visibility.Visible;
            }
        }

        private void btnORDERTRACKING_Click(object sender, RoutedEventArgs e)
        {
            ///need to find the order tracking according to the given id and show it
        }

        private void btnORDERDETAILS_Click(object sender, RoutedEventArgs e)
        {
            /// need to find the order according to the given id and show it
        }
    }
}
