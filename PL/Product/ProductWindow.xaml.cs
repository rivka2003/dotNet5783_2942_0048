using BlApi;
using BlImplementation;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PL.Product
{
    /// <summary>
    /// Interaction logic for Update.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        private IBl _bl = new Bl();
        public ProductWindow(int ID)///showing the current values
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
            cbGENDER.ItemsSource = Enum.GetValues(typeof(BO.Gender));
            cbGENDER.SelectedItem = product.Gender;
            tbDESCRIPTION.Text = product.Description;

            if (product.Category is BO.Category.Clothing)
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
            btnSAVE.Content = "UPDATE";
            lblCHECK1.Visibility = Visibility.Hidden;
            lblCHECK2.Visibility = Visibility.Hidden;
            lblCHECK3.Visibility = Visibility.Hidden;
            lblCHECK4.Visibility = Visibility.Hidden;
            lblCHECK5.Visibility = Visibility.Hidden;
            lblCHECK6.Visibility = Visibility.Hidden;
            lblCHECK7.Visibility = Visibility.Hidden;
            lblCHECK8.Visibility = Visibility.Hidden;
            lblCHECK9.Visibility = Visibility.Hidden;
            lblCHECK10.Visibility = Visibility.Hidden;
            lblx1.Visibility = Visibility.Hidden;
            lblx2.Visibility = Visibility.Hidden;
            lblx3.Visibility = Visibility.Hidden;
            lblx4.Visibility = Visibility.Hidden;
            lblx5.Visibility = Visibility.Hidden;
            lblx6.Visibility = Visibility.Hidden;
            lblx7.Visibility = Visibility.Hidden;
            lblx8.Visibility = Visibility.Hidden;
            lblx9.Visibility = Visibility.Hidden;
            lblx10.Visibility = Visibility.Hidden;
        }

        public ProductWindow(IBl bl)
        {
            InitializeComponent();
            _bl = bl;
            cbGENDER.ItemsSource = Enum.GetValues(typeof(BO.Gender));
            cbCATEGORY.ItemsSource = Enum.GetValues(typeof(BO.Category));
            cbCOLOR.ItemsSource = Enum.GetValues(typeof(BO.Color));

            cbGENDER.SelectedIndex = 0;
            cbCATEGORY.SelectedIndex = 0;
            cbCOLOR.SelectedIndex = 0;
            cbSIZE.SelectedIndex = 0;
            cbTYPE.SelectedIndex = 0;
            btnSAVE.Content = "ADD";
            lblCHECK1.Visibility = Visibility.Hidden;
            lblCHECK2.Visibility = Visibility.Hidden;
            lblCHECK3.Visibility = Visibility.Hidden;
            lblCHECK4.Visibility = Visibility.Hidden;
            lblCHECK5.Visibility = Visibility.Hidden;
            lblCHECK6.Visibility = Visibility.Hidden;
            lblCHECK7.Visibility = Visibility.Hidden;
            lblCHECK8.Visibility = Visibility.Hidden;
            lblCHECK9.Visibility = Visibility.Hidden;
            lblCHECK10.Visibility = Visibility.Hidden;
            lblx1.Visibility = Visibility.Hidden;
            lblx2.Visibility = Visibility.Hidden;
            lblx3.Visibility = Visibility.Hidden;
            lblx4.Visibility = Visibility.Hidden;
            lblx5.Visibility = Visibility.Hidden;
            lblx6.Visibility = Visibility.Hidden;
            lblx7.Visibility = Visibility.Hidden;
            lblx8.Visibility = Visibility.Hidden;
            lblx9.Visibility = Visibility.Hidden;
            lblx10.Visibility = Visibility.Hidden;
        }

        private void cbCATEGORY_SelectionChanged(object sender, SelectionChangedEventArgs e)///reset the enums according to the category
        {
            if (cbCATEGORY.SelectedItem == null)
            {
                lblCHECK7.Visibility = Visibility.Hidden;
                lblx7.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                lblx7.Visibility = Visibility.Hidden;
                lblCHECK7.Visibility = Visibility.Visible;
            }

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

        private void PreviewTextInputDigits(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new("[0-9]/.*[0-9]^");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnSAVE_Click(object sender, RoutedEventArgs e)
        {
            BO.Product product = new BO.Product();
            product.ID = int.Parse(tbID.Text);
            product.Name = tbNAME.Text;
            product.Price = int.Parse(tbPRICE.Text);
            product.InStock = int.Parse(tbINSTOCK.Text);
            product.Category = (BO.Category)cbCATEGORY.SelectedItem;
            product.Color = (BO.Color)cbCOLOR.SelectedItem;
            product.Gender = (BO.Gender)cbGENDER.SelectedItem;
            product.Description = tbDESCRIPTION.Text;
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
                if (btnSAVE.Content is "UPDATE")
                {
                    _bl.Product.UpdateProduct(product);
                    MessageBox.Show("Updated succesfuly!");
                }
                else
                {
                    _bl.Product.AddProduct(product);
                    MessageBox.Show("Added succesfuly");
                }
                Close();
            }
            catch (BO.NotValid)
            {
                if (tbID.Text.Length != 7)
                {
                    MessageBox.Show("Not valid ID- Should contain 6 digits");
                    lblCHECK1.Visibility = Visibility.Hidden;
                    lblx1.Visibility = Visibility.Visible;
                    return;
                }
                if (product.Name == " ")
                {
                    lblCHECK2.Visibility = Visibility.Hidden;
                    lblx2.Visibility = Visibility.Visible;
                    return;
                }
                if (product.Price <= 0)
                {
                    MessageBox.Show("Not valid price- Should be positive");
                    lblCHECK3.Visibility = Visibility.Hidden;
                    lblx3.Visibility = Visibility.Visible;
                    return;
                }
                if (product.InStock < 0)
                {
                    MessageBox.Show("Not valid data in atock");
                    lblCHECK4.Visibility = Visibility.Hidden;
                    lblx4.Visibility = Visibility.Visible;
                    return;
                }
            }
            catch (BO.NonFoundObjectBo ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BO.ExistingObjectBo ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tbID_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (btnSAVE.Content is "UPDATE")
            {
                tbID.IsReadOnly = true;
                return;
            }
            if (tbID.Text == " " || tbID.Text.Length > 8)
            {
                lblCHECK1.Visibility = Visibility.Hidden;
                lblx1.Visibility = Visibility.Visible;
                return;
            }
            lblx1.Visibility = Visibility.Hidden;
            lblCHECK1.Visibility = Visibility.Visible;
        }

        private void cbTYPE_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbTYPE.SelectedItem == null)
            {
                lblCHECK8.Visibility = Visibility.Hidden;
                lblx8.Visibility = Visibility.Visible;
                return;
            }
            lblx8.Visibility = Visibility.Hidden;
            lblCHECK8.Visibility = Visibility.Visible;
        }

        private void tbNAME_TextChanged(object sender, TextChangedEventArgs e)
        {
            lblx2.Visibility = Visibility.Hidden;
            lblCHECK2.Visibility = Visibility.Visible;
        }

        private void tbPRICE_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbPRICE.Text == " ")
            {
                lblCHECK3.Visibility = Visibility.Hidden;
                lblx3.Visibility = Visibility.Visible;
                return;
            }
            lblx3.Visibility = Visibility.Hidden;
            lblCHECK3.Visibility = Visibility.Visible;
        }

        private void tbINSTOCK_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbINSTOCK.Text == " " || tbINSTOCK.Text.Length > 8)
            {
                lblCHECK4.Visibility = Visibility.Hidden;
                lblx4.Visibility = Visibility.Visible;
                return;
            }
            lblx4.Visibility = Visibility.Hidden;
            lblCHECK4.Visibility = Visibility.Visible;
        }

        private void tbDESCRIPTION_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbDESCRIPTION.Text == " ")
            {
                lblCHECK5.Visibility = Visibility.Hidden;
                lblx5.Visibility = Visibility.Visible;
                return;
            }
            lblx5.Visibility = Visibility.Hidden;
            lblCHECK5.Visibility = Visibility.Visible;
        }

        private void cbGENDER_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbGENDER.SelectedItem == null)
            {
                lblCHECK6.Visibility = Visibility.Hidden;
                lblx6.Visibility = Visibility.Visible;
                return;
            }
            lblx6.Visibility = Visibility.Hidden;
            lblCHECK6.Visibility = Visibility.Visible;
        }

        private void cbCOLOR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbCOLOR.SelectedItem == null)
            {
                lblCHECK9.Visibility = Visibility.Hidden;
                lblx9.Visibility = Visibility.Visible;
                return;
            }
            lblx9.Visibility = Visibility.Hidden;
            lblCHECK9.Visibility = Visibility.Visible;
        }

        private void cbSIZE_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbSIZE.SelectedItem == null)
            {
                lblCHECK10.Visibility = Visibility.Hidden;
                lblx10.Visibility = Visibility.Visible;
                return;
            }
            lblx10.Visibility = Visibility.Hidden;
            lblCHECK10.Visibility = Visibility.Visible;
        }
    }
}
