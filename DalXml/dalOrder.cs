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
            throw new Exception("product File upload problem" + ex.Message);
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

        XElement Id = new XElement("Id", Or.ID);
        XElement CustomerName = new XElement("CustomerName", Or.CustomerName);
        XElement CustomerEmail = new XElement("CustomerEmail", Or.CustomerEmail);
        XElement CustomerAdress = new XElement("CustomerAdress", Or.CustomerAddress);
        XElement OrderDate = new XElement("OrderDate", Or.OrderDate);
        XElement ShipDate = new XElement("ShipDate", Or.ShipDate);
        XElement DeliveryDate = new XElement("DeliveryDate", Or.DeliveryDate);

        ordersRoot.Add(new XElement("Order", Id, CustomerName, CustomerEmail, CustomerAdress, OrderDate, ShipDate, DeliveryDate));
        ordersRoot.Save(path);

        return Or.ID;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Order RequestByPredicate(Func<Order?, bool>? cond)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Order?> RequestAllByPredicate(Func<Order?, bool>? cond = null)
    {
        throw new NotImplementedException();
    }

    public void Update(Order Or)
    {
        throw new NotImplementedException();
    }
}

