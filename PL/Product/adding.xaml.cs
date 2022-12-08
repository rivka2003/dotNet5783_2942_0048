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

        private BO.Product product = new BO.Product();
        
        public adding(IBl bl)
        {
            InitializeComponent();
            _bl = bl;
            cbCATEGORY.ItemsSource = Enum.GetValues(typeof(BO.Category));
            cbCOLOR.ItemsSource = Enum.GetValues(typeof(BO.Color));
        }

        private void cbCATEGORY_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbCATEGORY.SelectedItem is BO.Category.Clothing)
            {
                cbSIZE.ItemsSource = Enum.GetValues(typeof(BO.SizeClothing));
                cbTYPE.ItemsSource = Enum.GetValues(typeof(BO.Clothing));
            }
            else
            {
                cbSIZE.ItemsSource = Enum.GetValues(typeof(BO.SizeShoes));
                cbTYPE.ItemsSource = Enum.GetValues(typeof(BO.Shoes));
            }
        }

        private void tbID_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void cbINSTOCK_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void cbTYPE_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnSAVE_Click(object sender, RoutedEventArgs e) // שגיאת ריצה בהוספת מוצר יש שגיאה בזריקת שגיאה בexistingObject
        {
            if (tbID.Text.Length >= 9)
                product.ID = int.Parse(tbID.Text);
            product.Name = tbNAME.Text;
            product.Price = int.Parse(tbPRICE.Text);
            ///CATEGORIESM/
            product.InStock= int.Parse(tbINSTOCK.Text);
            product.Category = (BO.Category)cbCATEGORY.SelectedItem;
            product.Color = (BO.Color)cbCOLOR.SelectedItem;
            if (cbCATEGORY.SelectedItem is BO.Category.Clothing)
            {
                product.Clothing = (BO.Clothing)cbTYPE.SelectedItem;
                product.SizeClothing = (BO.SizeClothing)cbSIZE.SelectedItem;
            }
            else
            {
                product.Shoes = (BO.Shoes)cbTYPE.SelectedItem;
                product.SizeShoes = (BO.SizeShoes)cbSIZE.SelectedItem;
            }

            _bl.Product.AddProduct(product);
        }
    }
}
