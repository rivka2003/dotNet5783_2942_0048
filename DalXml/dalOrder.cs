using DalApi;
using DO;
using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;
namespace Dal;


//short implementation with XMLTools functions.
internal class DalOrder : IOrder
{
    static readonly string path = @"..\xml\orders.xml";

    static readonly string configPath = @"..\xml\config.xml";
    XElement? ordersRoot;

    public DalOrder()
    {
        LoadData();
    }

    /// <summary>
    /// function load data to the root variable from the file, if file doesn't exist creats it and loading.
    /// </summary>
    /// <exception cref="Exception"></exception>
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

    ///Implementation of iCrod functions for each entity within Excel files

    /// <summary>
    /// Add function to add an order to the list of orders (in the xml files)
    /// </summary>
    /// <param name="Or"></param>
    /// <returns></returns>
    public int Add(Order Or)
    {
        ///Read config file
        XElement configRoot = XElement.Load(configPath);

        int nextSeqNum = Convert.ToInt32(configRoot.Element("orderSequenceID")!.Value);
        Or.ID = nextSeqNum;
        nextSeqNum++;

        ///update config file
        configRoot.Element("orderSequenceID")!.SetValue(nextSeqNum);
        configRoot.Save(configPath);

        XElement Id = new("ID", Or.ID);
        XElement CustomerName = new("CustomerName", Or.CustomerName);
        XElement CustomerEmail = new("CustomerEmail", Or.CustomerEmail);
        XElement CustomerAddress = new("CustomerAddress", Or.CustomerAddress);
        XElement OrderDate = new("OrderDate", Or.OrderDate);
        XElement ShipDate = new("ShipDate", Or.ShipDate);
        XElement DeliveryDate = new("DeliveryDate", Or.DeliveryDate);

        ///add the product to the root and save the path
        ordersRoot!.Add(new XElement("Order", Id, CustomerName, CustomerEmail, CustomerAddress, OrderDate, ShipDate, DeliveryDate));
        ordersRoot.Save(path);

        return Or.ID;
    }


    /// <summary>
    /// Delete function to remove an order from the list
    /// </summary>
    /// <param name="ID"></param>
    public void Delete(int ID)
    {
        GetOrL(ID).Remove();

        ordersRoot!.Save(path);
    }


    /// <summary>
    /// A helper method that returns a single order according to the requested filter
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    /// <exception cref="NonFoundObjectDo"></exception>
    public Order RequestByPredicate(Func<Order?, bool>? predicate)
    {
        return RequestAllByPredicate(predicate).SingleOrDefault() ?? throw new NonFoundObjectDo("Error - The order does not exist");
    }


    /// <summary>
    /// A helper method that returns a partial list according to the requested filter
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public IEnumerable<Order?> RequestAllByPredicate(Func<Order?, bool>? predicate = null)
    {
        /// using linq to initialize the order
    
        return (from element in ordersRoot!.Elements() 
                select (Order?)new Order
                {
                    ID = int.Parse(element.Element("ID")!.Value),
                    CustomerName = element.Element("CustomerName")!.Value,
                    CustomerEmail = element.Element("CustomerEmail")!.Value,
                    CustomerAddress = element.Element("CustomerAddress")!.Value,
                    OrderDate = DateTime.TryParse(element.Element("OrderDate")!.Value, out DateTime OrderDate) ? OrderDate : null,
                    ShipDate = DateTime.TryParse(element.Element("ShipDate")!.Value, out DateTime ShipDate) ? ShipDate : null,
                    DeliveryDate = DateTime.TryParse(element.Element("DeliveryDate")!.Value, out DateTime DeliveryDate) ? DeliveryDate : null,
                }).Where(order => predicate is null || predicate!(order));
    }


    /// <summary>
    /// Update function to update the order
    /// </summary>
    /// <param name="order"></param>
    public void Update(Order order)
    {
        XElement orderElement = GetOrL(order.ID);

        orderElement.Element("CustomerName")!.Value = order.CustomerName!.ToString();
        orderElement.Element("CustomerEmail")!.Value = order.CustomerEmail!.ToString();
        orderElement.Element("CustomerAddress")!.Value = order.CustomerAddress!.ToString();
        orderElement.Element("OrderDate")!.Value = order.OrderDate.ToString()!;
        orderElement.Element("ShipDate")!.Value = order.ShipDate.ToString()!;
        orderElement.Element("DeliveryDate")!.Value = order.DeliveryDate.ToString()!;

        ordersRoot!.Save(path);
    }

    /// <summary>
    /// returns an order from the xml file according to the recieved id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public XElement GetOrL(int id)
    {
        return (from or in ordersRoot!.Elements()
                where or.Element("ID")!.Value == id.ToString()
                select or).FirstOrDefault() ?? throw new NonFoundObjectDo("Error - The order does not exist");
    }
}

