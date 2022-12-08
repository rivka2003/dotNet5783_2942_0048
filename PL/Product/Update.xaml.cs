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
        public Update(int ID)
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

        private void btnSAVE_Click(object sender, RoutedEventArgs e)
        {
            BO.Product product = new BO.Product();
            if (tbID.Text.Length >= 9)
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

            _bl.Product.UpdateProduct(product);
        }

        private void tbID_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void cbTYPE_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
