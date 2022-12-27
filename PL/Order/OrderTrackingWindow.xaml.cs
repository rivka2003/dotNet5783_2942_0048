using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
            btnORDERDETAILS.IsEnabled = false;
            btnORDERTRACKING.IsEnabled = false;

        }
        private void PreviewTextInputDigitsIDInStock(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void tbID_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbID.Text.Length <= 8)
            {
                ID = int.Parse(tbID.Text);
                btnORDERDETAILS.IsEnabled = true;
                btnORDERTRACKING.IsEnabled = true;
                
            }
            else
            {
                btnORDERDETAILS.IsEnabled = false;
                btnORDERTRACKING.IsEnabled = false;
            }

        }

        private void btnORDERTRACKING_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                orderTracking = bl.Order.TrackingOrder(ID);
                new OrderTrackingDetails(ID, orderTracking).ShowDialog();
            }

            catch (BO.NonFoundObjectBo ex)
            {
                MessageBox.Show(ex.ToString(), "Save error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
       
        

        private void btnORDERDETAILS_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                order = bl.Order.OrderDetails(ID);
                new OrderDetails(order).ShowDialog();
            }

            catch (BO.NonFoundObjectBo ex)
            {
                MessageBox.Show(ex.ToString(), "Save error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

      
    }
}
