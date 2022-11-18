using DO;
using DalApi;
using static Dal.DataSource;

namespace Dal;

internal class DalOrderItem : IOrderItem
{
    //CRUD for OrderItem

    /// <summary>
    /// A function to add orderItem, and return the ID
    /// </summary>
    /// <param name="orIt"></param>
    public int Add(OrderItem orIt)
    {
        orIt.ID = Config.getOrderItemSequenceID();
        DataSource.OrderItems.Add(orIt);
        return orIt.ID;
    }
    /// <summary>
    /// A function that returns all the orderItems
    /// </summary>
    /// <returns></returns>
    public IEnumerable<OrderItem> Get()
    {
        List<OrderItem> NewOrderItems = new List<OrderItem>();
        for (int i = 0; i < DataSource.OrderItems.Count; i++)
        {
            NewOrderItems.Add(DataSource.OrderItems[i]);
        }
        return NewOrderItems;
    }

    public IEnumerable<OrderItem> RequestAllByOrderID(int orID)
    {
        if (!DataSource.OrderItems.Exists(i => i.OrderID == orID))
            throw new Exception("The orderItem does not exist");
        List<OrderItem> byOrderID = new List<OrderItem>();
        for (int i = 0; i < DataSource.OrderItems.Count; i++)
        {
            if (DataSource.OrderItems[i].OrderID == orID)
            {
                byOrderID.Add(DataSource.OrderItems[i]);
            }
        }
        return byOrderID;
    }
    /// <summary>
    /// A function that returns a spacific orderItem by the specific ID
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public OrderItem RequestById(int ID)
    {
        if (!DataSource.OrderItems.Exists(i => i.ID == ID))
            throw new Exception("The orderItem does not exist");
        return DataSource.OrderItems.Find(i => i.ID == ID);
    }
    /// <summary>
    /// The function returns the value by the order ID and the product ID
    /// </summary>
    /// <param name="or"></param>
    /// <param name="pro"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public OrderItem RequestByOrderAndProductID(int orID, int proID)
    {
        if (!DataSource.OrderItems.Exists(i => i.OrderID == orID && i.ProductID == proID))
            throw new Exception("The orderItem does not exist");
        return DataSource.OrderItems.Find(i => i.OrderID == orID && i.ProductID == proID);
    }
    /// <summary>
    /// A function that updats the orderItem by the new parameter that we received
    /// </summary>
    /// <param name="orIt"></param>
    /// <exception cref="Exception"></exception>
    public void Update(OrderItem orIt)
    {
        ///if productItem dosnt exist throw exception 
        if (!DataSource.OrderItems.Exists(i => i.ID == orIt.ID))
            throw new Exception("Cannot update an order, does not exist");
        for (int i = 0; i < DataSource.OrderItems.Count; i++)
        {
            if (orIt.ID == DataSource.OrderItems[i].ID)
                DataSource.OrderItems[i] = orIt;
        }
    }
    /// <summary>
    /// A function to delete the orderItem that we have received his ID
    /// </summary>
    /// <param name="orIt"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int id)
    {
        DataSource.OrderItems.Remove(RequestById(id));
    }
}
