using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for TheOrderTrackingWindow.xaml
    /// </summary>
    public partial class TheOrderTrackingWindow : Page
    {
        private readonly BlApi.IBl bl = BlApi.Factory.Get();
        int ID;
        BO.Order order = new BO.Order();
        BO.OrderTracking orderTracking = new BO.OrderTracking();
        public TheOrderTrackingWindow()
        {
            InitializeComponent();
        }

        private void PreviewTextInputDigitsIDInStock(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void tbID_TextChanged(object sender, TextChangedEventArgs e)
        {
            ID = int.Parse(((TextBox)sender).Text);
        }

        private void btnORDERTRACKING_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow.mainFrame.Navigate(new TheOrderTrackingDetails(bl.Order.TrackingOrder(ID)));
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnORDERDETAILS_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow.mainFrame.Navigate(new TheOrderDetails(bl.Order.OrderDetails(ID)));
            }

            catch (BO.NonFoundObjectBo ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
