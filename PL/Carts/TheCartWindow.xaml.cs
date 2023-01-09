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

        public int ProductAmount
        {
            get { return (int)GetValue(ProductAmountProperty); }
            set { SetValue(ProductAmountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProductAmount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProductAmountProperty =
            DependencyProperty.Register("ProductAmount", typeof(int), typeof(ProductView));

        public TheCartWindow(BO.Cart cart)
        {
            InitializeComponent();

            Cart = cart;
            ProductAmount = 0;
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
            //int ID = (BO.OrderItem)((Button)sender).DataContext;
            //int index = Cart.Items.FindIndex(item => item.ID == ID);
            //bl.Cart.UpdateAmountProduct(Cart,ID ,Cart.Items[index].Amount + 1);
        }

        private void DecreaseBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        //private void btnCHANGEAMOUNT_Click(object sender, RoutedEventArgs e)
        //{
        //    var selection = (OrderItem)((ListView)sender).ItemsSource;
        //    bl!.Cart.UpdateAmountProduct(Cart,selection.ID, selection.Amount);

        //}

        private void CartView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            //nothing happens
        }
    }
}
