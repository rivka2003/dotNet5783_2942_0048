using BlApi;
using BlImplementation;
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
            cbCATEGORY.Text = product.Category.ToString();
            cbCOLOR.Text = product.Color.ToString();
            if(product.Category is BO.Category.Clothing)
            {
                cbSIZE.Text = product.SizeClothing.ToString();
                cbTYPE.Text = product.Clothing.ToString();
            }
            else
            {
                cbSIZE.Text = product.SizeShoes.ToString();
                cbTYPE.Text = product.Shoes.ToString();
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
            if (tbID.Text == " ")
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
            if (tbINSTOCK.Text == " ")
            {
                MessageBox.Show("Not valid in stock-EMPTY");
                return;
            }
            ///איך נותנים לו הזדמנות להקיש לפני שממשיכים הלאה
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

            
        }

        private void tbID_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void cbTYPE_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

     
    }
}
