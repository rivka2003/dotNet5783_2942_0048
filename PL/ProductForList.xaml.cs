using BlApi;
using BlImplementation;
using BO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

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
            productForLists = bl.Product.GetAll();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
            TypeSelector_Copy.ItemsSource = Enum.GetValues(typeof(BO.Clothing));
        }

        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CategorySelector.SelectedItem is BO.Category category)
            {
                productsLv.ItemsSource = productForLists.Where(x => x.Category == category);
            }
        }

        private void ProductListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TypeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
