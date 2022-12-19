using System.Xml.Linq;
namespace Dal;
using DalApi;
using DO;
using System;


//short implementation with XMLTools functions.
internal class dalOrderItem : IOrderItem
{
    string path = "orderItems.xml";
    string configPath = "config.xml";
    XElement ordersItemsRoot;

    public dalOrderItem()
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
                ordersItemsRoot = XElement.Load(path);
            else
            {
                ordersItemsRoot = new XElement("orderItems");
                ordersItemsRoot.Save(path);
            }
        }
        catch (Exception ex)
        {
            throw new Exception("orderItems File upload problem" + ex.Message);
        }
    }


    /// <summary>
    /// Implementation of iCrod functions for each entity within Excel files
    /// </summary>
    /// <param name="orI"></param>
    /// <returns></returns>
    public int Add(OrderItem orI)
    {
        List<OrderItem?> orderItemList = XmlTools.LoadListFromXMLSerializer<OrderItem?>(path);

        XElement configRoot = XElement.Load(configPath);

        int nextSeqNum = Convert.ToInt32(configRoot.Element("orderItemSequenceID")!.Value);
        orI.ID = nextSeqNum;
        nextSeqNum++;
        //update config file
        configRoot.Element("orderItemSequenceID")!.SetValue(nextSeqNum);
        configRoot.Save(configPath);

        orderItemList.Add(orI);

        XmlTools.SaveListToXMLSerializer(orderItemList, path);

        return orI.ID;
    }


    /// <summary>
    /// Implementation of iCrod functions for each entity within Excel files
    /// </summary>
    /// <param name="ID"></param>
    /// <exception cref="NonFoundObjectDo"></exception>
    public void Delete(int ID)
    {
        List<OrderItem?> orderItemList = XmlTools.LoadListFromXMLSerializer<OrderItem?>(path);

        if (!orderItemList.Exists(x => x?.ID == ID))
            throw new NonFoundObjectDo();

        orderItemList.Remove(RequestByPredicate(x => x?.ID == ID));

        XmlTools.SaveListToXMLSerializer(orderItemList, path);
    }


    /// <summary>
    /// A helper method that returns a single object according to the requested filter
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    /// <exception cref="NonFoundObjectDo"></exception>
    public OrderItem RequestByPredicate(Func<OrderItem?, bool>? predicate)
    {
        List<OrderItem?> orderItemList = XmlTools.LoadListFromXMLSerializer<DO.OrderItem?>(path);

        if (orderItemList.FirstOrDefault(predicate!) is OrderItem orderItem)
        {
            return orderItem;
        }
        throw new NonFoundObjectDo();
    }

    /// <summary>
    /// A helper method that returns a partial list according to the requested filter
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public IEnumerable<OrderItem?> RequestAllByPredicate(Func<OrderItem?, bool>? predicate = null)
    {
        List<DO.OrderItem?> orderItemList = XmlTools.LoadListFromXMLSerializer<DO.OrderItem?>(path);

        bool checkNull = predicate is null;
        return orderItemList.Where(product => checkNull ? true : predicate!(product));

    }


    /// <summary>
    /// Implementation of iCrod functions for each entity within Excel files
    /// </summary>
    /// <param name="orI"></param>
    /// <exception cref="NonFoundObjectDo"></exception>
    public void Update(OrderItem orI)
    {
        List<OrderItem?> orderItemList = XmlTools.LoadListFromXMLSerializer<OrderItem?>(path);

        if (!orderItemList.Exists(x => x?.ID == orI.ID))
            throw new NonFoundObjectDo();

        for(int i = 0; i < orderItemList.Count(); i++)
        {
            if (orderItemList[i]?.ID == orI.ID)
                orderItemList[i] = orI;
        }

        XmlTools.SaveListToXMLSerializer(orderItemList, path);
    }
}

