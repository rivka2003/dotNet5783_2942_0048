﻿using BO;
using System.Collections.ObjectModel;
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
        private readonly BlApi.IBl? bl = BlApi.Factory.Get();

        public static readonly DependencyProperty PoductDep = DependencyProperty.Register(nameof(product), typeof(BO.Product), typeof(ProductWindow));
        BO.Product product { get => (BO.Product)GetValue(PoductDep); set => SetValue(PoductDep, value); }

        //readonly IEnumerable<BO.Clothing> itemsClothing = Enum.GetValues(typeof(BO.Clothing)).Cast<BO.Clothing>();

        //readonly IEnumerable<BO.Shoes> itemsShoes = Enum.GetValues(typeof(BO.Shoes)).Cast<BO.Shoes>();

        readonly IEnumerable<BO.SizeClothing> SizeClothing = Enum.GetValues(typeof(BO.SizeClothing)).Cast<BO.SizeClothing>();

        readonly IEnumerable<int> SizeShoes = new int[] { 36, 37, 38, 39, 40, 41, 42, 43, 44, 45 };

        readonly IEnumerable<BO.Color> Color = Enum.GetValues(typeof(BO.Color)).Cast<BO.Color>();

        readonly IEnumerable<BO.Gender> Gender = Enum.GetValues(typeof(BO.Gender)).Cast<BO.Gender>();

        readonly IEnumerable<BO.Category> Category = Enum.GetValues(typeof(BO.Category)).Cast<BO.Category>();

        public ProductWindow(int ID)/// constructor to open an update window/
        {
            InitializeComponent();

            ///resets to show the current values
            product = bl.Product.ProductDetailsForManager(ID);

            //cbGENDER.ItemsSource = Enum.GetValues(typeof(BO.Gender));
            //cbCATEGORY.ItemsSource = Enum.GetValues(typeof(BO.Category));
            //cbCOLOR.ItemsSource = Enum.GetValues(typeof(BO.Color));

            //if (product.Category is BO.Category.Clothing) ///if cb was chosen as clothing
            //{
            //    AddItemsWithPredicate(cbSIZE.Items, SizeClothing);
            //    cbSIZE.SelectedItem = product.SizeClothing;
            //    cbTYPE.SelectedItem = product.Clothing;
            //}
            //else ///if cb was chosen as shoes
            //{
            //    AddItemsWithPredicate(cbSIZE.Items, SizeShoes);
            //    cbSIZE.SelectedItem = (int)product.SizeShoes!;
            //    cbTYPE.SelectedItem = product.Shoes;
            //}

            btnSAVE.Content = "UPDATE";
            tbID.IsEnabled = false; ///unable changing the id 
        }

        public ProductWindow() /// constructor to open the add window
        {

            InitializeComponent();

            ///resets the combo boxes options
            //cbGENDER.ItemsSource = Enum.GetValues(typeof(BO.Gender));
            //cbCATEGORY.ItemsSource = Enum.GetValues(typeof(BO.Category));
            //cbCOLOR.ItemsSource = Enum.GetValues(typeof(BO.Color));

            /// Default filling of the combo box with values
            //AddItemsWithPredicate(cbTYPE.Items, itemsClothing);
            //AddItemsWithPredicate(cbSIZE.Items, SizeClothing);

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
        private void CbCATEGORY_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ///// Clearing the combo box before re-adding
            //cbTYPE.Items.Clear();
            //cbTYPE.ItemsSource = null;
            //cbSIZE.Items.Clear();
            //cbSIZE.ItemsSource = null;

            //if (cbCATEGORY.SelectedItem is BO.Category.Clothing) ///in case clothing was chosen
            //{
            //    AddItemsWithPredicate(cbSIZE.Items, SizeClothing);

            //    ///resets the options inside the cb according to the chosen gender
            //    if (cbGENDER.SelectedItem is not BO.Gender.Women && cbGENDER.SelectedItem is not BO.Gender.Girls)
            //        AddItemsWithPredicate(cbTYPE.Items, itemsClothing, item => item is not BO.Clothing.Dresses && item is not BO.Clothing.Skirts);
            //    else
            //        AddItemsWithPredicate(cbTYPE.Items, itemsClothing);
            //}
            //else ///in case shoes was chosen
            //{
            //    AddItemsWithPredicate(cbSIZE.Items, SizeShoes);

            //    ///resets the options inside the cb according to the chosen gender 
            //    if (cbGENDER.SelectedItem is not BO.Gender.Women)
            //        AddItemsWithPredicate(cbTYPE.Items, itemsShoes, item => item is not BO.Shoes.Heels);
            //    else
            //        AddItemsWithPredicate(cbTYPE.Items, itemsShoes);
            //}
            //cbTYPE.SelectedIndex = 0;
            //cbSIZE.SelectedIndex = 0;
        }

        //private static void AddItemsWithPredicate<T>(ItemCollection itemCollection, IEnumerable<T> Collection, Predicate<T> predicate = null!)
        //{
        //    foreach (T item in Collection)
        //    {
        //        if (predicate is null)
        //            itemCollection.Add(item);
        //        else if (predicate(item))
        //            itemCollection.Add(item);
        //    }
        //}

        /// <summary>
        /// alowing only digits with a point for a double number
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviewTextInputDigits(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        /// <summary>
        /// alowing only digits in ID and amount in stock
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviewTextInputDigitsIDInStock(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        /// <summary>
        /// tou have to enter at list one letter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviewTextInputLetters(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new("^[A-Z,a-z]+ [0-9]*");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void BtnSAVE_Click(object sender, RoutedEventArgs e)
        {
            ///general pre checkings over the text boxes
            if (tbID.Text == "" || tbPRICE.Text == " ")
            {
                MessageBox.Show("Error - ID box can't be empty!", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (tbNAME.Text == "")
            {
                MessageBox.Show("Error - Name box can't be empty!", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (tbPRICE.Text == "" || tbPRICE.Text == " ")
            {
                MessageBox.Show("Error - Price box can't be empty!", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (tbINSTOCK.Text == "" || tbINSTOCK.Text == " ")
            {
                MessageBox.Show("Error - Amount in stock box can't be empty!", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (tbDESCRIPTION.Text == "")
            {
                MessageBox.Show("Error - Description box can't be empty!", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ///if pre checks are valid. put data in the product and send that to previous layers check.
            //BO.Product product = new ()
            //{
            //    ID = int.Parse(tbID.Text),
            //    Name = tbNAME.Text,
            //    InStock = int.Parse(tbINSTOCK.Text),
            //    Category = (BO.Category)cbCATEGORY.SelectedItem,
            //    Color = (BO.Color)cbCOLOR.SelectedItem,
            //    Gender = (BO.Gender)cbGENDER.SelectedItem,
            //    Description = tbDESCRIPTION.Text
            //};

            //if (tbPRICE.Text.Contains('.'))
            //    product.Price = double.Parse(tbPRICE.Text);
            //else
            //    product.Price = int.Parse(tbPRICE.Text);

            //if (cbCATEGORY.SelectedItem is BO.Category.Clothing)
            //{
            //    product.Clothing = (BO.Clothing)cbTYPE.SelectedItem;
            //    product.SizeClothing = (BO.SizeClothing)cbSIZE.SelectedItem;
            //}
            //else
            //{
            //    product.Shoes = (BO.Shoes)cbTYPE.SelectedItem;
            //    product.SizeShoes = (BO.SizeShoes)cbSIZE.SelectedItem;
            //}

            ///a try to update or add
            try
            {
                if (btnSAVE is null)
                    return;
                if (btnSAVE.Content is "UPDATE") ///for updating
                {
                    bl!.Product.UpdateProduct(product);
                    MessageBox.Show("Updated succesfuly!", "Saved product", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else ///for adding
                {
                    bl!.Product.AddProduct(product);
                    MessageBox.Show("Added succesfuly!", "Saved product", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                Close();
            }

            ///recieving error information from previous layer and showing the user with a message accordingly in case there is something wrong.
            catch (Exception ex)
            {
                if (tbID.Text.Length < 6)
                {
                    MessageBox.Show(ex.Message, "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                    if (lblCHECK1 is null)
                        return;

                    lblCHECK1.Visibility = Visibility.Hidden;
                    lblx1.Visibility = Visibility.Visible;
                    return;
                }
                MessageBox.Show(ex.Message, "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// checking and showing a small sign of v/x according to the user's typing. next to each text box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TbID_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (lblCHECK1 is null || lblx1 is null)
                return;
            if (((TextBox)sender).Text.Length < 6)
            {
                lblCHECK1.Visibility = Visibility.Hidden;
                lblx1.Visibility = Visibility.Visible;
                return;
            }
            lblx1.Visibility = Visibility.Hidden;
            lblCHECK1.Visibility = Visibility.Visible;
        }
    }
}
