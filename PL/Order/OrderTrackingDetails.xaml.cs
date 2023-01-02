﻿using System;
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
using System.Windows.Shapes;

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for OrderTrackingDetails.xaml
    /// </summary>
    public partial class OrderTrackingDetails : Window
    {
        public OrderTrackingDetails(int id, BO.OrderTracking orderTracking)
        {
            ///search the order with the recieved id and initialize the values in the text blocks accordingly
            InitializeComponent();
            lblID.Content = id;
            lblSTATUS.Content = orderTracking.Status;
        }
    }
}