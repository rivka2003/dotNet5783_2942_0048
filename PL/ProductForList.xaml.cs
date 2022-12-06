using BlApi;
using BlImplementation;
using BO;
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

        private BO.Gender gender = new BO.Gender();
        private BO.Category category = new BO.Category();
        private BO.Clothing clothing = new BO.Clothing();
        private BO.Shoes shoes = new BO.Shoes();
        private BO.Color color = new BO.Color();
        private BO.SizeClothing sizeClothing = new BO.SizeClothing();
        private BO.SizeShoes sizeShoes = new BO.SizeShoes();

        private IEnumerable<BO.ProductForList> productForLists;
        public ProductForList(IBl bl)
        {
            InitializeComponent();

            this.bl = bl;
            productsLv.ItemsSource = bl.Product.GetAll();
            productForLists = bl.Product.GetAll();
            GenderCB.ItemsSource = Enum.GetValues(typeof(BO.Gender));
            CategoryCB.ItemsSource = Enum.GetValues(typeof(BO.Category));
            ColorCB.SelectedItem = Enum.GetValues(typeof(BO.Color));
        }

        private void GenderCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //CategoryCB.Visibility = Visibility.Hidden;
            CategoryCB.Visibility = Visibility.Hidden;
            TypeCB.Visibility = Visibility.Hidden;
            ColorCB.Visibility = Visibility.Hidden;
            SizeCB.Visibility = Visibility.Hidden;
            CategoryL.Visibility = Visibility.Hidden;
            TypeL.Visibility = Visibility.Hidden;
            ColorL.Visibility = Visibility.Hidden;
            SizeL.Visibility = Visibility.Hidden;
            gender = (Gender)GenderCB.SelectedItem;
            productsLv.ItemsSource = productForLists.Where(item => item.Gender == gender);
            if (gender == BO.Gender.Women)
            {
                //TypeCB.ItemsSource = Enum.GetValues(typeof(BO.Clothing));
            }
            CategoryCB.Visibility = Visibility.Visible;
            CategoryL.Visibility = Visibility.Visible;
        }

        private void CategoryCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            category = (Category)CategoryCB.SelectedItem;
            TypeL.Visibility = Visibility.Visible;
            ColorL.Visibility = Visibility.Visible;
            SizeL.Visibility = Visibility.Visible;
            TypeCB.Visibility = Visibility.Visible;
            ColorCB.Visibility = Visibility.Visible;
            SizeCB.Visibility = Visibility.Visible;
        }

        private void TypeCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (category == Category.Clothing)
            {
                TypeCB.SelectedItem = Enum.GetValues(typeof(BO.Clothing));
                clothing = (Clothing)TypeCB.SelectedItem;
            }
            else
            {
                TypeCB.SelectedItem = Enum.GetValues(typeof(BO.Shoes));
                shoes = (Shoes)TypeCB.SelectedItem;
            }
        }

        private void ColorCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            color = (Color)ColorCB.SelectedItem;
        }

        private void TypeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (category == Category.Clothing)
            {
                SizeCB.SelectedItem = Enum.GetValues(typeof(BO.SizeClothing));
                sizeClothing = (SizeClothing)SizeCB.SelectedItem;
            }
            else
            {
                SizeCB.SelectedItem = Enum.GetValues(typeof(BO.SizeShoes));
                sizeShoes = (SizeShoes)SizeCB.SelectedItem;
            }
        }

        private void ChooseB(object sender, RoutedEventArgs e)
        {
            productsLv.ItemsSource = productForLists.Where(item => item.Gender == gender && item.Category == category &&
            (item.Clothing == clothing || item.Shoes == shoes) && item.Color == color &&(item.SizeClothing == sizeClothing
            || item.SizeShoes == sizeShoes));
        }

        private void doubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //new BO.Product().Show();
        }

        private void productsLv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
