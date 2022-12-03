using DO;
using DalApi;
namespace Dal;

internal class DalProduct : IProduct
{
    //CRUD for Product

    /// <summary>
    /// A function to add product, and return the ID
    /// </summary>
    /// <param name="pro"></param>
    public int Add(Product pro)
    {
        //if product exist throw exception 
        if (DataSource.Products.Exists(i => i!.Value.ID == pro.ID))
            throw new ExistingObjectDo();
        DataSource.Products.Add(pro);
        return pro.ID;
    }
    /// <summary>
    /// A function that updats the product by the new parameter that we received
    /// </summary>
    /// <param name="pro"></param>
    /// <exception cref="Exception"></exception>
    public void Update(Product pro)
    {
        ///if product dosnt exist throw exception 
        if (!DataSource.Products.Exists(i => i!.Value.ID == pro.ID))
            throw new NonFoundObjectDo();
        for (int i = 0; i < DataSource.Products.Count; i++)
        {
            if (pro.ID == DataSource.Products[i]!.Value.ID)
                DataSource.Products[i] = pro;
        }
    }

    /// <summary>
    /// A function to delete the product that we have received his ID
    /// </summary>
    /// <param name="pro"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int id)
    {
        DataSource.Products.Remove(RequestByPredicate(product => product!.Value.ID == id));
    }
    /// <summary>
    /// function that gets predicate and checks the condition and returns the collection acordingly
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public IEnumerable<Product?> RequestAllByPredicate(Func<Product?, bool>? predicate = null)
    {
        bool checkNull = predicate is null;
        return DataSource.Products.Where(product => checkNull ? true : predicate!(product));
    }

    public Product RequestByPredicate(Func<Product?, bool>? predicate)
    {
        if (DataSource.Products.FirstOrDefault(predicate!) is Product product)
        {
            return product;
        }
        throw new NonFoundObjectDo();
    }
}
