using BlApi;
using BlImplementation;
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
        private IBl bl = new Bl();

        private IEnumerable<BO.ProductForList> productForLists;
        public ProductForList(IBl bl)
        {
            InitializeComponent();

            this.bl = bl;
            productsLv.ItemsSource = bl.Product.GetAll();
            productForLists = bl.Product.GetAll()!;

            GenderCB.ItemsSource = Enum.GetValues(typeof(BO.Gender));
            CategoryCB.ItemsSource = Enum.GetValues(typeof(BO.Category));
            ColorCB.ItemsSource = Enum.GetValues(typeof(BO.Color));
            SizeCB.ItemsSource = Enum.GetValues(typeof(BO.SizeClothing));
            Array items = Enum.GetValues(typeof(BO.Clothing));
            foreach (BO.Clothing item in items)
            {
                TypeCB.Items.Add(item);
            }
            GenderCB.SelectedIndex = 0;
            CategoryCB.SelectedIndex = 0;
            ColorCB.SelectedIndex = 0;
            TypeCB.SelectedIndex = 0;
            SizeCB.SelectedIndex = 0;
        }

        private void CategoryCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TypeCB.Items.Clear();
            TypeCB.ItemsSource = null;
            if (CategoryCB.SelectedItem is BO.Category.Clothing)
            {
                SizeCB.ItemsSource = Enum.GetValues(typeof(BO.SizeClothing));
                Array items = Enum.GetValues(typeof(BO.Clothing));
                if (GenderCB.SelectedItem is not BO.Gender.Women && GenderCB.SelectedItem is not BO.Gender.Girls)
                {
                    foreach (BO.Clothing item in items)
                    {
                        if (item is not BO.Clothing.Dresses && item is not BO.Clothing.Skirts)
                        {
                            TypeCB.Items.Add(item);
                        }
                    }
                }
                else
                {
                    foreach (BO.Clothing item in items)
                    {
                        TypeCB.Items.Add(item);
                    }
                }
            }
            else
            {
                Array items = Enum.GetValues(typeof(BO.Shoes));
                if (GenderCB.SelectedItem is not BO.Gender.Women)
                {
                    foreach (BO.Shoes item in items)
                    {
                        if (item is not BO.Shoes.Heels)
                        {
                            TypeCB.Items.Add(item);
                        }
                    }
                }
                else
                {
                    foreach (BO.Shoes item in items)
                    {
                        TypeCB.Items.Add(item);
                    }
                }
                SizeCB.ItemsSource = new int[] { 36, 37, 38, 39, 40, 41, 42, 43, 44, 45 };
            }
        }

        private void TypeCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void ColorCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void TypeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void productsLv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

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

        private void doubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            int ID = ((BO.ProductForList)productsLv.SelectedItem).ID;
            new ProductWindow(ID).ShowDialog();
            productsLv.ItemsSource = bl.Product.GetAll();
        }

        private void Add_Product_Button_Click(object sender, RoutedEventArgs e)
        {
            new ProductWindow(bl).ShowDialog();
            productsLv.ItemsSource = bl.Product.GetAll();
        }

        private void ClearB(object sender, RoutedEventArgs e)
        {
            productsLv.ItemsSource = productForLists.Select(item => item);
        }

        private void SizeCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
