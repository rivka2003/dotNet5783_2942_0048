using System.Windows;

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for OrderDetails.xaml
    /// </summary>
    public partial class OrderDetails : Window
    {
     
        public OrderDetails(BO.Order order)
        {
            ///search the order with the recieved id and initialize the values in the text blocks accordingly
            InitializeComponent();

                lblID.Content = order.ID;
                lblADDRESS.Content = order.CustomerAddress;
                lblNAME.Content = order.CustomerName;
                lblDELIVERYDATE.Content = order.DeliveryDate;
                lblORDERDATE.Content = order.OrderDate;
                lblSHIPDATE.Content = order.ShipDate;
                lblDELIVERYDATE.Content = order.DeliveryDate;
        }
    }
}
