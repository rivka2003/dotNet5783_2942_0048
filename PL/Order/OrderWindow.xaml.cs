using PL.Product;
using System.Windows;

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        private readonly BlApi.IBl? bl = BlApi.Factory.Get();

        public static readonly DependencyProperty OrderDep = DependencyProperty.Register(nameof(order), typeof(BO.Order), typeof(OrderWindow));
        BO.Order order { get => (BO.Order)GetValue(OrderDep); set => SetValue(OrderDep, value); }
        public OrderWindow(int ID)/// constructor to open an update window/
        {
            InitializeComponent();

            /// resets to show the current values
            order = bl.Order.OrderDetails(ID);
            //tbID.Text = order.ID.ToString();
            //tbcNAME.Text = order.CustomerName;
            //tbADDRESS.Text = order.CustomerAddress;
            //tbORDERDATE.Text = order.OrderDate.ToString();
            //tbSHIPDATE.Text = order.ShipDate.ToString();
            //tbDELIVERYDATE.Text = order.DeliveryDate.ToString();

            lblx5.Visibility = Visibility.Hidden;
            lblx6.Visibility = Visibility.Hidden;
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

            ///if pre checks are valid put data in the product and send that to previous layers check.
            //BO.Order order = new BO.Order()
            //{
            //    ID = int.Parse(tbID.Text),
            //    CustomerName = tbcNAME.Text,
            //    //InStock = int.Parse(tbINSTOCK.Text),
            //    CustomerAddress = tbADDRESS.Text,
            //    OrderDate = DateTime.Parse(tbORDERDATE.Text),
            //    ShipDate = DateTime.Parse(tbSHIPDATE.Text),
            //    DeliveryDate = DateTime.Parse(tbDELIVERYDATE.Text),

            //};


            ///a try to update 
            try
            {
                if (btnUPDATE is null)
                    return;
                if (btnUPDATE.Content is "UPDATE") ///for updating
                {
                    bl!.Order.UpdateShipDate(order.ID);
                    bl!.Order.UpdateDeliveryDate(order.ID);
                    MessageBox.Show("Updated succesfuly!", "Saved order", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                Close();
            }

            ///recieving error information from previous layer and showing the user with a message accordingly in case there is something wrong.
            catch (BO.NotValid ex)
            {
                MessageBox.Show(ex.ToString(), "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.NonFoundObjectBo ex)
            {
                MessageBox.Show(ex.ToString(), "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.ExistingObjectBo ex)
            {
                MessageBox.Show(ex.ToString(), "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
} 
