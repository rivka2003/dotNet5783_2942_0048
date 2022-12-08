using BlApi;
using BlImplementation;
using BO;
using DO;
using DocumentFormat.OpenXml.Drawing.Diagrams;
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
            
        }

        private void cbINSTOCK_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnSAVE_Click(object sender, RoutedEventArgs e)
        {
            if (tbID.Text.Length >= 9)
                product.ID = int.Parse(tbID.Text);
            product.Name = tbNAME.Text;
            product.Price = int.Parse(tbPRICE.Text);
            ///CATEGORIESM/
            product.InStock= int.Parse(tbINSTOCK.Text);


            _bl.Product.AddProduct(product);
        }

        private void GenderCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            gender = (Gender)GenderCB.SelectedItem;
            // productsLv.ItemsSource = productForLists.Where(item => item.Gender == gender);
            if (gender == BO.Gender.Women)
            {
                //TypeCB.ItemsSource = Enum.GetValues(typeof(BO.Clothing));
            }
            CategoryCB.Visibility = Visibility.Visible;
            CategoryL.Visibility = Visibility.Visible;
        }

        private void CategoryCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            category = (Category)CategoryCB.SelectedItem;
            TypeL.Visibility = Visibility.Visible;
            ColorL.Visibility = Visibility.Visible;
            SizeL.Visibility = Visibility.Visible;
            TypeCB.Visibility = Visibility.Visible;
            ColorCB.Visibility = Visibility.Visible;
            SizeCB.Visibility = Visibility.Visible;
            chooseB.Visibility = Visibility.Visible;
            if (category == Category.Clothing)
            {
                TypeCB.ItemsSource = Enum.GetValues(typeof(BO.Clothing));
                SizeCB.ItemsSource = Enum.GetValues(typeof(BO.SizeClothing));
            }
            else
            {
                TypeCB.ItemsSource = Enum.GetValues(typeof(BO.Shoes));
                SizeCB.ItemsSource = Enum.GetValues(typeof(BO.SizeShoes));
            }
        }

        private void TypeCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (category == Category.Clothing)
                clothing = (Clothing)TypeCB.SelectedItem;
            else
                shoes = (Shoes)TypeCB.SelectedItem;
        }

        private void ColorCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            color = (Color)ColorCB.SelectedItem;
        }

        private void TypeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (category == Category.Clothing)
                sizeClothing = (SizeClothing)SizeCB.SelectedItem;
            else
                sizeShoes = (SizeShoes)SizeCB.SelectedItem;
        }

     
    }
}
