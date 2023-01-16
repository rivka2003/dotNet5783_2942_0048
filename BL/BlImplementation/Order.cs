using BO;
using CopyPropertisTo;

namespace BlImplementation
{
    internal class Order : BlApi.IOrder
    {
        public DalApi.IDal? Dal = DalApi.Factory.Get();
        /// <summary>
        /// Returns the entier list of products
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BO.OrderForList?> GetAll()
        {
            /// geting the entier list from the Dal
            var orders = Dal!.Order.RequestAllByPredicate();
            /// for every order (by the id) filling the data of the order
            return orders.Select(order =>
            {
                var data = getData((DO.Order)order!);
                BO.OrderForList orderForList = new BO.OrderForList();
                order.CopyPropTo(orderForList);
                orderForList.Status = getOrderStatus((DO.Order)order!);
                (orderForList.AmountOfItems, orderForList.TotalPrice) = (data.Item1.Count(), data.Item2);

                return orderForList;
            });
        }
        /// <summary>
        /// function that returns a touple of the list of all the order items that are in the same order (by the id)
        /// and return the sum of all the products prices that are in the cart
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private (IEnumerable<DO.OrderItem?>, double) getData(DO.Order order)
        {
            ///returns ienumerable of all the order items that are in the same order id
            IEnumerable<DO.OrderItem?> orderItems = Dal!.OrderItem.RequestAllByPredicate
              (orderItem => orderItem?.OrderID == order.ID);
            return (orderItems, (double)orderItems.Sum(o => o?.Price * o?.Amount)!);
        }
        /// <summary>
        /// a function that checks the order status
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private OrderStatus getOrderStatus(DO.Order order)
        {
            ///checks the case
            return order switch
            {
                DO.Order _order when _order.DeliveryDate is not null => OrderStatus.Delivered,
                DO.Order _order when _order.ShipDate is not null => OrderStatus.Shipped,
                _ => OrderStatus.Confirmed,
            };
        }
        /// <summary>
        /// a function that returns the order details by the id
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        /// <exception cref="BO.NonFoundObjectBo"></exception>
        /// <exception cref="BO.NotValid"></exception>
        public BO.Order OrderDetails(int ID)
        {
            BO.Order OrderBo = new BO.Order();
            DO.Order OrderDo;
            /// checks if the input is valid
            if (ID > 0)
            {
                ///tryng to get the order from the DO
                try
                {
                    OrderDo = Dal!.Order.RequestByPredicate(order => order?.ID == ID);
                }
                catch (DO.NonFoundObjectDo ex)
                { throw new BO.NonFoundObjectBo(ex.Message, ex); }

                OrderDo.CopyPropTo(OrderBo);/// copying the order details from the DO ti the order details from the BO

                OrderBo.PaymentDate = OrderBo.OrderDate?.AddSeconds(new Random().Next(-30, 0));

                ///for every order item that is in the order copy all the details from DO to BO and make them a list
                OrderBo.Items = Dal.OrderItem.RequestAllByPredicate(orderItem => orderItem?.OrderID == ID).Select(orderItem =>
                {
                    BO.OrderItem? orderItemBo = new OrderItem();
                    orderItem.CopyPropTo(orderItemBo);
                    orderItemBo.Name = Dal.Product.RequestByPredicate(product => product?.ID == orderItem?.ProductID).Name;
                    orderItemBo.TotalPrice = (double)(orderItem?.Price * orderItem?.Amount)!;
                    return orderItemBo;
                }).ToList()!;

                OrderBo.Status = getOrderStatus(OrderDo);
                OrderBo.TotalPrice = OrderBo.Items.Sum(o => o!.Amount * o.Price);
            }
            else/// if the ID is not valid..
                throw new BO.NotValid("Error - ID can't be a negative number!");

            return OrderBo;
        }
        /// <summary>
        /// a function that updates the shipping date
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        /// <exception cref="BO.NonFoundObjectBo"></exception>
        /// <exception cref="BO.AlreadyUpdated"></exception>
        public BO.Order UpdateShipDate(int ID)
        {
            DO.Order OrderDo;

            try /// trying to get the order from Dal and the order details from the Ibl
            {
                OrderDo = Dal!.Order.RequestByPredicate(order => order?.ID == ID);

                /// checking that the Ship date didnt updated yet
                if (OrderDo.ShipDate is not null)
                    throw new BO.AlreadyUpdated("Error - The order has been shiped!");

                if (OrderDo.DeliveryDate is not null)
                    throw new BO.AlreadyUpdated("Error - The order has been delivered!");

                OrderDo.ShipDate = DateTime.Now;
                Dal.Order.Update(OrderDo);
            }
            catch (DO.NonFoundObjectDo ex)
            { throw new BO.NonFoundObjectBo(ex.Message, ex); }

            return OrderDetails(ID);
        }
        /// <summary>
        /// a function that updates the delivery date
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        /// <exception cref="BO.NonFoundObjectBo"></exception>
        /// <exception cref="BO.AlreadyUpdated"></exception>
        /// <exception cref="BO.NotValid"></exception>
        public BO.Order UpdateDeliveryDate(int ID)
        {
            DO.Order OrderDo;

            try /// trying to get the order from the DO and the order datails from the BO
            {
                OrderDo = Dal!.Order.RequestByPredicate(order => order?.ID == ID);

                /// checking that the dates that neede to be updated have updated
                if (OrderDo.ShipDate is null)
                    throw new BO.NotValid("Error - The order was not shiped!");

                /// also checking that the delivery date didnt change yet
                if (OrderDo.DeliveryDate is not null)
                    throw new BO.AlreadyUpdated("Error - The order has been delivered!");/// if it is already updated

                OrderDo.DeliveryDate = DateTime.Now;
                Dal.Order.Update(OrderDo);
            }
            catch (DO.NonFoundObjectDo ex)
            { throw new BO.NonFoundObjectBo(ex.Message, ex); }

            return OrderDetails(ID);
        }
        /// <summary>
        /// a function that returns the order tracking
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        /// <exception cref="BO.NonFoundObjectBo"></exception>
        public BO.OrderTracking TrackingOrder(int ID)
        {
            BO.Order OrderBo;

            try /// trying to get the order from the DO and the order datails from the BO 
            {
                OrderBo = OrderDetails(ID);
            }
            catch (DO.NonFoundObjectDo ex)
            { throw new BO.NonFoundObjectBo(ex.Message, ex); }

            BO.OrderTracking orderTracking = new BO.OrderTracking()
            {
                ID = ID,
                Status = OrderBo.Status,
                OrderProgress = new List<Tuple<DateTime?, BO.OrderStatus?>> /// initialize the touple of dete time and order status
                {
                    new Tuple<DateTime?, OrderStatus?>(OrderBo.OrderDate, BO.OrderStatus.Confirmed),
                    new Tuple<DateTime?, OrderStatus?>(OrderBo.ShipDate, BO.OrderStatus.Shipped),
                    new Tuple<DateTime?, OrderStatus?>(OrderBo.DeliveryDate, BO.OrderStatus.Delivered)
                }
            };

            return orderTracking;
        }
        /// <summary>
        /// A function that grups all the orders by the statistics
        /// </summary>
        /// <returns></returns>
        public IEnumerable<StatisticksOrderByMonth> GetStatisticksOrderByMonths()
        {
            return from order in Dal!.Order.RequestAllByPredicate()
                   let _order = order.GetValueOrDefault()
                   let orderDate = _order.OrderDate.GetValueOrDefault() 
                   group order by orderDate.Month.ToString("MMMM") into newGroup
                   select new StatisticksOrderByMonth
                   {
                       MonthName = newGroup.Key,
                       CountOrders = newGroup.Count(),
                       OrdersTotalPrice = (from order in newGroup
                                          let totalPriceOfOrder = Dal.OrderItem?.RequestAllByPredicate(orderItem => orderItem?.ID == order?.ID)
                                          .Sum(orderItem => orderItem?.Price)
                                          select totalPriceOfOrder).Sum()
                   };
        }
    }

    /// <summary>
    /// A struct to present the orders by month, amount of orders and the total price for a month
    /// </summary>
    public struct StatisticksOrderByMonth
    {
        public string MonthName { get; set; }

        public int CountOrders { get; set; }

        public double? OrdersTotalPrice { get; set; }
    }
}
