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
    /// Interaction logic for ProductForList.xaml
    /// </summary>
    public partial class ProductForList : Window
    {
        private bool isContentVisible = false;

        private readonly BlApi.IBl? bl = BlApi.Factory.Get();

        private readonly static BO.Cart cart = new();



        private ObservableCollection<BO.ProductForList> productForLists
        {
            get { return (ObservableCollection<BO.ProductForList>)GetValue(productForListsProperty); }
            set { SetValue(productForListsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for productForLists.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty productForListsProperty =
            DependencyProperty.Register("productForLists", typeof(ObservableCollection<BO.ProductForList>), typeof(ProductForList));

        public IEnumerable<BO.SizeClothing> SizeClothing
        {
            get { return (IEnumerable<BO.SizeClothing>)GetValue(SizeClothingProperty); }
            set { SetValue(SizeClothingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SizeClothingProperty =
            DependencyProperty.Register("SizeClothing", typeof(IEnumerable<BO.SizeClothing>), typeof(ProductForList));


        //readonly IEnumerable<BO.Clothing> itemsClothing = Enum.GetValues(typeof(BO.Clothing)).Cast<BO.Clothing>();

        //readonly IEnumerable<BO.Shoes> itemsShoes = Enum.GetValues(typeof(BO.Shoes)).Cast<BO.Shoes>();

        readonly IEnumerable<int> SizeShoes = new int[] { 36, 37, 38, 39, 40, 41, 42, 43, 44, 45 };

        public IEnumerable<BO.Color> Color
        {
            get { return (IEnumerable<BO.Color> )GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Color.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register("Color", typeof(IEnumerable<BO.Color> ), typeof(ProductForList));

        public IEnumerable<BO.Gender> Gender
        {
            get { return (IEnumerable<BO.Gender> )GetValue(GenderProperty); }
            set { SetValue(GenderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GenderProperty =
            DependencyProperty.Register("Gender", typeof(IEnumerable<BO.Gender>), typeof(ProductForList));

        public IEnumerable<BO.Category> Category
        {
            get { return (IEnumerable<BO.Category>)GetValue(CategoryProperty); }
            set { SetValue(CategoryProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Category.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CategoryProperty =
            DependencyProperty.Register("Category", typeof(IEnumerable<BO.Category>), typeof(ProductForList));

        readonly bool isManager;

        public ICollectionView CollectionViewProductItemList { set; get; }
        public ProductForList(bool IsManager)
        {
            InitializeComponent();

            isManager = IsManager;
            Color = Enum.GetValues(typeof(BO.Color)).Cast<BO.Color>();

            productForLists = new ObservableCollection<BO.ProductForList>(bl.Product.GetAll()!);

          

            /////resets the combo boxes options
            //GenderCB.ItemsSource = Enum.GetValues(typeof(BO.Gender));
            //CategoryCB.ItemsSource = Enum.GetValues(typeof(BO.Category));
            //ColorCB.ItemsSource = Enum.GetValues(typeof(BO.Color));

            /// Default filling of the combo box with values
            //AddItemsWithPredicate(TypeCB.Items, itemsClothing);
            //AddItemsWithPredicate(SizeCB.Items, SizeClothing);

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

            Gender selection = (Gender)((ComboBox)sender).SelectedItem;

            bl!.Product.GrupingByChoos(selection, cart);

            ///// Clearing the combo box before re-adding
            ////TypeCB.Items.Clear();
            ////TypeCB.ItemsSource = null;
            ////SizeCB.Items.Clear();
            ////SizeCB.ItemsSource = null;

            //if (CategoryCB.SelectedItem is BO.Category.Clothing) ///in case clothing was chosen
            //{
            //    AddItemsWithPredicate(SizeCB.Items, SizeClothing);

            //    ///resets the options inside the cb according to the chosen gender
            //    if (GenderCB.SelectedItem is not BO.Gender.Women && GenderCB.SelectedItem is not BO.Gender.Girls)
            //        AddItemsWithPredicate(TypeCB.Items, itemsClothing, item => item is not BO.Clothing.Dresses && item is not BO.Clothing.Skirts);
            //    else
            //        AddItemsWithPredicate(TypeCB.Items, itemsClothing);
            //}
            //else ///in case shoes was chosen
            //{
            //    AddItemsWithPredicate(SizeCB.Items, SizeShoes);

            //    ///resets the options inside the cb according to the chosen gender 
            //    if (GenderCB.SelectedItem is not BO.Gender.Women)
            //        AddItemsWithPredicate(TypeCB.Items, itemsShoes, item => item is not BO.Shoes.Heels);
            //    else
            //        AddItemsWithPredicate(TypeCB.Items, itemsShoes);
            //}
            //TypeCB.SelectedIndex = 0;
            //SizeCB.SelectedIndex = 0;
        }

        //private static void AddItemsWithPredicate<T>(ItemCollection itemCollection, IEnumerable<T> Collection, Predicate<T> predicate = null!)
        //{
        //    foreach (T item in Collection)
        //    {
        //        if (predicate is null)
        //            itemCollection.Add(item);
        //        else if (predicate(item))
        //            itemCollection.Add(item);
        //    }
        //}

        /// <summary>
        /// sorting the presented data according to the user's choices
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChooseB(object sender, RoutedEventArgs e)
        {
            if (CategoryCB.SelectedItem is BO.Category.Clothing)
            {
                productsLv.DataContext = productForLists.Where(item => item.Gender == (BO.Gender)GenderCB.SelectedItem &&
                item.Category == (BO.Category)CategoryCB.SelectedItem && item.Color == (BO.Color)ColorCB.SelectedItem &&
                item.Clothing == (BO.Clothing)TypeCB.SelectedItem && item.SizeClothing == (BO.SizeClothing)SizeCB.SelectedItem);
            }
            else
            {
                productsLv.DataContext = productForLists.Where(item => item.Gender == (BO.Gender)GenderCB.SelectedItem &&
                item.Category == (BO.Category)CategoryCB.SelectedItem && item.Color == (BO.Color)ColorCB.SelectedItem &&
                item.Shoes == (BO.Shoes)TypeCB.SelectedItem && item.SizeShoes == (BO.SizeShoes)SizeCB.SelectedItem);
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
                new ProductWindow(ID).ShowDialog();
                //productsLv.ItemsSource = bl!.Product.GetAll();
            }
            else
            {
                new ProductItemView(ID).ShowDialog();
            }
        }

        /// <summary>
        /// to add a new product to the product's list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Product_Button_Click(object sender, RoutedEventArgs e)
        {
            new ProductWindow().ShowDialog();
            //productsLv.ItemsSource = bl!.Product.GetAll();
        }

        /// <summary>
        /// show the full list without a specific sort.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearB(object sender, RoutedEventArgs e)
        {
            productsLv.DataContext = productForLists.Select(item => item);
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            isContentVisible = !isContentVisible;
            Labels.Visibility = isContentVisible ? Visibility.Visible : Visibility.Collapsed;
            ComboBoxes.Visibility = isContentVisible ? Visibility.Visible : Visibility.Collapsed;
            chooseB.Visibility = isContentVisible ? Visibility.Visible : Visibility.Collapsed;
            clearB.Visibility = isContentVisible ? Visibility.Visible : Visibility.Collapsed;
        }

        private void Cart_Button_Click(object sender, RoutedEventArgs e)
        {
            new CartWindow(cart).ShowDialog();
        }
    }
}
