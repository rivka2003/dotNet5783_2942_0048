using BO;
using System.Windows;
using System.Windows.Controls;
namespace PL.Carts
{
    /// <summary>
    /// Interaction logic for TheCartWindow.xaml
    /// </summary>
    public partial class TheCartWindow : Page
    {
        private readonly BlApi.IBl? bl = BlApi.Factory.Get();

        public BO.Cart Cart
        {
            get { return (BO.Cart)GetValue(cartProperty); }
            set { SetValue(cartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Cart.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty cartProperty =
            DependencyProperty.Register("Cart", typeof(BO.Cart), typeof(TheCartWindow));

        public TheCartWindow(BO.Cart cart)
        {
            InitializeComponent();

            Cart = cart;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl!.Cart.OrderMaking(Cart);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void IncreaseBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.OrderItem selection = (BO.OrderItem)((Button)sender).DataContext;
                int index = Cart.Items!.FindIndex(item => item!.ProductID == selection.ProductID);
                int ProductAmount = Cart.Items[index]!.Amount;
                Cart temp = bl!.Cart.UpdateAmountProduct(Cart, selection.ProductID, ProductAmount + 1);
                Cart = null!;
                Cart = temp;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DecreaseBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.OrderItem selection = (BO.OrderItem)((Button)sender).DataContext;
                int index = Cart.Items!.FindIndex(item => item!.ProductID == selection.ProductID);
                int ProductAmount = Cart.Items[index]!.Amount;
                if (ProductAmount > 0)
                {
                    Cart temp = bl!.Cart.UpdateAmountProduct(Cart, selection.ProductID, ProductAmount - 1); 
                    Cart = null!;
                    Cart = temp;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RemoveBtn_Click(object sender, RoutedEventArgs e)
        {
            BO.OrderItem selection = (BO.OrderItem)((Button)sender).DataContext;
            int index = Cart.Items!.FindIndex(item => item!.ID == selection.ID);
            Cart temp = bl!.Cart.UpdateAmountProduct(Cart,selection.ProductID, 0);
            Cart = null!;
            Cart = temp;
        }
    }
}
