using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for TheOrderTrackingDetails.xaml
    /// </summary>
    public partial class TheOrderTrackingDetails : Page
    {
        public BO.OrderTracking OrderTracking
        {
            get { return (BO.OrderTracking)GetValue(OrderTrackingProperty); }
            set { SetValue(OrderTrackingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Order tracking.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrderTrackingProperty =
            DependencyProperty.Register("OrderTracking", typeof(BO.OrderTracking), typeof(TheOrderTrackingDetails));
        public TheOrderTrackingDetails(BO.OrderTracking orderTracking)
        {
            ///search the order with the recieved id and initialize the values in the text blocks accordingly
            InitializeComponent();
            OrderTracking = orderTracking;
        }
    }
}
