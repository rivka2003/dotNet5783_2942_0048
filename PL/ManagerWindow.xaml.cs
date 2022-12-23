using System.Windows;

namespace PL
{
    /// <summary>
    /// Interaction logic for ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        private BlApi.IBl? bl = BlApi.Factory.Get();

        public ManagerWindow()
        {
            InitializeComponent();
        }

        private void BTProducts_Click(object sender, RoutedEventArgs e)
        {
            Close();
            new ProductForList(bl!).ShowDialog();
        }

        private void BTOrders_Click(object sender, RoutedEventArgs e)
        {
            Close();
            new OrderForList(bl!).ShowDialog();
        }
    }
}
