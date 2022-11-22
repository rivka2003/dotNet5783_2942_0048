using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Order
    {
        public int ID { get; set; }
        /// <summary>
        /// The ID of the Order
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// The name of the customer
        /// </summary>
        public string CustomerEmail { get; set; }
        /// <summary>
        /// The email of the customer
        /// </summary>
        public string CustomerAddress { get; set; }
        /// <summary>
        /// The address of the customer
        /// </summary>
        public DateTime OrderDate { get; set; }
        /// <summary>
        /// The date when the order was confirmed
        /// </summary>
        public BO.OrderStatus Status { get; set; }
        /// <summary>
        /// The status of the order
        /// </summary>
        public DateTime PaymentDate { get; set; }
        /// <summary>
        /// The date of the payment
        /// </summary>
        public DateTime ShipDate { get; set; }
        /// <summary>
        /// The date of the shipment
        /// </summary>
        public DateTime DeliveryDate { get; set; }
        /// <summary>
        /// The date when the order was delivered
        /// </summary>
        public List<BO.OrderItem> Items { get; set; }
        /// <summary>
        /// The list of all the items from the order
        /// </summary>
        public double TotalPrice { get; set; }
        /// <summary>
        /// The total price of the order
        /// </summary>

        public override string ToString()
        {
            return $@"
             ID: {ID}
             CustomerName: {CustomerName}
             CustomerEmail: {CustomerEmail}
             CustomerAddress: {CustomerAddress}
             OrderDate: {OrderDate}
             Status: {Status}
             PaymentDate: {PaymentDate}
             ShipDate: {ShipDate}
             DeliveryDate: {DeliveryDate}
             Items: {string.Join(", ", Items)}
             TotalPrice: {TotalPrice}";
        }
    }
}
