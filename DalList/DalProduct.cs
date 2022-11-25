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
        if (DataSource.Products.Exists(i => i.ID == pro.ID))
            throw new Exception("Cannot creat a product, is already exist");
        DataSource.Products.Add(pro);
        return pro.ID;
    }
    /// <summary>
    /// A function that returns all the products
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Product> GetAll()
    {
        List<Product> NewProducts = new List<Product>();
        for (int i = 0; i < DataSource.Products.Count; i++)
        {
            NewProducts.Add(DataSource.Products[i]);
        }
        return NewProducts;
    }
    /// <summary>
    /// A function that returns a spacific product by the ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public Product Get(int id)
    {
        if (!DataSource.Products.Exists(i => i.ID == id))
            throw new Exception("The product does not exist");
        return DataSource.Products.Find(i => i.ID == id);
    }
    /// <summary>
    /// A function that updats the product by the new parameter that we received
    /// </summary>
    /// <param name="pro"></param>
    /// <exception cref="Exception"></exception>
    public void Update(Product pro)
    {
        ///if product dosnt exist throw exception 
        if (!DataSource.Products.Exists(i => i.ID == pro.ID))
            throw new Exception("Cannot update a product, does not exist");
        for (int i = 0; i < DataSource.Products.Count; i++)
        {
            if (pro.ID == DataSource.Products[i].ID)
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
        DataSource.Products.Remove(Get(id));
    }
}
