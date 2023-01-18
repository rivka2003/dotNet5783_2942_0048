using System.Windows;
using System.Windows.Controls;

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for TheOrderDetails.xaml
    /// </summary>
    public partial class TheOrderDetails : Page
    {
        public BO.Order Order
        {
            get { return (BO.Order)GetValue(OrderProperty); }
            set { SetValue(OrderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Order.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrderProperty =
            DependencyProperty.Register("Order", typeof(BO.Order), typeof(TheOrderDetails));

        public TheOrderDetails(BO.Order order)
        {
            InitializeComponent();

            Order = order;
        }
    }
}
