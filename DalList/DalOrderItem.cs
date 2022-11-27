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
        orIt.ID = getOrderItemSequenceID();
        DataSource.OrderItems.Add(orIt);
        return orIt.ID;
    }
    /// <summary>
    /// A function that returns all the orderItems
    /// </summary>
    /// <returns></returns>
    public IEnumerable<OrderItem> GetAll()
    {
        return RequestAllByPredicate();
    }

    /// <summary>
    /// A function that returns a spacific orderItem by the specific ID
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public OrderItem Get(int ID)
    {
        if (!DataSource.OrderItems.Exists(i => i.ID == ID))
            throw new NonFoundObjectDo();
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
            throw new NonFoundObjectDo();
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
            throw new NonFoundObjectDo();
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
        DataSource.OrderItems.Remove(Get(id));
    }
    /// <summary>
    /// function that gets predicate and checks the condition and returns the collection acordingly
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public IEnumerable<OrderItem> RequestAllByPredicate(Predicate<OrderItem> predicate = null)
    {
        bool checkNull = predicate is null;
        return DataSource.OrderItems.Where(orderItem => checkNull? true: predicate(orderItem)); // לבדוק את התנאי הפנימי
    }
}
