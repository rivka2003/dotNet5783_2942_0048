using System;
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
        public IEnumerable<BO.OrderItem> Items { get; set; }
        /// <summary>
        /// The total price of the cart
        /// </summary>
        public double TotalPrice { get; set; }
        public override string ToString() => $@"
        CustomerName: {CustomerName}
        CustomerEmail: {CustomerEmail}
        CustomerAddress: {CustomerAddress}
        Items: {string.Join(", ", Items)}
        TotalPrice: {TotalPrice}";
    }
}
