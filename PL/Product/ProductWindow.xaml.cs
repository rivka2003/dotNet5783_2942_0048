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
        private BlApi.IBl? bl = BlApi.Factory.Get();

        IEnumerable<BO.Clothing> itemsClothing = Enum.GetValues(typeof(BO.Clothing)).Cast<BO.Clothing>();

        IEnumerable<BO.Shoes> itemsShoes = Enum.GetValues(typeof(BO.Shoes)).Cast<BO.Shoes>();

        IEnumerable<BO.SizeClothing> SizeClothing = Enum.GetValues(typeof(BO.SizeClothing)).Cast<BO.SizeClothing>();

        IEnumerable<int> SizeShoes = new int[] { 36, 37, 38, 39, 40, 41, 42, 43, 44, 45 };
        public ProductWindow(int ID)/// constructor to open an update window/
        {
            InitializeComponent();

            ///resets to show the current values
            BO.Product product = bl.Product.ProductDetailsForManager(ID);
            tbID.Text = product.ID.ToString();
            tbNAME.Text = product.Name;
            tbPRICE.Text = product.Price.ToString();
            tbINSTOCK.Text = product.InStock.ToString();
            tbDESCRIPTION.Text = product.Description;

            cbGENDER.ItemsSource = Enum.GetValues(typeof(BO.Gender));
            cbGENDER.SelectedItem = product.Gender;
            cbCATEGORY.ItemsSource = Enum.GetValues(typeof(BO.Category));
            cbCATEGORY.SelectedItem = product.Category;
            cbCOLOR.ItemsSource = Enum.GetValues(typeof(BO.Color));
            cbCOLOR.SelectedItem = product.Color;

            if (product.Category is BO.Category.Clothing) ///if cb was chosen as clothing
            {
                AddSize(SizeClothing);
                cbSIZE.SelectedItem = product.SizeClothing;
                cbTYPE.SelectedItem = product.Clothing;
            }
            else ///if cb was chosen as shoes
            {
                AddSize(SizeShoes);
                cbSIZE.SelectedItem = (int)product.SizeShoes!;
                cbTYPE.SelectedItem = product.Shoes;
            }

            btnSAVE.Content = "UPDATE";
            tbID.IsEnabled = false; ///unable changing the id 
        }

        public ProductWindow(BlApi.IBl _bl) /// constructor to open the add window
        {

            InitializeComponent();
            bl = _bl;

            ///resets the combo boxes options
            cbGENDER.ItemsSource = Enum.GetValues(typeof(BO.Gender));
            cbCATEGORY.ItemsSource = Enum.GetValues(typeof(BO.Category));
            cbCOLOR.ItemsSource = Enum.GetValues(typeof(BO.Color));

            /// Default filling of the combo box with values
            AddItems(itemsClothing);
            AddSize(SizeClothing);

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
            /// Clearing the combo box before re-adding
            cbTYPE.Items.Clear();
            cbTYPE.ItemsSource = null;
            cbSIZE.Items.Clear();
            cbSIZE.ItemsSource = null;

            if (cbCATEGORY.SelectedItem is BO.Category.Clothing) ///in case clothing was chosen
            {
                AddSize(SizeClothing);

                ///resets the options inside the cb according to the chosen gender
                if (cbGENDER.SelectedItem is not BO.Gender.Women && cbGENDER.SelectedItem is not BO.Gender.Girls)
                {
                    foreach (var item in itemsClothing)
                    {
                        if (item is not BO.Clothing.Dresses && item is not BO.Clothing.Skirts)
                            cbTYPE.Items.Add(item);
                    }
                }
                else
                    AddItems(itemsClothing);
            }
            else ///in case shoes was chosen
            {
                AddSize(SizeShoes);

                ///resets the options inside the cb according to the chosen gender
                if (cbGENDER.SelectedItem is not BO.Gender.Women)
                {
                    foreach (var item in itemsShoes)
                    {
                        if (item is not BO.Shoes.Heels)
                            cbTYPE.Items.Add(item);
                    }
                }
                else
                    AddItems(itemsShoes);
            }
            cbTYPE.SelectedIndex = 0;
            cbSIZE.SelectedIndex = 0;
        }

        private void AddItems<T>(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                cbTYPE.Items.Add(item);
            }
        }

        private void AddSize<T>(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                cbSIZE.Items.Add(item);
            }
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

        private void btnSAVE_Click(object sender, RoutedEventArgs e)
        {
            ///general pre checkings over the text boxes
            if (tbID.Text == "" || tbPRICE.Text == " ")
            {
                if (tbID.Text == "" || tbPRICE.Text == " ")
                    MessageBox.Show("Error - ID box can't be empty!", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                lblCHECK1.Visibility = Visibility.Hidden;
                lblx1.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                lblCHECK1.Visibility = Visibility.Visible;
                lblx1.Visibility = Visibility.Hidden;
            }
            if (tbNAME.Text == "")
            {
                MessageBox.Show("Error - Name box can't be empty!", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                lblCHECK2.Visibility = Visibility.Hidden;
                lblx2.Visibility = Visibility.Visible;
                return;
            }
            if (tbPRICE.Text == "" || tbPRICE.Text == " ")
            {
                MessageBox.Show("Error - Price box can't be empty!", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                lblCHECK3.Visibility = Visibility.Hidden;
                lblx3.Visibility = Visibility.Visible;
                return;
            }
            if (tbINSTOCK.Text == "" || tbINSTOCK.Text == " ")
            {
                MessageBox.Show("Error - Amount in stock box can't be empty!", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                lblCHECK4.Visibility = Visibility.Hidden;
                lblx4.Visibility = Visibility.Visible;
                return;
            }
            if (tbDESCRIPTION.Text == "")
            {
                MessageBox.Show("Error - Description box can't be empty!", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                lblCHECK5.Visibility = Visibility.Hidden;
                lblx5.Visibility = Visibility.Visible;
                return;
            }

            ///if pre checks are valid. put data in the product and send that to previous layers check.
            BO.Product product = new BO.Product()
            {
                ID = int.Parse(tbID.Text),
                Name = tbNAME.Text,
                InStock = int.Parse(tbINSTOCK.Text),
                Category = (BO.Category)cbCATEGORY.SelectedItem,
                Color = (BO.Color)cbCOLOR.SelectedItem,
                Gender = (BO.Gender)cbGENDER.SelectedItem,
                Description = tbDESCRIPTION.Text
            };

            if (tbPRICE.Text.Contains("."))
                product.Price = double.Parse(tbPRICE.Text);
            else
                product.Price = int.Parse(tbPRICE.Text);

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
        private void tbID_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (lblCHECK1 is null || lblx1 is null)
                return;
            if (tbID.Text.Length < 6)
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
