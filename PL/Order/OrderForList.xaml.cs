﻿using PL.Order;
using System.Collections.Generic;
using System.Windows;

namespace PL
{
    /// <summary>
    /// Interaction logic for OrderForList.xaml
    /// </summary>
    public partial class OrderForList : Window
    {
        private BlApi.IBl? bl = BlApi.Factory.Get();

        public OrderForList(BlApi.IBl bl)
        {
            InitializeComponent();

            this.bl = bl;
            var v = bl.Order.GetAll();
            ordersLv.ItemsSource = bl.Order.GetAll();///to change name in xaml
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
