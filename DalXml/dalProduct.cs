using DalApi;
using DO;
using System.Xml.Linq;

namespace Dal;

//short implementation with XMLTools functions.
internal class DalProduct : IProduct
{
    string path = "products.xml";

    ///Implementation of iCrod functions for each entity within Excel files

    /// <summary>
    /// Add function to add a product to the list of products (in the xml files)
    /// </summary>
    /// <param name="pro"></param>
    /// <returns></returns>
    /// <exception cref="ExistingObjectDo"></exception>
    public int Add(Product pro)
    {
        List<Product?> productLst = XmlTools.LoadListFromXMLSerializer<Product?>(path);

        if (productLst.Exists(x => x?.ID == pro.ID))
            throw new ExistingObjectDo("Error - The product is alredy exist - can't add");

        productLst.Add(pro);

        XmlTools.SaveListToXMLSerializer(productLst, path);

        return pro.ID;
    }


    /// <summary>
    /// Delete function to remove a product from the list
    /// </summary>
    /// <param name="ID"></param>
    /// <exception cref="NonFoundObjectDo"></exception>
    public void Delete(int ID)
    {
        List<Product?> productLst = XmlTools.LoadListFromXMLSerializer<Product?>(path);

        if (!productLst.Exists(x => x?.ID == ID))
            throw new NonFoundObjectDo("Error - The product does not exist - can't delete");

        productLst.Remove(RequestByPredicate(product => product?.ID == ID));

        XmlTools.SaveListToXMLSerializer(productLst, path);
    }

    /// <summary>
    /// A helper method that returns a partial list according to the requested filter
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public IEnumerable<Product?> RequestAllByPredicate(Func<Product?, bool>? predicate = null)///list i enumerable.
    {
        return XmlTools.LoadListFromXMLSerializer<DO.Product?>(path).Where(product => predicate is null || predicate(product));
    }

    /// <summary>
    /// A helper method that returns a single object according to the requested filter
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    /// <exception cref="NonFoundObjectDo"></exception>
    public Product RequestByPredicate(Func<Product?, bool>? predicate)
    {
        return RequestAllByPredicate(predicate).SingleOrDefault() ?? throw new NonFoundObjectDo("Error - The product does not exist");
    }


    /// <summary>
    /// Update function to update the product
    /// </summary>
    /// <param name="pro"></param>
    /// <exception cref="NonFoundObjectDo"></exception>
    public void Update(Product pro)
    {
        List<Product?> productList = XmlTools.LoadListFromXMLSerializer<Product?>(path);

        if (!productList.Exists(i => i?.ID == pro.ID))
            throw new NonFoundObjectDo("Error - The product does not exist - can't update");

        /// checking what is the index in the list that the ID is the same as the pro ID
        var index = productList.FindIndex(item => item?.ID == pro.ID);

        productList[index] = pro;

        XmlTools.SaveListToXMLSerializer(productList, path);
    }
}

