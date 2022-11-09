using Dal;
using DO;
using static Dal.DataSource;

namespace Dal;

public class DalOrderItem
{
    //CRUD for OrderItem

    /// <summary>
    /// A function to add orderItem
    /// </summary>
    /// <param name="orIt"></param>
    public void Create(OrderItem orIt)
    {
        orIt.ID = Config.OrderItemSequenceID;
        DataSource.OrderItems.Add(orIt);
    }
    /// <summary>
    /// A function that returns all the orderItems
    /// </summary>
    /// <returns></returns>
    public List<OrderItem> RequestAll()
    {
        return DataSource.OrderItems;
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
            throw new Exception("the student is not exist");

        return DataSource.OrderItems.Find(i => i.ID == ID);
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
            throw new Exception("cannot update a student, is not exists");
        OrderItem orItToRemove = DataSource.OrderItems.Find(i => i.ID == orIt.ID);
        DataSource.OrderItems.Remove(orItToRemove);
        DataSource.OrderItems.Add(orIt);
    }
    /// <summary>
    /// A function to delete the orderItem that we have received
    /// </summary>
    /// <param name="orIt"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(OrderItem orIt)
    {
        ///if orderItem dosnt exist throw exception 
        if (!DataSource.OrderItems.Exists(i => i.ID == orIt.ID))
            throw new Exception("cannot delete a student, is not exists");
        DataSource.OrderItems.Remove(orIt); //or set Active..
    }
}
