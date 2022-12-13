﻿using CopyPropertisTo;

namespace BO
{
    public class OrderTracking
    {
        /// <summary>
        /// The ID of the order
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// The Status of the order
        /// </summary>
        public BO.OrderStatus? Status { get; set; }
        /// <summary>
        /// List of tuple
        /// </summary>
        public List<(DateTime?, BO.OrderStatus?)>?  OrderProgress { get; set; }
        public override string ToString() => this.ToStringProperty();
    }
}
