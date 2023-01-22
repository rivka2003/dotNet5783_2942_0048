using PL.Carts;
using PL.Order;
using PL.Product;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly BO.Cart cart = new() { Items = new List<BO.OrderItem?>() };
        public static Frame mainFrame;
        internal static string passwordText;

        public bool IsMenuOpen
        {
            get { return (bool)GetValue(isMenuOpenProperty); }
            set { SetValue(isMenuOpenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsMenuOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty isMenuOpenProperty =
            DependencyProperty.Register("IsMenuOpen", typeof(bool), typeof(MainWindow));


        public string PasswordText
        {
            get { return (string)GetValue(PasswordTextProperty); }
            set { SetValue(PasswordTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PasswordText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PasswordTextProperty =
            DependencyProperty.Register("PasswordText", typeof(string), typeof(MainWindow));


        public MainWindow()
        {
            IsMenuOpen = false;
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
            IsMenuOpen = !IsMenuOpen;
        }

        //in order to have options of a manager - a password is bein requierd
        private void BTManager_Click(object sender, RoutedEventArgs e)
        {
            new Password().ShowDialog();
            PasswordText = passwordText;
        }

       //opens the window according to the hamburger's selection:

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

        private void BTGeneralDes_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new StoreDescription());
        }

        private void Cart_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new TheCartWindow(cart));
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            new TimerWindow().ShowDialog();
        }
    }
}
