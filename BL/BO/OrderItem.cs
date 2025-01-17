﻿using ToolsForProject;

namespace BO
{
    public class OrderItem
    {
        /// <summary>
        /// The ID of the order item
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// The name of the product
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// The ID of the product
        /// </summary>
        public int ProductID { get; set; }
        /// <summary>
        /// The price of the product
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// The amount of products that in the cart
        /// </summary>
        public int Amount { get; set; }
        /// <summary>
        /// The total price of the product (by the price of it and the amount)
        /// </summary>
        public string? Image { get; set; }
        /// <summary>
        /// The Imege of the product
        /// </summary>
        public double TotalPrice { get; set; }
        public override string ToString() => this.ToStringProperty();
    }
}
