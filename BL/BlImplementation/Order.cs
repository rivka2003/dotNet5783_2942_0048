using BlApi;
using BO;
using CopyPropertisTo;

namespace BlImplementation
{
    internal class Order : BlApi.IOrder
    {
        public DalApi.IDal Dal = new Dal.DalList();
        public IBl Ibl = new Bl();
        public IEnumerable<BO.OrderForList> GetAll()
        {
            var orders = Dal.Order.GetAll();
            return orders.Select(order =>
            {
                var data = getData(order);
                BO.OrderForList orderForList = new BO.OrderForList();
                order.CopyPropTo(orderForList);
                orderForList.Status = getOrderStatus(order);   
                (orderForList.AmountOfItems, orderForList.TotalPrice) = (data.Item1.Count(), data.Item2);
    
                return orderForList;
            });
        }

        private (IEnumerable<DO.OrderItem>, double) getData(DO.Order order)
        {
            IEnumerable<DO.OrderItem> orderItems = Dal.OrderItem.RequestAllByPredicate
              (orderItem => orderItem.OrderID == order.ID);
            return (orderItems, orderItems.Sum(o => o.Price * o.Amount));
        }
        private OrderStatus getOrderStatus(DO.Order order)
        {
            return order switch
            {
                DO.Order _order when _order.DeliveryDate != DateTime.MinValue => OrderStatus.Delivered,
                DO.Order _order when _order.ShipDate != DateTime.MinValue => OrderStatus.Shipped,
                _ => OrderStatus.Confirmed,
            };
        }

        /// <summary>
        /// trying.........
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        /// <exception cref="NotImplementedException"></exception>
        public BO.Order OrderDetails(int ID)
        {
            BO.Order OrderBo = new BO.Order();
            DO.Order OrderDo;
            if (ID > 0)
            {
                try
                {
                    OrderDo = Dal.Order.Get(ID);
                }
                catch (DO.NonFoundObjectDo ex)
                { throw new BO.NonFoundObjectBo("", ex); }

                var data = getData(OrderDo);
                OrderDo.CopyPropTo(OrderBo);

                OrderBo.PaymentDate = OrderBo.OrderDate.AddSeconds(new Random().Next(-30, 0));

                OrderBo.Items = data.Item1.Select(orderItem =>
                {
                    BO.OrderItem orderItemBo = new OrderItem();
                    orderItem.CopyPropTo(orderItemBo);
                    orderItemBo.Name = Dal.Product.Get(orderItem.ProductID).Name;
                    orderItemBo.TotalPrice = orderItem.Price * orderItem.Amount;
                    return orderItemBo;
                }).ToList();

                OrderBo.Status = getOrderStatus(OrderDo);
                OrderBo.TotalPrice = OrderBo.Items.Sum(o => o.Amount * o.Price);
            }
            else
                throw new BO.NotValid();

            return OrderBo;
        }

        public BO.Order UpdeteShipDate(int ID)
        {
            BO.Order OrderBo;
            DO.Order OrderDo;

            try
            {
                OrderDo = Dal.Order.Get(ID);
                OrderBo = Ibl.Order.OrderDetails(ID);
            }
            catch (DO.NonFoundObjectDo ex)
            { throw new BO.NonFoundObjectBo("", ex); }

            if(OrderDo.ShipDate == DateTime.MinValue)
            {
                OrderDo.ShipDate = DateTime.Now;
                OrderBo.ShipDate = DateTime.Now;
                try
                { Dal.Order.Update(OrderDo); }
                catch(DO.NonFoundObjectDo ex)
                { throw new BO.NonFoundObjectBo("", ex); }
            }
            else
                throw new BO.AlreadyUpdated(); 

            return OrderBo;
        }

        public BO.Order UpdateDeliveryDate(int ID)
        {
            BO.Order OrderBo;
            DO.Order OrderDo;

            try
            {
                OrderDo = Dal.Order.Get(ID);
                OrderBo = Ibl.Order.OrderDetails(ID);
            }
            catch (DO.NonFoundObjectDo ex)
            { throw new BO.NonFoundObjectBo("", ex); }

            if (OrderDo.ShipDate != DateTime.MinValue)
            {
                if (OrderDo.DeliveryDate == DateTime.MinValue)
                {
                    OrderDo.DeliveryDate = DateTime.Now;
                    OrderBo.DeliveryDate = DateTime.Now;
                    try
                    { Dal.Order.Update(OrderDo); }
                    catch (DO.NonFoundObjectDo ex) 
                    { throw new BO.NonFoundObjectBo("", ex); }
                }
                else
                    throw new BO.AlreadyUpdated();
            }
            else 
                throw new BO.NotValid();

            return OrderBo;
        }

        public BO.OrderTracking TrackingOrder(int ID)
        {
            DO.Order OrderDo;
            BO.Order OrderBo;

            try
            {
                OrderDo = Dal.Order.Get(ID);
                OrderBo = Ibl.Order.OrderDetails(ID);
            }
            catch (DO.NonFoundObjectDo ex)
            { throw new BO.NonFoundObjectBo("", ex); }

            BO.OrderTracking orderTracking = new BO.OrderTracking()
            {
                ID = ID,
                Status = OrderBo.Status,
                OrderProgress = new List<(DateTime, BO.OrderStatus)>   
                {
                    (OrderBo.OrderDate, BO.OrderStatus.Confirmed),
                    (OrderBo.ShipDate, BO.OrderStatus.Shipped),
                    (OrderBo.DeliveryDate, BO.OrderStatus.Delivered)
                }
            };

            return orderTracking;
        }
    }
}
