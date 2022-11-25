using BlApi;
using CopyPropertisTo;

namespace BlImplementation
{
    internal class Order : BlApi.IOrder
    {
        public DalApi.IDal Dal = new Dal.DalList();
        public IBl Ibl = new Bl();
        public IEnumerable<BO.OrderForList> GetAll()
        {
            return Dal.Order.GetAll().CopyPropToList<DO.Order, BO.OrderForList> ();
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
            DO.Order OrderDo = new DO.Order();
            if (ID > 0)
            {
                try
                {
                    OrderDo = Dal.Order.Get(ID);
                }
                catch (DO.NonFoundObjectDo)
                { throw new BO.NonFoundObjectBo(); }
                OrderDo.CopyPropTo(OrderBo);
            }
            return OrderBo;
        }

        public BO.Order UpdeteShipDate(int ID)
        {
            BO.Order OrderBo = new BO.Order();
            DO.Order OrderDo = new DO.Order();
            try
            {
                OrderDo = Dal.Order.Get(ID);
                OrderBo = Ibl.Order.OrderDetails(ID);
            }
            catch (DO.NonFoundObjectDo)
            { throw new BO.NonFoundObjectBo(); }

            if(OrderDo.ShipDate == DateTime.MinValue)///if it didnt pass)
            {
                OrderDo.ShipDate = OrderDo.OrderDate + TimeSpan.FromDays(4);
                OrderBo.ShipDate = OrderBo.OrderDate + TimeSpan.FromDays(4);
                try
                { Dal.Order.Update(OrderDo); }
                catch
                { throw new BO.NonFoundObjectBo(); }
            }
            else
            { throw new BO.AlreadyUpdated(); }
            return OrderBo;
        }

        public BO.Order UpdateDeliveryDate(int ID)
        {
            BO.Order OrderBo = new BO.Order();
            DO.Order OrderDo = new DO.Order();
            try
            {
                OrderDo = Dal.Order.Get(ID);
                OrderBo = Ibl.Order.OrderDetails(ID);
            }
            catch (DO.NonFoundObjectDo)
            { throw new BO.NonFoundObjectBo(); }
            if (OrderDo.DeliveryDate == DateTime.MinValue)
            {
                OrderDo.DeliveryDate = OrderDo.OrderDate + TimeSpan.FromDays(4);
                OrderBo.DeliveryDate = OrderBo.OrderDate + TimeSpan.FromDays(4);
                try
                { Dal.Order.Update(OrderDo); }
                catch (DO.NonFoundObjectDo)
                { throw new BO.NonFoundObjectBo(); }
            }
            else
                throw new BO.AlreadyUpdated();
            return OrderBo;
        }

        public BO.OrderTracking TrackingOrder(int ID)
        {
            DO.Order OrderDo = new DO.Order();
            BO.Order OrderBo = new BO.Order();
            try
            {
                OrderDo = Dal.Order.Get(ID);
                OrderBo = Ibl.Order.OrderDetails(ID);
            }
            catch (DO.NonFoundObjectDo)
            { throw new BO.NonFoundObjectBo(); }
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
