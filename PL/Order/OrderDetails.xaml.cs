using System.Windows;

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for OrderDetails.xaml
    /// </summary>
    public partial class OrderDetails : Window
    {
        public BO.Order Order
        {
            get { return (BO.Order)GetValue(OrderProperty); }
            set { SetValue(OrderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Order.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrderProperty =
            DependencyProperty.Register("Order", typeof(BO.Order), typeof(OrderDetails));


        public OrderDetails(BO.Order order)
        {
            ///search the order with the recieved id and initialize the values in the text blocks accordingly
            InitializeComponent();

            Order = order;
        }
    }
}
