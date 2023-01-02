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

namespace PL.Carts
{
    /// <summary>
    /// Interaction logic for ItemView.xaml
    /// </summary>
    public partial class ItemView : Window
    {
        private readonly BlApi.IBl? bl = BlApi.Factory.Get();
        private readonly static BO.Cart cart = new();
        readonly int id;
        public ItemView(int ID)
        {
            InitializeComponent();

            id = ID;
            BO.OrderItem orderItem = bl.OrderItem.///bring the oi according to id;

            Name.Content = product.Name;
            Description.Content = product.Description;
            Price.Content = product.Price;
            Amount.ItemsSource = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
