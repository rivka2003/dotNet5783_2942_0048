using DO;
using DalApi;
using static Dal.DataSource;
using System.Runtime.CompilerServices;

namespace Dal;

internal class DalOrder : IOrder
{
    //CRUD for Order

    /// <summary>
    /// A function to add order, and return the ID
    /// </summary>
    /// <param name="or"></param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(Order or)
    {
        or.ID = Config.GetOrderSequenceID();
        Orders.Add(or);
        return or.ID;
    }
    /// <summary>
    /// A function that updats the order by the new parameter that we received
    /// </summary>
    /// <param name="or"></param>
    /// <exception cref="Exception"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(Order or)
    {
        ///if Order dosnt exist throw exception 
        if (!Orders.Exists(i => i?.ID == or.ID))
            throw new NonFoundObjectDo("Error - The order does not exist - can't update");
        for (int i = 0; i < Orders.Count; i++)
        {
            if (or.ID == Orders[i]?.ID)
                Orders[i] = or;
        }
    }
    /// <summary>
    /// A function to delete the order that we have received his ID
    /// </summary>
    /// <param name="or"></param>
    /// <exception cref="Exception"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        Orders.Remove(RequestByPredicate(order => order?.ID == id)); 
    }
    /// <summary>
    /// function that gets predicate and checks the condition and returns the collection acordingly
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<Order?> RequestAllByPredicate(Func<Order?, bool>? predicate = null!)
    {
        return Orders.Where(order => predicate is null ? true : predicate!(order));
    }
    /// <summary>
    /// A helper method that returns a single order according to the requested filter
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    /// <exception cref="NonFoundObjectDo"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public Order RequestByPredicate(Func<Order?, bool>? predicate)
    {
        return RequestAllByPredicate(predicate).SingleOrDefault() ?? throw new NonFoundObjectDo("Error - The order does not exist");
    }
}
