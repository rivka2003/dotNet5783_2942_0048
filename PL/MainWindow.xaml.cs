using System.Windows;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BlApi.IBl? bl = BlApi.Factory.Get();
        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// getting to the manager menue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Products_Button_Click(object sender, RoutedEventArgs e)
        {
            new ProductForList(bl!).Show();
        }
    }
}
