﻿using BlApi;
using BlImplementation;
using System;
using System.Text.RegularExpressions;
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
    /// Interaction logic for adding.xaml..
    /// </summary>
    public partial class adding : Window
    {
        private IBl _bl = new Bl();

        private BO.Product product = new BO.Product();
        
        public adding(IBl bl)
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

        private void PreviewTextInputDigits(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnADD_Click(object sender, RoutedEventArgs e) // mשגיאת ריצה בהוספת מוצר יש שגיאה בזריקת שגיאה בexistingObject
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
                MessageBox.Show("Not valid amount in stock-EMPTY");
                return;
            }
            if (tbDESCRIPTION.Text == " ")
            {
                MessageBox.Show("Not valid description-EMPTY");
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

            if (tbID.Text.Length == 7)
                product.ID = int.Parse(tbID.Text);
            product.Name = tbNAME.Text;
            product.Price = int.Parse(tbPRICE.Text);
            product.InStock= int.Parse(tbINSTOCK.Text);
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
                _bl.Product.AddProduct(product);
                MessageBox.Show("Added succesfuly");
               Close();
            }
            catch (BO.NotValid)///PRINTING ERROR MESSAGE ACCORDING TO THE PROBLEMATIC INPUT OR THE EMPTY PROPERTY
            {
                if ( tbID.Text.Length != 7)
                    MessageBox.Show("Not valid ID- Should contain 6 digits!");
                if (product.Name ==" ")
                    MessageBox.Show("Not valid name- Can't be empty!");
                if (product.Price <= 0)
                    MessageBox.Show("Not valid price- Should be positive!");
                if (product.InStock < 0)
                    MessageBox.Show("Not valid data in atock!");
            }
            catch(BO.ExistingObjectBo)
            {
                MessageBox.Show("The product already exist!");
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
