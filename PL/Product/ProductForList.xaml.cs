using PL.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PL
{
    /// <summary>
    /// Interaction logic for ProductForList.xaml
    /// </summary>
    public partial class ProductForList : Window
    {
        private BlApi.IBl? bl = BlApi.Factory.Get();

        private IEnumerable<BO.ProductForList> productForLists;

        IEnumerable<BO.Clothing> itemsClothing = Enum.GetValues(typeof(BO.Clothing)).Cast<BO.Clothing>();

        IEnumerable<BO.Shoes> itemsShoes = Enum.GetValues(typeof(BO.Shoes)).Cast<BO.Shoes>();

        IEnumerable<BO.SizeClothing> SizeClothing = Enum.GetValues(typeof(BO.SizeClothing)).Cast<BO.SizeClothing>();

        IEnumerable<int> SizeShoes = new int[] { 36, 37, 38, 39, 40, 41, 42, 43, 44, 45 };
        public ProductForList(BlApi.IBl bl)
        {
            InitializeComponent();

            this.bl = bl;
            productsLv.ItemsSource = bl.Product.GetAll();
            productForLists = bl.Product.GetAll()!;

            ///resets the combo boxes options
            GenderCB.ItemsSource = Enum.GetValues(typeof(BO.Gender));
            CategoryCB.ItemsSource = Enum.GetValues(typeof(BO.Category));
            ColorCB.ItemsSource = Enum.GetValues(typeof(BO.Color));

            /// Default filling of the combo box with values
            AddItems(itemsClothing);
            AddSize(SizeClothing);

            ///resets the combo boxes in default values
            GenderCB.SelectedIndex = 0;
            CategoryCB.SelectedIndex = 0;
            ColorCB.SelectedIndex = 0;
            TypeCB.SelectedIndex = 0;
            SizeCB.SelectedIndex = 0;
        }

        /// <summary>
        /// checking the chosen first two combo box and resets the rest of the cb accordingly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CategoryCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /// Clearing the combo box before re-adding
            TypeCB.Items.Clear();
            TypeCB.ItemsSource = null;
            SizeCB.Items.Clear();
            SizeCB.ItemsSource = null;

            if (CategoryCB.SelectedItem is BO.Category.Clothing) ///in case clothing was chosen
            {
                AddSize(SizeClothing);

                ///resets the options inside the cb according to the chosen gender
                if (GenderCB.SelectedItem is not BO.Gender.Women && GenderCB.SelectedItem is not BO.Gender.Girls)
                {
                    foreach (var item in itemsClothing)
                    {
                        if(item is not BO.Clothing.Dresses && item is not BO.Clothing.Skirts)
                            TypeCB.Items.Add(item);
                    }
                }
                else
                    AddItems(itemsClothing);
            }
            else ///in case shoes was chosen
            {
                AddSize(SizeShoes);

                ///resets the options inside the cb according to the chosen gender
                if (GenderCB.SelectedItem is not BO.Gender.Women)
                {
                    foreach (var item in itemsShoes)
                    {
                        if (item is not BO.Shoes.Heels)
                            TypeCB.Items.Add(item);
                    }
                }
                else
                    AddItems(itemsShoes);
            }
            TypeCB.SelectedIndex = 0;
            SizeCB.SelectedIndex = 0;
        }

        private void AddItems<T>(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                TypeCB.Items.Add(item);
            }
        }

        private void AddSize<T>(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                SizeCB.Items.Add(item);
            }
        }

        /// <summary>
        /// sorting the presented data according to the user's choices
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChooseB(object sender, RoutedEventArgs e)
        {
            if (CategoryCB.SelectedItem is BO.Category.Clothing)
            {
                productsLv.ItemsSource = productForLists.Where(item => item.Gender == (BO.Gender)GenderCB.SelectedItem &&
                item.Category == (BO.Category)CategoryCB.SelectedItem && item.Color == (BO.Color)ColorCB.SelectedItem &&
                item.Clothing == (BO.Clothing)TypeCB.SelectedItem && item.SizeClothing == (BO.SizeClothing)SizeCB.SelectedItem);
            }
            else
            {
                productsLv.ItemsSource = productForLists.Where(item => item.Gender == (BO.Gender)GenderCB.SelectedItem &&
                item.Category == (BO.Category)CategoryCB.SelectedItem && item.Color == (BO.Color)ColorCB.SelectedItem &&
                item.Shoes == (BO.Shoes)TypeCB.SelectedItem && item.SizeShoes == (BO.SizeShoes)SizeCB.SelectedItem);
            }
        }

        /// <summary>
        /// to update details of a specific product by double clicking the product in the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void doubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            int ID = ((BO.ProductForList)productsLv.SelectedItem).ID;
            new ProductWindow(ID).ShowDialog();
            productsLv.ItemsSource = bl!.Product.GetAll();
        }

        /// <summary>
        /// to add a new product to the product's list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Product_Button_Click(object sender, RoutedEventArgs e)
        {
            new ProductWindow(bl!).ShowDialog();
            productsLv.ItemsSource = bl!.Product.GetAll();
        }

        /// <summary>
        /// show the full list without a specific sort.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearB(object sender, RoutedEventArgs e)
        {
            productsLv.ItemsSource = productForLists.Select(item => item);
        }
    }
}
