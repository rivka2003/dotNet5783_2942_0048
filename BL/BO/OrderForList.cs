using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class OrderForList
    {
        /// <summary>
        /// The ID of the order
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// The customer name
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// The status of the order
        /// </summary>
        public BO.OrderStatus Status { get; set; }
        /// <summary>
        /// The amount of items that are in the order
        /// </summary>
        public int AmountOfItems { get; set; }
        /// <summary>
        /// The total price of the order
        /// </summary>
        public double TotalPrice { get; set; }
        public override string ToString() => $@"
        ID: {ID}
        CustomerName: {CustomerName}
        Status: {Status}
        AmountOfItems: {AmountOfItems}
        TotalPrice: {TotalPrice}";
    }
}
