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
            TypeCB.ItemsSource = Enum.GetValues(typeof(BO.Clothing));

            GenderCB.SelectedIndex = 0;
            CategoryCB.SelectedIndex = 0;
            ColorCB.SelectedIndex = 0;
            TypeCB.SelectedIndex = 0;
            SizeCB.SelectedIndex = 0;
        }

        private void GenderCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void CategoryCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CategoryCB.SelectedItem is BO.Category.Clothing)
            {
                SizeCB.ItemsSource = Enum.GetValues(typeof(BO.SizeClothing));
                TypeCB.ItemsSource = Enum.GetValues(typeof(BO.Clothing));
                if ((GenderCB.SelectedItem is not BO.Gender.Women) && (GenderCB.SelectedItem is not BO.Gender.Girls))
                {
                    TypeCB.Items.RemoveAt(10);
                    TypeCB.Items.RemoveAt(9);
                }
            }
            else
            {
                TypeCB.ItemsSource = Enum.GetValues(typeof(BO.Shoes));
                if (GenderCB.SelectedItem is not BO.Gender.Women)
                {
                    TypeCB.Items.RemoveAt(4);
                }
                SizeCB.ItemsSource = Enum.GetValues(typeof(BO.SizeShoes));
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
            new Update(ID).ShowDialog();
            productsLv.ItemsSource = bl.Product.GetAll();
        }

        private void Add_Product_Button_Click(object sender, RoutedEventArgs e)
        {
            new adding(bl).ShowDialog();
            productsLv.ItemsSource = bl.Product.GetAll();
        }
    }
}
