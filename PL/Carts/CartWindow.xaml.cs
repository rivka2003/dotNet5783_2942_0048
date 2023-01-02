using DocumentFormat.OpenXml.Office2010.Excel;
using PL.Order;
using System;
using System.Collections.Generic;
using System.IO.Packaging;
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
using BO;

namespace PL.Carts
{
    /// <summary>
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        private BlApi.IBl? bl = BlApi.Factory.Get();
        Cart cart;
        public CartWindow()
        {
            InitializeComponent();

            this.bl = bl;
            cart = new Cart() { Items = new List<OrderItem?>() };
            productView.ItemsSource = cart.Items;

            tbNAME = cart.CustomerName;
            tbEMAIL = cart.CustomerEmail;
            tbADDRESS = cart.CustomerAddress;
            tbPRICE = cart.TotalPrice;


        }

        private void doubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            int ID = ((BO.Items)ProductView.SelectedItem).ID;
            new ItemView(ID).ShowDialog();
            productView.ItemsSource = bl!.cart.Items.GetAll();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try 
            {
                cart.orderMaking();
                Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btnCHANGEAMOUNT_Click(object sender, RoutedEventArgs e)
        {
            cart.Items.updateAmount(Amount.selectedItem.text());
            ///also need to reupdate the total price.
        }

        ///איך עושים את ההבאת אובייקט לעמוד ואתחול עם בבינדינג לפקדים. איך ואיפה מעדכנים כמות למוצר. איך מעצים את הליסט ויו כמו במקומות האחרים 
    }
}
