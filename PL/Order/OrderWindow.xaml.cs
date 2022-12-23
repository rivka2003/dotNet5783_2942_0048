using System;
using System.Windows;
using System.Windows.Controls;

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        private readonly BlApi.IBl? bl = BlApi.Factory.Get();
        public OrderWindow(int ID)/// constructor to open an update window/
        {
            InitializeComponent();

            /// resets to show the current values
            BO.Order order = bl.Order.OrderDetails(ID);
            tbID.Text = order.ID.ToString();
            tbcNAME.Text = order.CustomerName;
            tbADDRESS.Text = order.CustomerAddress;
            tbORDERDATE.Text = order.OrderDate.ToString();
            tbSHIPDATE.Text = order.ShipDate.ToString();
            tbDELIVERYDATE.Text = order.DeliveryDate.ToString();

            ///allowing changes only in the places that are connected to the manager
            tbID.IsEnabled = false;
            tbcNAME.IsEnabled = false;
            tbADDRESS.IsEnabled = false;
            tbORDERDATE.IsEnabled = false;

            lblx1.Visibility = Visibility.Hidden;
            lblx2.Visibility = Visibility.Hidden;
            lblx3.Visibility = Visibility.Hidden;
            lblx4.Visibility = Visibility.Hidden;
            lblx5.Visibility = Visibility.Hidden;
            lblx6.Visibility = Visibility.Hidden;
        }




        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            ///if pre checks are valid put data in the product and send that to previous layers check.
            BO.Order order = new BO.Order()
            {
                ID = int.Parse(tbID.Text),
                CustomerName = tbcNAME.Text,
                //InStock = int.Parse(tbINSTOCK.Text),
                CustomerAddress = tbADDRESS.Text,
                OrderDate = DateTime.Parse(tbORDERDATE.Text),
                ShipDate = DateTime.Parse(tbSHIPDATE.Text),
                DeliveryDate = DateTime.Parse(tbDELIVERYDATE.Text),

            };

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

        private void tbSHIPDATE_TextChanged(object sender, TextChangedEventArgs e)
        {
            //    if (lblCHECK2 is null || lblx2 is null)
            //        return;
            //    if (valid check)
            //    {
            //        lblCHECK2.Visibility = Visibility.Hidden;
            //        lblx2.Visibility = Visibility.Visible;
            //        return;
            //    }
            //    lblx2.Visibility = Visibility.Hidden;
            //    lblCHECK2.Visibility = Visibility.Visible;
        }

        private void tbDELIVERYDATE_TextChanged(object sender, TextChangedEventArgs e)
        {
            //    if (lblCHECK3 is null || lblx3 is null)
            //        return;
            //    if (valid check)
            //    {
            //        lblCHECK3.Visibility = Visibility.Hidden;
            //        lblx3.Visibility = Visibility.Visible;
            //        return;
            //    }
            //    lblx3.Visibility = Visibility.Hidden;
            //    lblCHECK3.Visibility = Visibility.Visible;
        }
    }


    ///what more needs to b done?  v x labels for ship and delivery date only and checks about the valid data 
}
