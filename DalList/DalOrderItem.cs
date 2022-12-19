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
    /// A function that updats the orderItem by the new parameter that we received
    /// </summary>
    /// <param name="orIt"></param>
    /// <exception cref="Exception"></exception>
    public void Update(OrderItem orIt)
    {
        ///if productItem dosnt exist throw exception 
        if (!OrderItems.Exists(i => i?.ID == orIt.ID))
            throw new NonFoundObjectDo("OrderItem");
        for (int i = 0; i < OrderItems.Count; i++)
        {
            if (orIt.ID == OrderItems[i]?.ID)
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
        OrderItems.Remove(RequestByPredicate(orderItem => orderItem?.ID == id));
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
        return RequestAllByPredicate(predicate).SingleOrDefault() ?? throw new NonFoundObjectDo("OrderItem");
    }
}
