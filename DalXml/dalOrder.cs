using DalApi;
using DO;
using System.Xml.Linq;
namespace Dal;

internal class dalOrder : IOrder
{
    string path = "orders.xml";
    string configPath = "config.xml";
    XElement ordersRoot;

    public dalOrder()
    {
        LoadData();
    }

    private void LoadData()
    {
        try
        {
            if (File.Exists(path))
                ordersRoot = XElement.Load(path);
            else
            {
                ordersRoot = new XElement("orders");
                ordersRoot.Save(path);
            }
        }
        catch (Exception ex)
        {
            throw new Exception("orders File upload problem" + ex.Message);
        }
    }

    public int Add(Order Or)
    {
        //Read config file
        XElement configRoot = XElement.Load(configPath);

        int nextSeqNum = Convert.ToInt32(configRoot.Element("orderSequenceID")!.Value);
        Or.ID = nextSeqNum;
        nextSeqNum++;
        //update config file
        configRoot.Element("orderSequenceID")!.SetValue(nextSeqNum);
        configRoot.Save(configPath);

        XElement Id = new XElement("ID", Or.ID);
        XElement CustomerName = new XElement("CustomerName", Or.CustomerName);
        XElement CustomerEmail = new XElement("CustomerEmail", Or.CustomerEmail);
        XElement CustomerAddress = new XElement("CustomerAddress", Or.CustomerAddress);
        XElement OrderDate = new XElement("OrderDate", Or.OrderDate);
        XElement ShipDate = new XElement("ShipDate", Or.ShipDate);
        XElement DeliveryDate = new XElement("DeliveryDate", Or.DeliveryDate);

        ordersRoot.Add(new XElement("Order", Id, CustomerName, CustomerEmail, CustomerAddress, OrderDate, ShipDate, DeliveryDate));
        ordersRoot.Save(path);

        return Or.ID;
    }

    public void Delete(int ID)
    {
        getOr(ID).Remove();

        ordersRoot.Save(path);
    }

    public Order RequestByPredicate(Func<Order?, bool>? predicate)
    {
        return RequestAllByPredicate(predicate).SingleOrDefault() ?? throw new NonFoundObjectDo();
    }

    public IEnumerable<Order?> RequestAllByPredicate(Func<Order?, bool>? predicate = null)
    {
        IEnumerable<Order?> orderList = (IEnumerable<Order?>)(from element in ordersRoot.Elements()
                                                              select new Order
                                                              {
                                                                  ID = int.Parse(element.Element("ID")!.Value),
                                                                  CustomerName = element.Element("CustomerName")!.Value,
                                                                  CustomerEmail = element.Element("CustomerEmail")!.Value,
                                                                  CustomerAddress = element.Element("CustomerAddress")!.Value,
                                                                  OrderDate = DateTime.Parse(element.Element("OrderDate")!.Value),
                                                                  ShipDate = DateTime.Parse(element.Element("ShipDate")!.Value),
                                                                  DeliveryDate = DateTime.Parse(element.Element("DeliveryDate")!.Value)
                                                              });

        bool checkNull = predicate is null;
        return orderList.Where(order => checkNull? true : predicate!(order));
    }

    public void Update(Order order)
    {
        XElement orderElement = getOr(order.ID);

        orderElement.Element("CustomerName")!.Value = order.CustomerName!.ToString();
        orderElement.Element("CustomerEmail")!.Value = order.CustomerEmail!.ToString();
        orderElement.Element("CustomerAddress")!.Value = order.CustomerAddress!.ToString();
        orderElement.Element("OrderDate")!.Value = order.OrderDate.ToString()!;
        orderElement.Element("ShipDate")!.Value = order.ShipDate.ToString()!;
        orderElement.Element("DeliveryDate")!.Value = order.DeliveryDate.ToString()!;

        ordersRoot.Save(path);
    }

    public XElement getOr(int id)
    {
        return (from or in ordersRoot.Elements() 
         where or.Element("ID")!.Value == id.ToString()
         select or).FirstOrDefault()!;
    }
}

