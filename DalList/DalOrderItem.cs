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
    /// The function returns the value by the order ID and the product ID
    /// </summary>
    /// <param name="or"></param>
    /// <param name="pro"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public OrderItem RequestByOrderAndProductID(int orID, int proID)
    {
        if (!OrderItems.Exists(i => i!.Value.OrderID == orID && i.Value.ProductID == proID))
            throw new NonFoundObjectDo();
        return OrderItems.Find(i => i?.OrderID == orID && i?.ProductID == proID)!.Value;
    }
    /// <summary>
    /// A function that updats the orderItem by the new parameter that we received
    /// </summary>
    /// <param name="orIt"></param>
    /// <exception cref="Exception"></exception>
    public void Update(OrderItem orIt)
    {
        ///if productItem dosnt exist throw exception 
        if (!OrderItems.Exists(i => i!.Value.ID == orIt.ID))
            throw new NonFoundObjectDo();
        for (int i = 0; i < OrderItems.Count; i++)
        {
            if (orIt.ID == OrderItems[i]!.Value.ID)
                OrderItems[i] = orIt;
        }
    }
    /// <summary>
    /// A function to delete the orderItem that we have received his ID
    /// </summary>
    /// <param name="orIt"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int id)
    {
        OrderItems.Remove(RequestByPredicate(orderItem => orderItem!.Value.ID == id));
    }
    /// <summary>
    /// function that gets predicate and checks the condition and returns the collection acordingly
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public IEnumerable<OrderItem?> RequestAllByPredicate(Func<OrderItem?, bool>? predicate = null!)
    {
        bool checkNull = predicate is null;
        return OrderItems.Where(orderItem => checkNull? true: predicate!(orderItem)); 
    }

    public OrderItem RequestByPredicate(Func<OrderItem?, bool>? predicate)
    {
        if (OrderItems.FirstOrDefault(predicate!) is OrderItem orderItem)
        {
            return orderItem;
        }
        throw new NonFoundObjectDo();
    }
}
