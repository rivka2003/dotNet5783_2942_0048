using BO;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace PL.Product
{
    /// <summary>
    /// Interaction logic for CatalogCustomer.xaml
    /// </summary>
    public partial class CatalogCustomer : Page
    {

        private readonly BlApi.IBl? bl = BlApi.Factory.Get();

        public BO.Cart Cart
        {
            get { return (BO.Cart)GetValue(CartProperty); }
            set { SetValue(CartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Cart.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CartProperty =
            DependencyProperty.Register("Cart", typeof(BO.Cart), typeof(CatalogCustomer));

        private ObservableCollection<ProductItem?> ProductItems
        {
            get { return (ObservableCollection<ProductItem?>)GetValue(productItemsProperty); }
            set { SetValue(productItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for productForLists.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty productItemsProperty =
            DependencyProperty.Register("ProductItems", typeof(ObservableCollection<ProductItem?>), typeof(CatalogCustomer));

        public IEnumerable<BO.Color> Color
        {
            get { return (IEnumerable<BO.Color>)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Color.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register("Color", typeof(IEnumerable<BO.Color>), typeof(CatalogCustomer));

        public IEnumerable<BO.Gender> Gender
        {
            get { return (IEnumerable<BO.Gender>)GetValue(GenderProperty); }
            set { SetValue(GenderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GenderProperty =
            DependencyProperty.Register("Gender", typeof(IEnumerable<BO.Gender>), typeof(CatalogCustomer));

        public IEnumerable<BO.Category> Category
        {
            get { return (IEnumerable<BO.Category>)GetValue(CategoryProperty); }
            set { SetValue(CategoryProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Category.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CategoryProperty =
            DependencyProperty.Register("Category", typeof(IEnumerable<BO.Category>), typeof(CatalogCustomer));

        public bool Click
        {
            get { return (bool)GetValue(ClickProperty); }
            set { SetValue(ClickProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Click.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ClickProperty =
            DependencyProperty.Register("Click", typeof(bool), typeof(CatalogCustomer));

        private readonly string groupName = "Category";
        readonly PropertyGroupDescription propertyGroupDescription;
        public ICollectionView CollectionViewProductItemList { set; get; }

        public CatalogCustomer(BO.Cart cart)
        {
            ProductItems = new ObservableCollection<ProductItem?>(bl.Product.GetAllOrderItems(Cart)!);
            Click = false;
            InitializeComponent();

            Cart = cart;
            /////resets the combo boxes options
            Color = Enum.GetValues(typeof(BO.Color)).Cast<BO.Color>();
            Gender = Enum.GetValues(typeof(BO.Gender)).Cast<BO.Gender>();
            Category = Enum.GetValues(typeof(BO.Category)).Cast<BO.Category>();

            CollectionViewProductItemList = CollectionViewSource.GetDefaultView(ProductItems);
            propertyGroupDescription = new PropertyGroupDescription(groupName);
            CollectionViewProductItemList.GroupDescriptions.Add(propertyGroupDescription);
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
        /// function to show the customer the product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_DoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (((ListView)sender) is not null)
            {
                BO.ProductItem selection = (BO.ProductItem)((ListView)sender).SelectedItem;
                MainWindow.mainFrame.Navigate(new ProductView(Cart, selection.ID));
            }
        }

        /// <summary>
        /// show the full list without a specific sort.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearB(object sender, RoutedEventArgs e)
        {
            Click = false;
        }

        /// <summary>
        /// adding the specific product to the cart 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    var p = ProductItems.First(p => p!.ID == productId);
                    ProductItems[ProductItems.IndexOf(p)] = bl!.Product.ProductDetailsForCustomer(productId, Cart);
                }
            }
            ///recieving error information from previous layer and showing the user with a message accordingly in case there is something wrong
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
