using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal;
using DalApi;
using DO;

internal class dalOrderItem : IOrderItem
    {
        string path = XmlTools.dir + "ordersItems.xml";
        XElement ordersItemsRoot;

        public dalOrderItem()
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                if (File.Exists(path))
                    ordersItemsRoot = XElement.Load(path);
                else
                {
                    ordersItemsRoot = new XElement("ordersItems");
                    ordersItemsRoot.Save(path);
                    }
            }
            catch (Exception ex)
            {
                throw new Exception("product File upload problem" + ex.Message);
            }
        }

        public int Create(OrderItem Or)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public OrderItem GetByCondition(Func<OrderItem?, bool>? cond)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderItem?> RequestAll(Func<OrderItem?, bool>? cond = null)
        {
            throw new NotImplementedException();
        }

        public OrderItem RequestById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(OrderItem Or)
        {
            throw new NotImplementedException();
        }
    }

