using PL.Carts;
using PL.Order;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace PL
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        internal static string PasswordText;

        public BO.Cart Cart
        {
            get { return (BO.Cart)GetValue(CartProperty); }
            set { SetValue(CartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Cart.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CartProperty =
            DependencyProperty.Register("Cart", typeof(BO.Cart), typeof(HomePage));


        public HomePage(BO.Cart cart)
        {
            InitializeComponent();
            Cart = cart;
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
        private void Products_Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.mainFrame.Navigate(new Catalog(Cart, false));
        }
    }
}
