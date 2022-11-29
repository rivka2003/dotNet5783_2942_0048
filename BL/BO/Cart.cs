﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Cart
    {
        /// <summary>
        /// The name of the customer
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// The email of the customer
        /// </summary>
        public string CustomerEmail { get; set; }
        /// <summary>
        /// The addresss of the customer
        /// </summary>
        public string CustomerAddress { get; set; }
        /// <summary>
        /// The list of all the items from the order
        /// </summary>
        public List<BO.OrderItem> Items { get; set; }
        /// <summary>
        /// The total price of the cart
        /// </summary>
        public double TotalPrice { get; set; }
        public override string ToString()
        {
return$@"
Customer name: {CustomerName}
Customer email: {CustomerEmail}
Customer address: {CustomerAddress}
Items: {string.Join("\n", Items)}
Total price: {TotalPrice}
";
        }
    }
}
