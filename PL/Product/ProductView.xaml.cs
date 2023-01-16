using System.Windows;
using System.Windows.Controls;

namespace PL.Product
{
    /// <summary>
    /// Interaction logic for ProductView.xaml
    /// </summary>
    public partial class ProductView : Page
    {
        private readonly BlApi.IBl? bl = BlApi.Factory.Get();
        readonly int id;

        public BO.Cart Cart
        {
            get { return (BO.Cart)GetValue(cartProperty); }
            set { SetValue(cartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for cart.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty cartProperty =
            DependencyProperty.Register("cart", typeof(BO.Cart), typeof(ProductView));

        public int ProductAmount
        {
            get { return (int)GetValue(ProductAmountProperty); }
            set { SetValue(ProductAmountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProductAmount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProductAmountProperty =
            DependencyProperty.Register("ProductAmount", typeof(int), typeof(ProductView));

        public BO.ProductItem product
        {
            get { return (BO.ProductItem)GetValue(productProperty); }
            set { SetValue(productProperty, value); }
        }

        // Using a DependencyProperty as the backing store for product.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty productProperty =
            DependencyProperty.Register("product", typeof(BO.ProductItem), typeof(ProductView));

        
        public ProductView(BO.Cart cart, int ID)
        {
            InitializeComponent();
            //resets values of product to show
            Cart = cart;
            int index = Cart.Items!.FindIndex(item => item!.ProductID == ID);
            if (index == -1)
                ProductAmount = 0;
            else
                ProductAmount += Cart.Items[index]!.Amount;
            id = ID;
            product = bl.Product.ProductDetailsForCustomer(ID, Cart);
        }

        //adding the current product to the cart
        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl!.Cart.AddProductToCart(Cart, id, ProductAmount);
            }
            ///recieving error information from previous layer and showing the user with a message accordingly in case there is something wrong.
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //raise amount
        private void IncreaseBtn_Click(object sender, RoutedEventArgs e)
        {
            ProductAmount += 1;
        }

        //decrease amount
        private void DecreaseBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ProductAmount == 0)
                return;
            ProductAmount -= 1;
        }
    }
}
