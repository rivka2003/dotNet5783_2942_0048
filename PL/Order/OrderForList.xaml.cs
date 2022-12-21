using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
        }
    }
}
