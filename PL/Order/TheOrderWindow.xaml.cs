using System.Windows;
using System.Windows.Controls;

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for TheOrderWindow.xaml
    /// </summary>
    public partial class TheOrderWindow : Page
    {
        private readonly BlApi.IBl? bl = BlApi.Factory.Get();

        public static readonly DependencyProperty OrderDep = DependencyProperty.Register(nameof(order), 
            typeof(BO.Order), typeof(TheOrderWindow));

        BO.Order order { get => (BO.Order)GetValue(OrderDep); set => SetValue(OrderDep, value); }
        public TheOrderWindow(int ID)
        {
            InitializeComponent();

            try
            {
                /// resets to show the current values
                order = bl.Order.OrderDetails(ID);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (tbSHIPDATE.Text == "" || tbSHIPDATE.Text == " ")
            {
                MessageBox.Show("Error - Ship date box can't be empty!", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (tbDELIVERYDATE.Text == "" || tbDELIVERYDATE.Text == " ")
            {
                MessageBox.Show("Error - Delivery box can't be empty!", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ///a try to update 
            try
            {
                bl!.Order.UpdateShipDate(order.ID);
                bl!.Order.UpdateDeliveryDate(order.ID);
                MessageBox.Show("Updated succesfuly!", "Saved order", MessageBoxButton.OK, MessageBoxImage.Information);

                MainWindow.mainFrame.Navigate(new OrdersList());
            }
            ///recieving error information from previous layer and showing the user with a message accordingly in case there is something wrong.
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Order error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
