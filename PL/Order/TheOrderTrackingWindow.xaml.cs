using BO;
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

        public BO.OrderTracking OrderTracking
        {
            get { return (BO.OrderTracking)GetValue(OrderTrackingProperty); }
            set { SetValue(OrderTrackingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OrderTracking.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrderTrackingProperty =
            DependencyProperty.Register("OrderTracking", typeof(BO.OrderTracking), typeof(TheOrderTrackingWindow));

        public bool IsClicked
        {
            get { return (bool)GetValue(IsClickedProperty); }
            set { SetValue(IsClickedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsClicked.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsClickedProperty =
            DependencyProperty.Register("IsClicked", typeof(bool), typeof(TheOrderTrackingWindow));


        public TheOrderTrackingWindow()
        {
            IsClicked = false;
            InitializeComponent();
        }

        //enables numbers only
        private void PreviewTextInputDigitsIDInStock(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        //resets order tracking variable according to the id that was entered 
        private void tbID_TextChanged(object sender, TextChangedEventArgs e)
        {
            string myId = ((TextBox)sender).Text;
            if (string.IsNullOrEmpty(myId))
                return;
            ID = int.Parse(((TextBox)sender).Text);
            try
            {
                OrderTracking = bl.Order.TrackingOrder(ID);
            }
            ///recieving error information from previous layer and showing the user with a message accordingly in case there is something wrong.
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //button leads to detail's window of the current order to which the given id belongs to
        private void btnORDERDETAILS_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow.mainFrame.Navigate(new TheOrderDetails(bl.Order.OrderDetails(ID)));
            }
            ///recieving error information from previous layer and showing the user with a message accordingly in case there is something wrong.
            catch (BO.NonFoundObjectBo ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            IsClicked = true;
        }
    }
}
