using DocumentFormat.OpenXml.Wordprocessing;
using PL.Order;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Data;
using BO;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;

namespace PL
{
    /// <summary>
    /// Interaction logic for OrderForList.xaml
    /// </summary>
    public partial class OrderForList : Window
    {
        private BlApi.IBl? bl = BlApi.Factory.Get();

        //private string groupName = "Status";

        //private PropertyGroupDescription groupDescription;

        private ObservableCollection<BO.OrderForList?> OrderList
        { get => (ObservableCollection<BO.OrderForList?>)GetValue(OrderListDep); set => SetValue(OrderListDep, value); }

        private static DependencyProperty OrderListDep = DependencyProperty.Register(nameof(OrderList), typeof(ObservableCollection<BO.OrderForList?>), typeof(OrderForList));
        //IEnumerable<BO.OrderForList> OrderList { get => (IEnumerable<BO.OrderForList>)GetValue(OrderListDep); set => SetValue(OrderListDep, value); }

        //public ICollectionView CollectionViewProductItemList { set; get; }

        public OrderForList()
        {
            InitializeComponent();
            //OrderList = new ObservableCollection<BO.OrderForList?>(bl.Order.GetAll());///to change name in xaml

            OrderList = new ObservableCollection<BO.OrderForList?>(bl.Order.GetAll());
            //CollectionViewProductItemList = CollectionViewSource.GetDefaultView(OrderList);
            //groupDescription = new PropertyGroupDescription(groupName);
            //CollectionViewProductItemList.GroupDescriptions.Add(groupDescription);
        }

        /// <summary>
        /// to update details of a specific product by double clicking the product in the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void doubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            BO.OrderForList selection = (BO.OrderForList)((ListView)sender).SelectedItem;
            new OrderWindow(selection.ID).ShowDialog();
            ordersLv.ItemsSource = bl!.Order.GetAll();
        }
    }
}