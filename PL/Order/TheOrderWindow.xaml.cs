using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

            /// resets to show the current values
            order = bl.Order.OrderDetails(ID);
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
            }

            ///recieving error information from previous layer and showing the user with a message accordingly in case there is something wrong.
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
