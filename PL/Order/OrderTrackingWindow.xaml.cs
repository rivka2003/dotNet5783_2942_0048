using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for OrderTrackingWindow.xaml
    /// </summary>
    public partial class OrderTrackingWindow : Window
    {
        private readonly BlApi.IBl bl = BlApi.Factory.Get();
        int ID;
        BO.Order order = new BO.Order();
        BO.OrderTracking orderTracking = new BO.OrderTracking();

        public OrderTrackingWindow()
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
                new OrderTrackingDetails(ID, bl.Order.TrackingOrder(ID)).ShowDialog();
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
                new OrderDetails(bl.Order.OrderDetails(ID)).ShowDialog();
            }

            catch (BO.NonFoundObjectBo ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
