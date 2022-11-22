using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface IOrder
    {
        /// <summary>
        /// Returns a list of all the orders
        /// </summary>
        /// <param name="listOfOrders"></param>
        /// <returns></returns>
        public IEnumerable<BO.OrderForList> AllList();
        /// <summary>
        /// Request order details of the order by the given ID and returns the order object
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public BO.OrderForList OrderDetails(int ID);
        /// <summary>
        /// Updats the ship date of the order
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public BO.Order UpdeteShipDate(int ID);
        /// <summary>
        /// Updats the delivery date of the order
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public BO.Order UpdateDeliveryDate(int ID);
        /// <summary>
        /// Returning the status of the orer
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public BO.OrderTracking TrackingOrder(int ID);
    }
}
