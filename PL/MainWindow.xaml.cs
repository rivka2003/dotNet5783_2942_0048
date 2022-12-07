using BlApi;
using BlImplementation;
using PL.Product;
using System.Windows;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IBl _bl = new Bl();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Products_Button_Click(object sender, RoutedEventArgs e)
        {
            new ProductForList(_bl).Show();
        }

        private void Add_Product_Button_Click(object sender, RoutedEventArgs e)
        {
            new adding(_bl).Show();
        }

       
    }
}
