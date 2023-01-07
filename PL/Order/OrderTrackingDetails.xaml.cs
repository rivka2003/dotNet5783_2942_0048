using System.Windows;

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for OrderTrackingDetails.xaml
    /// </summary>
    public partial class OrderTrackingDetails : Window
    {
        public OrderTrackingDetails(int id, BO.OrderTracking orderTracking)
        {
            ///search the order with the recieved id and initialize the values in the text blocks accordingly
            InitializeComponent();
            lblID.Content = id;
            lblSTATUS.Content = orderTracking.Status;
        }
    }
}
