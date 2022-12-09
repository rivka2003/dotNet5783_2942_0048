using BlApi;
using BlImplementation;
using DO;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PL.Product
{
    /// <summary>
    /// Interaction logic for Update.xaml
    /// </summary>
    public partial class Update : Window
    {
        private IBl _bl = new Bl();
        public Update(int ID)///showing the current values
        {
            InitializeComponent();
            BO.Product product = _bl.Product.ProductDetailsForManager(ID);
            tbID.Text = product.ID.ToString();
            tbNAME.Text = product.Name;
            tbPRICE.Text = product.Price.ToString();
            tbINSTOCK.Text = product.InStock.ToString();
            cbCATEGORY.ItemsSource = Enum.GetValues(typeof(BO.Category));
            cbCATEGORY.SelectedItem = product.Category;
            cbCOLOR.ItemsSource = Enum.GetValues(typeof(BO.Color));
            cbCOLOR.SelectedItem = product.Color;

            if(product.Category is BO.Category.Clothing)
            {
                cbSIZE.ItemsSource = Enum.GetValues(typeof(BO.SizeClothing));
                cbSIZE.SelectedItem = product.SizeClothing;
                cbTYPE.ItemsSource = Enum.GetValues(typeof(BO.Clothing));
                cbTYPE.SelectedItem = product.Clothing;
            }
            else
            {
                cbSIZE.ItemsSource = Enum.GetValues(typeof(BO.SizeShoes));
                cbSIZE.SelectedItem = product.SizeShoes;
                cbTYPE.ItemsSource = Enum.GetValues(typeof(BO.Shoes));
                cbTYPE.SelectedItem = product.Shoes;
            }
        }

        private void cbCATEGORY_SelectionChanged(object sender, SelectionChangedEventArgs e)///reset the enums according to the category
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

        private void btnUPDATE_Click(object sender, RoutedEventArgs e)
        {
            if (tbID.Text == " " || tbID.Text.Length > 8)
            {
                MessageBox.Show("Not valid ID-EMPTY");
                return;
            }
            if (tbNAME.Text == " ")
            {
                MessageBox.Show("Not valid name-EMPTY");
                return;
            }
            if (tbPRICE.Text == " ")
            {
                MessageBox.Show("Not valid price-EMPTY");
                return;
            }
            if (tbINSTOCK.Text == " " || tbINSTOCK.Text.Length > 8)
            {
                MessageBox.Show("Not valid in stock-EMPTY");
                return;
            }
            BO.Product product = new BO.Product();
            if (tbID.Text.Length == 6)
                product.ID = int.Parse(tbID.Text);
            product.Name = tbNAME.Text;
            product.Price = int.Parse(tbPRICE.Text);
            ///CATEGORIESM/
            product.InStock = int.Parse(tbINSTOCK.Text);
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
            try
            {
                _bl.Product.UpdateProduct(product);
            }
            catch(Exception)
            {


                if (tbID.Text.Length != 6)
                    MessageBox.Show("Not valid ID- Should contain 6 digits");
                if (product.Name == " ")
                    MessageBox.Show("Not valid name- Can't be empty");
                if (product.Price <= 0)
                    MessageBox.Show("Not valid price- Should be positive");
                if (product.InStock < 0)
                    MessageBox.Show("Not valid data in atock");
            }
            Close();
        }

        private void tbID_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void cbTYPE_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

     
    }
}
