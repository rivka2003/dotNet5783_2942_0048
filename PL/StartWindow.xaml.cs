using BO;
using System.Windows;

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

        private void BTManager_Click(object sender, RoutedEventArgs e)
        {
            new ProductForList(bl!).ShowDialog();
        }

        private void BTOrderTracking_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BTNewOrder_Click(object sender, RoutedEventArgs e)
        {
            new OrderForList(bl!).ShowDialog();
        }
    }
}
