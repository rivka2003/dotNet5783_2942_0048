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

        // Using a DependencyProperty as the backing store for isManager.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty isManagerProperty =
            DependencyProperty.Register("isManager", typeof(bool), typeof(Catalog));

        public BO.Product Product
        {
            get { return (BO.Product)GetValue(ProductProperty); }
            set { SetValue(ProductProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Product.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProductProperty =
            DependencyProperty.Register("Product", typeof(BO.Product), typeof(Catalog));

        public bool Click
        {
            get { return (bool)GetValue(ClickProperty); }
            set { SetValue(ClickProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Click.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ClickProperty =
            DependencyProperty.Register("Click", typeof(bool), typeof(Catalog));

        private string groupName = "Category";
        PropertyGroupDescription propertyGroupDescription;
        public ICollectionView CollectionViewproductForListsList { set; get; }

        public Catalog(BO.Cart cart)
        {
            productForLists = new ObservableCollection<BO.ProductForList>(bl.Product.GetAll()!);
            Click = false;
            InitializeComponent();

            Cart = cart;
            ///resets the combo boxes options
            Color = Enum.GetValues(typeof(BO.Color)).Cast<BO.Color>();
            Gender = Enum.GetValues(typeof(BO.Gender)).Cast<BO.Gender>();
            Category = Enum.GetValues(typeof(BO.Category)).Cast<BO.Category>();

            CollectionViewproductForListsList = CollectionViewSource.GetDefaultView(productForLists);

            propertyGroupDescription = new PropertyGroupDescription(groupName);
            CollectionViewproductForListsList.GroupDescriptions.Add(propertyGroupDescription);
        }

        /// <summary>
        /// sorting the presented data according to the user's choices
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChooseB(object sender, RoutedEventArgs e)
        {
            Click = true;
           
        }

        /// <summary>
        /// to update details of a specific product by double clicking the product in the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update_DoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (((ListView)sender) is not null)
            {
                BO.ProductForList selection = (BO.ProductForList)((ListView)sender).SelectedItem;
                MainWindow.mainFrame.Navigate(new TheProductWindow(false, Cart, selection.ID));
                productForLists = new ObservableCollection<BO.ProductForList>(bl!.Product.GetAll()!);
            }
        }

        /// <summary>
        /// removing a specific productfrom the catalog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_Product_Button_Click(object sender, RoutedEventArgs e)
        {
            BO.ProductForList selection = (BO.ProductForList)((Button)sender).DataContext;
            try
            {  
                bl!.Product.DeleteProduct(selection.ID);
                productForLists.Remove(selection);
            }
            ///recieving error information from previous layer and showing the user with a message accordingly in case there is something wrong.
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        /// <summary>
        /// button that opens a window for adding a new product to the product's category
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Product_Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.mainFrame.Navigate(new TheProductWindow(true, Cart));
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
    }
}
