using Dal;
using DO;
using System.Runtime.CompilerServices;
using static Dal.DataSource;

namespace Dal;

public class DalOrder
{
    //CRUD for Order

    /// <summary>
    /// A function to add order, and return the ID
    /// </summary>
    /// <param name="or"></param>
    public int Create(Order or)
    {
        or.ID = Config.getOrderSequenceID();
        DataSource.Orders.Add(or);
        return or.ID;
    }
    /// <summary>
    /// A function that returns all the orders
    /// </summary>
    /// <returns></returns>
    public List<Order> RequestAll()
    {
        List<Order> NewOrders = new List<Order>();
        for (int i = 0; i < DataSource.Orders.Count; i++)
        {
            NewOrders.Add(DataSource.Orders[i]);

        }
        return NewOrders;
    }
    /// <summary>
    /// A function that returns a spacific order by the specific ID
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public Order RequestById(int ID)
    {
        if (!DataSource.Orders.Exists(i => i.ID == ID))
            throw new Exception("the order does not exist");
        return DataSource.Orders.Find(i => i.ID == ID);
    }
    /// <summary>
    /// A function that updats the order by the new parameter that we received
    /// </summary>
    /// <param name="or"></param>
    /// <exception cref="Exception"></exception>
    public void Update(Order or)
    {
        ///if Order dosnt exist throw exception 
        if (!DataSource.Orders.Exists(i => i.ID == or.ID))
            throw new Exception("cannot update an order, does not exists");
        for (int i = 0; i < DataSource.Orders.Count; i++)
        {
            if (or.ID == DataSource.Orders[i].ID)
                DataSource.Orders[i] = or;
        }
    }
    /// <summary>
    /// A function to delete the order that we have received his ID
    /// </summary>
    /// <param name="or"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int id)
    {
        DataSource.Orders.Remove(RequestById(id)); 
    }
}
