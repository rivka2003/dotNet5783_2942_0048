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

        /// <summary>
        /// getting to main menue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Products_Button_Click(object sender, RoutedEventArgs e)
        {
            new ProductForList(_bl).Show();
        }
    }
}
