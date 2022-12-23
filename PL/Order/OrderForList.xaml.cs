
using PL.Order;
using PL.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PL
{
    /// <summary>
    /// Interaction logic for ProductForList.xaml
    /// </summary>
    public partial class OrderForList : Window
    {
        private BlApi.IBl? bl = BlApi.Factory.Get();

        private IEnumerable<BO.OrderForList> orderForLists;
        public OrderForList(BlApi.IBl bl)
        {
            InitializeComponent();

            this.bl = bl;
            ordersLv.ItemsSource = bl.Order.GetAll();///to change name in xaml
            orderForLists = bl.Order.GetAll()!;

          
        }

      

        /// <summary>
        /// to update details of a specific product by double clicking the product in the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void doubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            int ID = ((BO.OrderForList)ordersLv.SelectedItem).ID;
            new OrderWindow(ID).ShowDialog();
            ordersLv.ItemsSource = bl!.Order.GetAll();
        }


    }
}
