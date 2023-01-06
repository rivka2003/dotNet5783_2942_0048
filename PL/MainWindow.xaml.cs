using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using PL.Order;
using System.ComponentModel;
using System.Windows;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly BlApi.IBl? bl = BlApi.Factory.Get();
        private bool _isMenuOpen = false;
        public MainWindow()
        {
            InitializeComponent();
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
        public bool IsMenuOpen = false;
        private void Products_Button_Click(object sender, RoutedEventArgs e)
        {
            new ProductForList(false).ShowDialog();
        }

        private void Hamburger_Checked(object sender, RoutedEventArgs e)
        {
            _isMenuOpen = !_isMenuOpen;
            Data.Visibility = _isMenuOpen ? Visibility.Visible : Visibility.Collapsed;
        }

        private void BTManager_Click(object sender, RoutedEventArgs e)
        {
            new Password().ShowDialog();

            BTOrders.Visibility = Visibility.Visible;
            BTProducts.Visibility = Visibility.Visible;
        }

        private void BTOrderTracking_Click(object sender, RoutedEventArgs e)
        {
            new OrderTrackingWindow().ShowDialog();
        }

        private void BTCatalog_Click(object sender, RoutedEventArgs e)
        {
            new ProductForList(false).ShowDialog();
        }

        private void BTOrders_Click(object sender, RoutedEventArgs e)
        {
            new OrderForList().ShowDialog();
        }

        private void BTProducts_Click(object sender, RoutedEventArgs e)
        {
            new ProductForList(true).ShowDialog();
        }

        private void BTHome_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow();
        }
    }
}
