﻿using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class OrderTracking
    {
        /// <summary>
        /// The ID of the order
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// The status of the order
        /// </summary>
        public BO.OrderStatus Status { get; set; }
        /// <summary>
        /// List of tuple
        /// </summary>
        public List<Tuple<DateTime, BO.OrderStatus>>  OrderProgress { get; set; }
        public override string ToString() => $@"
        ID: {ID}
        Status: {Status}
        OrderProgress: {}"; // לטפל בהדפסה
    }
}