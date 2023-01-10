using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for OrdersList.xaml
    /// </summary>
    public partial class OrdersList : Page
    {
        private BlApi.IBl? bl = BlApi.Factory.Get();

        //private string groupName = "Status";

        //private PropertyGroupDescription groupDescription;

        private ObservableCollection<BO.OrderForList?> OrderList
        { get => (ObservableCollection<BO.OrderForList?>)GetValue(OrderListDep); set => SetValue(OrderListDep, value); }

        private static DependencyProperty OrderListDep = DependencyProperty.Register(nameof(OrderList), 
            typeof(ObservableCollection<BO.OrderForList?>), typeof(OrdersList));
        public OrdersList()
        {
            InitializeComponent();

            OrderList = new ObservableCollection<BO.OrderForList?>(bl.Order.GetAll());
        }
        /// <summary>
        /// to update details of a specific product by double clicking the product in the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void doubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            BO.OrderForList selection = (BO.OrderForList)((ListView)sender).SelectedItem;
            MainWindow.mainFrame.Navigate(new TheOrderWindow(selection.ID));
            OrderList = new ObservableCollection<BO.OrderForList?>(bl!.Order.GetAll());
        }
    }
}
