using CopyPropertisTo;

namespace BO
{
    public class Cart
    {
        /// <summary>
        /// The name of the customer
        /// </summary>
        public string? CustomerName { get; set; }
        /// <summary>
        /// The email of the customer
        /// </summary>
        public string? CustomerEmail { get; set; }
        /// <summary>
        /// The addresss of the customer
        /// </summary>
        public string? CustomerAddress { get; set; }
        /// <summary>
        /// The list of all the items from the order
        /// </summary>
        public List<BO.OrderItem?>? Items { get; set; }
        /// <summary>
        /// The total price of the cart
        /// </summary>
        public string? Imege { get; set; }
        /// <summary>
        /// The Imege of the product
        /// </summary>
        public double TotalPrice { get; set; }
        public override string ToString() => this.ToStringProperty();
    }
}
