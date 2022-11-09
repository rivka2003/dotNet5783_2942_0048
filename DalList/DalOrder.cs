using Dal;
using DO;
using System.Runtime.CompilerServices;
using static Dal.DataSource;

namespace Dal;

public class DalOrder
{
    //CRUD for Order

    /// <summary>
    /// A function to add order
    /// </summary>
    /// <param name="or"></param>
    public void Create(Order or)
    {
        or.ID = Config.OrderSequenceID;
        DataSource.Orders.Add(or);
    }
    /// <summary>
    /// A function that returns all the orders
    /// </summary>
    /// <returns></returns>
    public List<Order> RequestAll()
    {
        return DataSource.Orders;
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
            throw new Exception("the student is not exist");

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
            throw new Exception("cannot update a student, is not exists");
        Order orToRemove = DataSource.Orders.Find(i => i.ID == or.ID);
        or.ID = orToRemove.ID;
        DataSource.Orders.Remove(orToRemove);
        DataSource.Orders.Add(or);
    }
    /// <summary>
    /// A function to delete the order that we have received
    /// </summary>
    /// <param name="or"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(Order or)
    {
        ///if Order dosnt exist throw exception 
        if (!DataSource.Orders.Exists(i => i.ID == or.ID))
            throw new Exception("cannot delete a student, is not exists");
        DataSource.Orders.Remove(or); //or set Active..
    }
}
