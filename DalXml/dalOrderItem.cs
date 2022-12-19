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

    ///Implementation of iCrod functions for each entity within Excel files

    /// <summary>
    /// Add function to add an orderItem to the list of orderItems (in the xml files)
    /// </summary>
    /// <param name="orI"></param>
    /// <returns></returns>
    public int Add(OrderItem orI)
    {
        List<OrderItem?> orderItemList = XmlTools.LoadListFromXMLSerializer<OrderItem?>(path);

        ///Read config file
        XElement configRoot = XElement.Load(configPath);

        int nextSeqNum = Convert.ToInt32(configRoot.Element("orderItemSequenceID")!.Value);
        orI.ID = nextSeqNum;
        nextSeqNum++;

        //update config file
        configRoot.Element("orderItemSequenceID")!.SetValue(nextSeqNum);
        configRoot.Save(configPath);

        ///add the product to the root and save the path
        orderItemList.Add(orI);

        XmlTools.SaveListToXMLSerializer(orderItemList, path);

        return orI.ID;
    }


    /// <summary>
    /// Delete function to remove an orderItem from the list
    /// </summary>
    /// <param name="ID"></param>
    /// <exception cref="NonFoundObjectDo"></exception>
    public void Delete(int ID)
    {
        List<OrderItem?> orderItemList = XmlTools.LoadListFromXMLSerializer<OrderItem?>(path);

        if (!orderItemList.Exists(x => x?.ID == ID))
            throw new NonFoundObjectDo("OrderItem");

        orderItemList.Remove(RequestByPredicate(x => x?.ID == ID));

        XmlTools.SaveListToXMLSerializer(orderItemList, path);
    }


    /// <summary>
    /// A helper method that returns a single orderItem according to the requested filter
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    /// <exception cref="NonFoundObjectDo"></exception>
    public OrderItem RequestByPredicate(Func<OrderItem?, bool>? predicate)
    {
        return RequestAllByPredicate(predicate).SingleOrDefault() ?? throw new NonFoundObjectDo("OrderItem");
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
    /// Update function to update the orderItem
    /// </summary>
    /// <param name="orI"></param>
    /// <exception cref="NonFoundObjectDo"></exception>
    public void Update(OrderItem orI)
    {
        List<OrderItem?> orderItemList = XmlTools.LoadListFromXMLSerializer<OrderItem?>(path);

        if (!orderItemList.Exists(x => x?.ID == orI.ID))
            throw new NonFoundObjectDo("OrerItem");

        ///A loop that runs on the list and gets the object and updates it
        for (int i = 0; i < orderItemList.Count(); i++)
        {
            if (orderItemList[i]?.ID == orI.ID)
                orderItemList[i] = orI;
        }

        XmlTools.SaveListToXMLSerializer(orderItemList, path);
    }
}

