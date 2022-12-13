
namespace BlApi
{
    public interface IOrder
    {
        /// <summary>
        /// Returns a list of all the orders
        /// </summary>
        /// <param name="listOfOrders"></param>
        /// <returns></returns>
        public IEnumerable<BO.OrderForList?> GetAll();
        /// <summary>
        /// Request order details of the order by the given ID and returns the order object
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public BO.Order OrderDetails(int ID);
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
        /// Returning the Status of the orer
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public BO.OrderTracking TrackingOrder(int ID);
    }
}
