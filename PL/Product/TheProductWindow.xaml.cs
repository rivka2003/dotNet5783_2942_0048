﻿using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PL.Product
{
    /// <summary>
    /// Interaction logic for TheProductWindow.xaml
    /// </summary>
    public partial class TheProductWindow : Page
    {
        private readonly BlApi.IBl? bl = BlApi.Factory.Get();

        public static readonly DependencyProperty PoductDep = DependencyProperty.Register(nameof(product), typeof(BO.Product), typeof(TheProductWindow));
        BO.Product product { get => (BO.Product)GetValue(PoductDep); set => SetValue(PoductDep, value); }

        public BO.Cart Cart
        {
            get { return (BO.Cart)GetValue(CartProperty); }
            set { SetValue(CartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Cart.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CartProperty =
            DependencyProperty.Register("Cart", typeof(BO.Cart), typeof(TheProductWindow));


        public IEnumerable<BO.Color> Color
        {
            get { return (IEnumerable<BO.Color>)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Color.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColorProperty =
        DependencyProperty.Register("Color", typeof(IEnumerable<BO.Color>), typeof(TheProductWindow));

        public IEnumerable<BO.Gender> Gender
        {
            get { return (IEnumerable<BO.Gender>)GetValue(GenderProperty); }
            set { SetValue(GenderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GenderProperty =
            DependencyProperty.Register("Gender", typeof(IEnumerable<BO.Gender>), typeof(TheProductWindow));

        public bool Window
        {
            get { return (bool)GetValue(WindowProperty); }
            set { SetValue(WindowProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Window.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WindowProperty =
            DependencyProperty.Register("Window", typeof(bool), typeof(TheProductWindow));

        public IEnumerable<BO.Category> Category
        {
            get { return (IEnumerable<BO.Category>)GetValue(CategoryProperty); }
            set { SetValue(CategoryProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Category.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CategoryProperty =
            DependencyProperty.Register("Category", typeof(IEnumerable<BO.Category>), typeof(TheProductWindow));
        public TheProductWindow(bool window, BO.Cart cart, int ID = 0)
        {
            if(!window)
                product = bl.Product.ProductDetailsForManager(ID);
            InitializeComponent();

            Cart = cart;
            Window = window;
            ///resets to show the current values
            Color = Enum.GetValues(typeof(BO.Color)).Cast<BO.Color>();
            Gender = Enum.GetValues(typeof(BO.Gender)).Cast<BO.Gender>();
            Category = Enum.GetValues(typeof(BO.Category)).Cast<BO.Category>();
        }

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

            ///if pre checks are valid.put data in the product and send that to previous layers check.
            //BO.Product product = new()
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

            /// a try to update or add
            try
            {
                if (!Window) ///for updating
                {
                    bl!.Product.UpdateProduct(product);

                    MessageBox.Show("Updated succesfuly!", "Saved product", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else ///for adding
                {
                    bl!.Product.AddProduct(product);

                    MessageBox.Show("Added succesfuly!", "Saved product", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                MainWindow.mainFrame.Navigate(new Catalog(Cart));
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
