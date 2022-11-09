using Dal;
using DO;
namespace Dal;

public class DalProduct
{
    //CRUD for Product

    /// <summary>
    /// A function to add product
    /// </summary>
    /// <param name="pro"></param>
    public void Create(Product pro)
    {
        //if product exist throw exception 
        if (DataSource.Products.Exists(i => i.ID == pro.ID))
            throw new Exception("cannot create a student, is already exists");
        DataSource.Products.Add(pro);
    }
    /// <summary>
    /// A function that returns all the products
    /// </summary>
    /// <returns></returns>
    public List<Product> RequestAll()
    {
        return DataSource.Products;
    }
    /// <summary>
    /// A function that returns a spacific product by the ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public Product RequestById(int id)
    {
        if (!DataSource.Products.Exists(i => i.ID == id))
            throw new Exception("the student is not exist");

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
            throw new Exception("cannot update a student, is not exists");
        Product sToRemove = DataSource.Products.Find(i => i.ID == pro.ID);
        DataSource.Products.Remove(sToRemove);
        DataSource.Products.Add(pro);

    }
    /// <summary>
    /// A function to delete the product that we have received
    /// </summary>
    /// <param name="pro"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(Product pro)
    {
        ///if product dosnt exist throw exception 
        if (!DataSource.Products.Exists(i => i.ID == pro.ID))
            throw new Exception("cannot delete a student, is not exists");
        DataSource.Products.Remove(pro); //or set Active..
    }
}
