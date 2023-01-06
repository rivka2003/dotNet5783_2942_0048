using System.Windows;
using System.Windows.Controls;
using BO;

namespace PL.Carts
{
    /// <summary>
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        private readonly BlApi.IBl? bl = BlApi.Factory.Get();

        public static readonly DependencyProperty CartDep = DependencyProperty.Register(nameof(cart), typeof(Cart), typeof(CartWindow));
        Cart cart { get => (Cart)GetValue(CartDep); set => SetValue(CartDep, value); }
        public CartWindow(Cart c)
        {
            InitializeComponent();

            cart = c;
            //cart.Items = new List<OrderItem?>();
            //cartView.ItemsSource = cart.Items;

            //tbNAME.Text = cart.CustomerName;
            //tbEMAIL.Text = cart.CustomerEmail;
            //tbADDRESS.Text = cart.CustomerAddress;
            //tbPRICE.Text = cart.TotalPrice.ToString();


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try 
            {
                bl!.Cart.OrderMaking(cart);
                Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btnCHANGEAMOUNT_Click(object sender, RoutedEventArgs e)
        {
            var selection = (OrderItem)((ListView)sender).ItemsSource;
            bl!.Cart.UpdateAmountProduct(cart,selection.ID, selection.Amount);
            //also need to reupdate the total price.
        }

        private void cartView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        ///איך עושים את ההבאת אובייקט לעמוד ואתחול עם בבינדינג לפקדים. איך ואיפה מעדכנים כמות למוצר. איך מעצים את הליסט ויו כמו במקומות האחרים 
    }
}
