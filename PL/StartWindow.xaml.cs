using BO;
using DocumentFormat.OpenXml.ExtendedProperties;
using PL.Order;
using System.Windows;
using System.Windows.Controls;

namespace PL
{
    /// <summary>
    /// Interaction logic for StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        private BlApi.IBl? bl = BlApi.Factory.Get();

        public StartWindow(BlApi.IBl bl)
        {

            InitializeComponent();
        }

        private void BTOrders_Click(object sender, RoutedEventArgs e)
        {
            Close();
            new OrderForList(bl!).ShowDialog();
        }

        private void BTProducts_Click(object sender, RoutedEventArgs e)
        {
            Close();
            new ProductForList(bl!).ShowDialog();
        }

        private void BTNewOrder_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BTOrderTracking_Click(object sender, RoutedEventArgs e)
        {
            Close();
            new OrderTrackingWindow().ShowDialog();

        }


    }
}
