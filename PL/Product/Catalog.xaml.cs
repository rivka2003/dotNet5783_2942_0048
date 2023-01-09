using BO;
using PL.Carts;
using PL.Product;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace PL
{
    /// <summary>
    /// Interaction logic for Catalog.xaml
    /// </summary>
    public partial class Catalog : Page
    {
        private readonly BlApi.IBl? bl = BlApi.Factory.Get();

        public BO.Cart Cart
        {
            get { return (BO.Cart)GetValue(CartProperty); }
            set { SetValue(CartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Cart.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CartProperty =
            DependencyProperty.Register("Cart", typeof(BO.Cart), typeof(Catalog));

        private ObservableCollection<BO.ProductForList> productForLists
        {
            get { return (ObservableCollection<BO.ProductForList>)GetValue(productForListsProperty); }
            set { SetValue(productForListsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for productForLists.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty productForListsProperty =
            DependencyProperty.Register("productForLists", typeof(ObservableCollection<BO.ProductForList>), typeof(Catalog));

        private ObservableCollection<BO.ProductItem> productItems
        {
            get { return (ObservableCollection<BO.ProductItem>)GetValue(productItemsProperty); }
            set { SetValue(productItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for productForLists.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty productItemsProperty =
            DependencyProperty.Register("productItems", typeof(ObservableCollection<BO.ProductItem>), typeof(Catalog));

        public IEnumerable<BO.Color> Color
        {
            get { return (IEnumerable<BO.Color>)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Color.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register("Color", typeof(IEnumerable<BO.Color>), typeof(Catalog));

        public IEnumerable<BO.Gender> Gender
        {
            get { return (IEnumerable<BO.Gender>)GetValue(GenderProperty); }
            set { SetValue(GenderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GenderProperty =
            DependencyProperty.Register("Gender", typeof(IEnumerable<BO.Gender>), typeof(Catalog));

        public IEnumerable<BO.Category> Category
        {
            get { return (IEnumerable<BO.Category>)GetValue(CategoryProperty); }
            set { SetValue(CategoryProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Category.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CategoryProperty =
            DependencyProperty.Register("Category", typeof(IEnumerable<BO.Category>), typeof(Catalog));

        public bool isManager
        {
            get { return (bool)GetValue(isManagerProperty); }
            set { SetValue(isManagerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for isManager.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty isManagerProperty =
            DependencyProperty.Register("isManager", typeof(bool), typeof(Catalog));

        private string groupName = "Category";
        PropertyGroupDescription propertyGroupDescription;
        public ICollectionView CollectionViewProductItemList { set; get; }

        public Catalog(BO.Cart cart, bool IsManager)
        {
            isManager = IsManager;
            InitializeComponent();

            Cart = cart;
            /////resets the combo boxes options
            Color = Enum.GetValues(typeof(BO.Color)).Cast<BO.Color>();
            Gender = Enum.GetValues(typeof(BO.Gender)).Cast<BO.Gender>();
            Category = Enum.GetValues(typeof(BO.Category)).Cast<BO.Category>();
            //if (IsManager)
            //    productForLists = new ObservableCollection<BO.ProductForList>(bl.Product.GetAll()!);
            productForLists = new ObservableCollection<BO.ProductForList>(bl.Product.GetAll()!);
            //else
            //    productItems = new ObservableCollection<BO.ProductItem>(bl.Product.GrupingByChoos(BO.Gender.Women, cart)!);
           
        CollectionViewProductItemList = CollectionViewSource.GetDefaultView(productForLists);
            
            propertyGroupDescription = new PropertyGroupDescription(groupName);
            CollectionViewProductItemList.GroupDescriptions.Add(propertyGroupDescription);
           // CollectionViewProductItemList.GroupDescriptions.Clear();
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
                productForLists = new ObservableCollection<BO.ProductForList>(productForLists.Where(item => item.Gender == (BO.Gender)GenderCB.SelectedItem &&
                item.Category == (BO.Category)CategoryCB.SelectedItem && item.Color == (BO.Color)ColorCB.SelectedItem &&
                item.Clothing == (BO.Clothing)TypeCB.SelectedItem && item.SizeClothing == (BO.SizeClothing)SizeCB.SelectedItem));
            }
            else
            {
                productForLists = new ObservableCollection<BO.ProductForList>(productForLists.Where(item => item.Gender == (BO.Gender)GenderCB.SelectedItem &&
                item.Category == (BO.Category)CategoryCB.SelectedItem && item.Color == (BO.Color)ColorCB.SelectedItem &&
                item.Shoes == (BO.Shoes)TypeCB.SelectedItem && item.SizeShoes == (BO.SizeShoes)SizeCB.SelectedItem));
            }
        }

        /// <summary>
        /// to update details of a specific product by double clicking the product in the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update_DoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            int ID = ((BO.ProductForList)productsLv.SelectedItem).ID;
            if (isManager)
            {
                MainWindow.mainFrame.Navigate(new TheProductWindow(ID));
                productForLists = new ObservableCollection<BO.ProductForList>(bl!.Product.GetAll()!);
            }
            else
            {
                //new ProductItemView(ID).ShowDialog();
                MainWindow.mainFrame.Navigate(new ProductView(Cart, ID));
            }
        }

        /// <summary>
        /// to add a new product to the product's list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Product_Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.mainFrame.Navigate(new TheProductWindow());
            productForLists = new ObservableCollection<BO.ProductForList>(bl!.Product.GetAll()!);
        }

        /// <summary>
        /// show the full list without a specific sort.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearB(object sender, RoutedEventArgs e)
        {
            productForLists = new ObservableCollection<BO.ProductForList>(bl!.Product.GetAll()!);
        }

        private void AddToCart_Product_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FrameworkElement frameworkElement = (sender as FrameworkElement)!;
                int productId;
                if (frameworkElement is not null && frameworkElement.DataContext is not null)
                {
                    productId = ((ProductItem)(frameworkElement.DataContext)).ID;
                    bl?.Cart.AddProductToCart(Cart, productId, 1);
                    var p = productForLists.First(p => p.ID == productId);
                    //productForLists[productForLists.IndexOf(p)] = bl?.Product.GetProductDetailsForCustomer(productId, cart);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Out of stock!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
    }
}
