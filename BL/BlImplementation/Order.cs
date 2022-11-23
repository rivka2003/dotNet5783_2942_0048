using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using CopyPropertisTo;
using Dal;
using DalApi;
using DO;

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
                      OrderDo = Dal.Order.RequestById(ID); 
                }
                catch (DO.NonFoundObject)
                { throw new Exception("not found"); }
            
            {
                OrderDo.CopyPropTo(OrderBo);
            }
            
            return OrderBo;
        }
             
        public BO.OrderTracking TrackingOrder(int ID)
        {
                BO.Order OrderBo = new BO.Order();
                DO.Order OrderDo = new DO.Order();
                try
                    {
                        OrderDo = Dal.Order.RequestById(ID);
                    }
                catch (DO.NonFoundObject)
                    { throw new Exception("not found"); }

                if(OrderDo.ShipDate<DateTime.Now())///if it didnt pass)
                {
                    OrderDo.///updates
                        ///also update logical layer

                }

                    

                
                return OrderBo;
        }

        public BO.Order UpdateDeliveryDate(int ID)
        {

                BO.Order OrderBo = new BO.Order();
                DO.Order OrderDo = new DO.Order();
                try
                {
                    OrderDo = Dal.Order.RequestById(ID);
                }
                catch (DO.NonFoundObject)
                { throw new Exception("not found"); }
                if(OrderDo.ShipDate///passed but OrderDo.status==0)
                        {
                    OrderDo.ShipDate///update;
                    ///update in the logical layer as well
                        }
                return OrderBo;

            }

            public BO.Order UpdeteShipDate(int ID)
        {
                BO.Order OrderBo = new BO.Order();
                DO.Order OrderDo = new DO.Order();
                try
                {
                    OrderDo = Dal.Order.RequestById(ID);
                }
                catch (DO.NonFoundObject)
                { throw new Exception("not found"); }
                return OrderBo;
            }

        IEnumerable<OrderForList> BlApi.IOrder.AllList(List<DO.Order> listOfOrders)
        {
            throw new NotImplementedException();
        }
    }
}
