using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.Product
{
    /// <summary>
    /// Interaction logic for ProductItemView.xaml
    /// </summary>
    public partial class ProductItemView : Window
    {
        private readonly BlApi.IBl? bl = BlApi.Factory.Get();
        private readonly static BO.Cart cart = new() { Items = new List<BO.OrderItem?>()};
        readonly int id;
        BO.ProductItem product = new ();
        public ProductItemView(int ID)
        {
            InitializeComponent();

            id = ID;
            product = bl.Product.ProductDetailsForCustomer(ID, cart);
        }

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            bl!.Cart.AddProductToCart(cart, id);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            bl!.Cart.UpdateAmountProduct(cart, id, int.Parse(((TextBox)sender).Text));
        }
    }
}
