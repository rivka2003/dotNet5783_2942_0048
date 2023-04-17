using BO;
using PL.Product;
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

        /// <summary>
        /// If he wanted to order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Cart.Items!.Count > 0)
                {
                    BO.Order order = bl!.Cart.OrderMaking(Cart);
                    MessageBox.Show($"Your order have made! this is your tracking number: {order.ID}", "Ordered", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("There are no products in the cart!", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                MainWindow.mainFrame.Navigate(new HomePage(Cart));
            }
            ///recieving error information from previous layer and showing the user with a message accordingly in case there is something wrong.
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        /// <summary>
        /// ti increase the amount of the product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            ///recieving error information from previous layer and showing the user with a message accordingly in case there is something wrong.
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// to decreas the amount of the product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            ///recieving error information from previous layer and showing the user with a message accordingly in case there is something wrong.
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// a button to remove a product from the cart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.OrderItem selection = (BO.OrderItem)((Button)sender).DataContext;
                int index = Cart.Items!.FindIndex(item => item!.ID == selection.ID);
                Cart temp = bl!.Cart.UpdateAmountProduct(Cart, selection.ProductID, 0);
                Cart = null!;
                Cart = temp;
            }
            ///recieving error information from previous layer and showing the user with a message accordingly in case there is something wrong.
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// showing the customer the product view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void List_DoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            BO.OrderItem selection = (BO.OrderItem)((ListView)sender).SelectedItem;
            MainWindow.mainFrame.Navigate(new ProductView(Cart, selection.ProductID));
        }
    }
}
