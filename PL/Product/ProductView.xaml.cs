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
    /// Interaction logic for ProductView.xaml
    /// </summary>
    public partial class ProductView : Window
    {
        private readonly BlApi.IBl? bl = BlApi.Factory.Get();
        private readonly static BO.Cart cart = new ();
        readonly int id;
        public ProductView(int ID)
        {
            InitializeComponent();

            id = ID;
            BO.Product product = bl.Product.ProductDetailsForManager(id);

            Name.Content = product.Name;
            Description.Content = product.Description;
            Price.Content = product.Price;
            Amount.ItemsSource = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
        }

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            bl!.Cart.AddProductToCart(cart, id);
        }
    }
}
