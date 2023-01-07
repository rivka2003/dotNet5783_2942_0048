using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        //private void btnCHANGEAMOUNT_Click(object sender, RoutedEventArgs e)
        //{
        //    var selection = (OrderItem)((ListView)sender).ItemsSource;
        //    bl!.Cart.UpdateAmountProduct(cart,selection.ID, selection.Amount);

        //}

        private void cartView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            //nothing happens
        }

        //enables numbers only for the requested amount
        private void PreviewTextInputDigits(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        //לבדוק אותם

        //whenever amount changes-update the amount of the product and calculate the total price again and put inside its label
        private void tbAMOUNT_TextChanged(object sender, TextChangedEventArgs e)
        {
            var selection = (OrderItem)((ListView)sender).ItemsSource;
            int amount = selection.Amount;
            bl!.Cart.UpdateAmountProduct(cart, selection.ID, amount);
            lblTOTALPRICE.Content = cart.TotalPrice;
        }

        //if x was requested delete the specific item from the cart order items list
        private void btnX_Click(object sender, RoutedEventArgs e)
        {
            var selection = (OrderItem)((ListView)sender).SelectedValue;
            //איך אני מפעילה את הפעולה delete על הitem sourse שממנו זימנתי את הפעולה?

        }

    }
}
