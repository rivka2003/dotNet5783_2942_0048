using DocumentFormat.OpenXml.Spreadsheet;
using PL.Carts;
using PL.Order;
using PL.Product;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _isMenuOpen = false;
        private readonly BO.Cart cart = new() { Items = new List<BO.OrderItem?>() };
        public static Frame mainFrame;
        internal static string PasswordText;
        public MainWindow()
        {
            InitializeComponent();
            mainFrame = MainFrame;
            mainFrame.Navigate(new HomePage(cart));
        }
        /// <summary>
        /// getting to the manager menue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Hamburger_Checked(object sender, RoutedEventArgs e)
        {
            _isMenuOpen = !_isMenuOpen;
            Data.Visibility = _isMenuOpen ? Visibility.Visible : Visibility.Collapsed;
        }

        private void BTManager_Click(object sender, RoutedEventArgs e)
        {
            new Password().ShowDialog();

            if (PasswordText == "Fation")
            {
                BTOrders.Visibility = Visibility.Visible;
                BTProducts.Visibility = Visibility.Visible;
            }
        }

        private void BTOrderTracking_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new TheOrderTrackingWindow());
        }

        private void BTCatalog_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new CatalogCustomer(cart));
        }

        private void BTOrders_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new OrdersList());
        }

        private void BTProducts_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new Catalog(cart));
        }

        private void BTHome_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new HomePage(cart));
        }

        private void Cart_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new TheCartWindow(cart));
        }
    }
}
