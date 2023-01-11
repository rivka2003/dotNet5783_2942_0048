using BlApi;
using System.Windows;
using System.Windows.Controls;

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for TheOrderTrackingDetails.xaml
    /// </summary>
    public partial class TheOrderTrackingDetails : Page
    {

        static readonly IBl bl = Factory.Get();
        public BO.OrderTracking OrderTracking
        {
            get { return (BO.OrderTracking)GetValue(OrderTrackingProperty); }
            set { SetValue(OrderTrackingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Order tracking.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrderTrackingProperty =
            DependencyProperty.Register("OrderTracking", typeof(BO.OrderTracking), typeof(TheOrderTrackingDetails));

        public TheOrderTrackingDetails(int ID)
        {
            try///search the order with the recieved id and initialize the values in the text blocks accordingly
            {
                OrderTracking = bl.Order.TrackingOrder(ID);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            InitializeComponent();
        }
    }
}