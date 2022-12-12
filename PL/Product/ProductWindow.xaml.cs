﻿using BlApi;
using BlImplementation;
using System;
using System.Security.Cryptography.Xml;
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
        public ProductWindow(int ID)/// constructor to open an update window
        {
            InitializeComponent();
            ///resets to show the current values
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

            ///resets the options of the second and third combo boxes according to what was chosen
            if (product.Category is BO.Category.Clothing)///if first cb was chosen as clothing
            {
                cbSIZE.ItemsSource = Enum.GetValues(typeof(BO.SizeClothing));
                cbSIZE.SelectedItem = product.SizeClothing;
                Array items = Enum.GetValues(typeof(BO.Clothing));
                foreach (BO.Clothing item in items)
                {
                    cbTYPE.Items.Add(item);
                }
                cbTYPE.SelectedItem = product.Clothing;
            }
            else///if first cb was chosen as shoes
            {
                cbSIZE.ItemsSource = Enum.GetValues(typeof(BO.SizeShoes));
                cbSIZE.SelectedItem = product.SizeShoes;
                Array items = Enum.GetValues(typeof(BO.Clothing));
                foreach (BO.Clothing item in items)
                {
                    cbTYPE.Items.Add(item);
                }
                cbTYPE.SelectedItem = product.Shoes;
            }
            btnSAVE.Content = "UPDATE";
            tbID.IsReadOnly = true;///unable changing the id 
        }

        public ProductWindow(IBl bl)/// constructor to open an update window
        {

            InitializeComponent();
            _bl = bl;
            cbGENDER.ItemsSource = Enum.GetValues(typeof(BO.Gender));
            cbCATEGORY.ItemsSource = Enum.GetValues(typeof(BO.Category));
            cbCOLOR.ItemsSource = Enum.GetValues(typeof(BO.Color));
            Array items = Enum.GetValues(typeof(BO.Clothing));
            foreach (BO.Clothing item in items)
            {
                cbTYPE.Items.Add(item);
            }
            ///resets to default values 
            cbGENDER.SelectedIndex = 0;
            cbCATEGORY.SelectedIndex = 0;
            cbCOLOR.SelectedIndex = 0;
            cbSIZE.SelectedIndex = 0;
            cbTYPE.SelectedIndex = 0;
            btnSAVE.Content = "ADD";
            lblTITLE.Content = "Add a new product";
        }

        ///reset the enums according to the category
        private void cbCATEGORY_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ///signs of v/x according to the values entered
            
            if (lblCHECK7 is null || lblx7 is null)
                return;

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

            if (lblCHECK6 is null || lblx6 is null)
                return;

            if (cbGENDER.SelectedItem == null)
            {
                lblCHECK6.Visibility = Visibility.Hidden;
                lblx6.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                lblx6.Visibility = Visibility.Hidden;
                lblCHECK6.Visibility = Visibility.Visible;
            }

            cbTYPE.Items.Clear();
            cbTYPE.ItemsSource = null;
            if (cbCATEGORY.SelectedItem is BO.Category.Clothing)///for clothing
            {
                cbSIZE.ItemsSource = Enum.GetValues(typeof(BO.SizeClothing));
                Array items = Enum.GetValues(typeof(BO.Clothing));
                if (cbGENDER.SelectedItem is not BO.Gender.Women && cbGENDER.SelectedItem is not BO.Gender.Girls)
                {
                    foreach (BO.Clothing item in items)
                    {
                        if (item is not BO.Clothing.Dresses && item is not BO.Clothing.Skirts)
                        {
                            cbTYPE.Items.Add(item);
                        }
                    }
                }
                else
                {
                    foreach (BO.Clothing item in items)
                    {
                        cbTYPE.Items.Add(item);
                    }
                }
            }
            else///for shoes
            {
                Array items = Enum.GetValues(typeof(BO.Shoes));
                if (cbGENDER.SelectedItem is not BO.Gender.Women)
                {
                    foreach (BO.Shoes item in items)
                    {
                        if (item is not BO.Shoes.Heels)
                        {
                            cbTYPE.Items.Add(item);
                        }
                    }
                }
                else
                {
                    foreach (BO.Shoes item in items)
                    {
                        cbTYPE.Items.Add(item);
                    }
                }
                cbSIZE.ItemsSource = new int[] { 36, 37, 38, 39, 40, 41, 42, 43, 44, 45 };
            }
        }

        ///allowing only digits:
        private void PreviewTextInputDigits(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new("[0-9]/.*[0-9]^");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void PreviewTextInputDigitsIDInStock(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void PreviewTextInputLetters(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new("^[A-Z,a-z]+ [0-9]*");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnSAVE_Click(object sender, RoutedEventArgs e)///a common button for both updating and adding.
        {
            ///general pre checkings over the text boxes
            if (tbID.Text == "" || tbID.Text.Length > 8)
            {
                MessageBox.Show("Not valid ID-empty/too many digits");
                return;
            }
            if (tbNAME.Text == "")
            {
                MessageBox.Show("Not valid name-EMPTY");
                return;
            }
            if (tbPRICE.Text == "")
            {
                MessageBox.Show("Not valid price-EMPTY");
                return;
            }
            if (tbINSTOCK.Text == "" || tbINSTOCK.Text.Length > 8)
            {
                MessageBox.Show("Not valid in stock-EMPTY");
                return;
            }
            if (tbDESCRIPTION.Text == "")
            {
                MessageBox.Show("Not valid description-EMPTY");
                return;
            }
            if (cbGENDER.SelectedItem == null)
            {
                MessageBox.Show("Not valid gender-EMPTY");
                return;
            }
            if (cbCATEGORY.SelectedItem == null)
            {
                MessageBox.Show("Not valid category-EMPTY");
                return;
            }
            if (cbCOLOR.SelectedItem == null)
            {
                MessageBox.Show("Not valid color-EMPTY");
                return;
            }
            if (cbTYPE.SelectedItem == null)
            {
                MessageBox.Show("Not valid type-EMPTY");
                return;
            }
            if (cbSIZE.SelectedItem == null)
            {
                MessageBox.Show("Not valid size-EMPTY");
                return;
            }


            ///if pre checks are valid. put data in the product and send that to previous layers check.
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


            ///a try to update or add
            try
            {
                if (btnSAVE is null)
                    return;
                if (btnSAVE.Content is "UPDATE")///for updating
                {
                    _bl.Product.UpdateProduct(product);
                    MessageBox.Show("Updated succesfuly!");
                }
                else///for adding
                {
                    _bl.Product.AddProduct(product);
                    MessageBox.Show("Added succesfuly");
                }
                Close();
            }

            ///recieving error information from previous layer and showing the user with a message accordingly in case there is something wrong.
            catch (BO.NotValid ex)
            {
                
                if (tbID.Text.Length != 7)
                {
                    MessageBox.Show(ex.Message);

                    if (lblCHECK1 is null)
                        return;
                    lblCHECK1.Visibility = Visibility.Hidden;
                    lblx1.Visibility = Visibility.Visible;
                    return;
                }
                if (product.Name == "")
                {
                    MessageBox.Show(ex.Message);
                    if (lblCHECK2 is null)
                        return;
                    lblCHECK2.Visibility = Visibility.Hidden;
                    lblx2.Visibility = Visibility.Visible;
                    return;
                }
                if (product.Description == "")
                {
                    MessageBox.Show(ex.Message);
                    if (lblCHECK5 is null)
                        return;
                    lblCHECK5.Visibility = Visibility.Hidden;
                    lblx5.Visibility = Visibility.Visible;
                    return;
                }
                if (product.Price <= 0)
                {
                    MessageBox.Show(ex.Message);
                    if (lblCHECK3 is null)
                        return;
                    lblCHECK3.Visibility = Visibility.Hidden;
                    lblx3.Visibility = Visibility.Visible;
                    return;
                }
                if (product.InStock < 0)
                {
                    MessageBox.Show(ex.Message);
                    if (lblCHECK4 is null)
                        return;
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



        /// <summary>
        /// checking and showing a small sign of v/x according to the user's typing. next to each text box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbID_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (lblCHECK1 is null || lblx1 is null)
                return;
            if (tbID.Text == "" || tbID.Text.Length !=6)
            {
                lblCHECK1.Visibility = Visibility.Hidden;
                lblx1.Visibility = Visibility.Visible;
                return;
            }
            lblx1.Visibility = Visibility.Hidden;
            lblCHECK1.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// checking and showing a small sign of v/x according to the user's typing. next to each text box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbTYPE_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lblCHECK8 is null || lblx8 is null)
                return;
            if (cbTYPE.SelectedItem == null)
            {
                lblCHECK8.Visibility = Visibility.Hidden;
                lblx8.Visibility = Visibility.Visible;
                return;
            }
            lblx8.Visibility = Visibility.Hidden;
            lblCHECK8.Visibility = Visibility.Visible;
        }


        /// <summary>
        /// checking and showing a small sign of v/x according to the user's typing. next to each text box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbNAME_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (lblCHECK2 is null || lblx2 is null)
                return;
            if(tbNAME.Text == "")
            {
                lblCHECK2.Visibility = Visibility.Hidden;
                lblx2.Visibility = Visibility.Visible;
                return;
            }
            lblx2.Visibility = Visibility.Hidden;
            lblCHECK2.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// checking and showing a small sign of v/x according to the user's typing. next to each text box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbPRICE_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (lblCHECK3 is null || lblx3 is null)
                return;
            if (tbPRICE.Text == "")
            {
                lblCHECK3.Visibility = Visibility.Hidden;
                lblx3.Visibility = Visibility.Visible;
                return;
            }
            lblx3.Visibility = Visibility.Hidden;
            lblCHECK3.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// checking and showing a small sign of v/x according to the user's typing. next to each text box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbINSTOCK_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (lblCHECK4 is null || lblx4 is null)
                return;
            if (tbINSTOCK.Text == "" || tbINSTOCK.Text.Length > 8)
            {
                lblCHECK4.Visibility = Visibility.Hidden;
                lblx4.Visibility = Visibility.Visible;
                return;
            }
            lblx4.Visibility = Visibility.Hidden;
            lblCHECK4.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// checking and showing a small sign of v/x according to the user's typing. next to each text box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbDESCRIPTION_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (lblCHECK5 is null || lblx5 is null)
                return;
            if (tbDESCRIPTION.Text == "")
            {
                lblCHECK5.Visibility = Visibility.Hidden;
                lblx5.Visibility = Visibility.Visible;
                return;
            }
            lblx5.Visibility = Visibility.Hidden;
            lblCHECK5.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// checking and showing a small sign of v/x according to the user's typing. next to each text box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbCOLOR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lblCHECK9 is null || lblx9 is null)
                return;
            if (cbCOLOR.SelectedItem == null)
            {
                lblCHECK9.Visibility = Visibility.Hidden;
                lblx9.Visibility = Visibility.Visible;
                return;
            }
            lblx9.Visibility = Visibility.Hidden;
            lblCHECK9.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// checking and showing a small sign of v/x according to the user's typing. next to each text box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbSIZE_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lblCHECK10 is null || lblx10 is null)
                return;
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
