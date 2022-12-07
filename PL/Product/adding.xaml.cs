using BlApi;
using BlImplementation;
using BO;
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
    /// Interaction logic for adding.xaml.
    /// </summary>
    public partial class adding : Window
    {
        private IBl _bl = new Bl();

        private BO.Category category = new BO.Category();
        private BO.Product product = new BO.Product();
        
        public adding(IBl bl)
        {
            InitializeComponent();
            cbCATEGORY.ItemsSource = Enum.GetValues(typeof(BO.Category));
        }

        private void cbCATEGORY_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            category = (Category)cbCATEGORY.SelectedItem;
        }

        private void tbID_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (tbID.Text.Length >= 9)
            //   product.ID = tbID.;
        }

        private void cbINSTOCK_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnSAVE_Click(object sender, RoutedEventArgs e)
        {
            _bl.Product.AddProduct(product);
        }

     
    }
}
