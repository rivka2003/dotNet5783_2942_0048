using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using Dal;
using DalApi;

namespace BlImplementation
{
    internal class Order : BlApi.IOrder
    {
        public IDal Dal = new DalList();
        public IEnumerable<BO.OrderForList> AllList()
        {
            var listOfOrders = Dal.Order.Get();
            BO.OrderForList orForLst = new BO.OrderForList();
            IEnumerable<BO.OrderForList> newListOfOrders = listOfOrders.Select(item => (OrderForList)item = );
        }

        public BO.OrderForList OrderDetails(int ID)
        {
            return null;
        }

        public BO.OrderTracking TrackingOrder(int ID)
        {
            return null;
        }

        public BO.Order UpdateDeliveryDate(int ID)
        {
            return null;
        }

        public BO.Order UpdeteShipDate(int ID)
        {
            return null;
        }

        IEnumerable<OrderForList> BlApi.IOrder.AllList(List<DO.Order> listOfOrders)
        {
            throw new NotImplementedException();
        }
    }
}
