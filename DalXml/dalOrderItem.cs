using System.Xml.Linq;
namespace Dal;
using DalApi;
using DO;
using System;

internal class dalOrderItem : IOrderItem
{
    string path = "orderItems.xml";
    string configPath = "config.xml";
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
                ordersItemsRoot = new XElement("orderItems");
                ordersItemsRoot.Save(path);
            }
        }
        catch (Exception ex)
        {
            throw new Exception("orderItems File upload problem" + ex.Message);
        }
    }

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

    public void Delete(int ID)
    {
        List<OrderItem?> orderItemList = XmlTools.LoadListFromXMLSerializer<OrderItem?>(path);

        if (!orderItemList.Exists(x => x?.ID == ID))
            throw new NonFoundObjectDo();

        orderItemList.Remove(RequestByPredicate(x => x?.ID == ID));

        XmlTools.SaveListToXMLSerializer(orderItemList, path);
    }

    public OrderItem RequestByPredicate(Func<OrderItem?, bool>? predicate)
    {
        List<OrderItem?> orderItemList = XmlTools.LoadListFromXMLSerializer<DO.OrderItem?>(path);

        if (orderItemList.FirstOrDefault(predicate!) is OrderItem orderItem)
        {
            return orderItem;
        }
        throw new NonFoundObjectDo();
    }

    public IEnumerable<OrderItem?> RequestAllByPredicate(Func<OrderItem?, bool>? predicate = null)
    {
        List<DO.OrderItem?> orderItemList = XmlTools.LoadListFromXMLSerializer<DO.OrderItem?>(path);

        bool checkNull = predicate is null;
        return orderItemList.Where(product => checkNull ? true : predicate!(product));

    }

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

