using DalApi;
using DO;
using System.Xml.Linq;

namespace Dal;

//short implementation with XMLTools functions.
internal class dalProduct : IProduct
{
    string path = "products.xml";
    XElement productsRoot;

    
    public dalProduct()
    {
        LoadData();
    }


    /// <summary>
    /// function load data to the root variable from the file, if file doesn't exist creats it and loading.
    /// </summary>
    /// <exception cref="Exception"></exception>
    private void LoadData()
    {
        try
        {
            if (File.Exists(path))
                productsRoot = XElement.Load(path);
            else
            {
                productsRoot = new XElement("products");
                productsRoot.Save(path);
            }
        }
        catch (Exception ex)
        {
            throw new Exception("products File upload problem" + ex.Message);
        }
    }


    /// <summary>
    /// Implementation of iCrod functions for each entity within Excel files
    /// </summary>
    /// <param name="pro"></param>
    /// <returns></returns>
    /// <exception cref="ExistingObjectDo"></exception>
    public int Add(Product pro)
    {
        List<Product?> productLst = XmlTools.LoadListFromXMLSerializer<Product?>(path);

        if (productLst.Exists(x => x?.ID == pro.ID))
            throw new ExistingObjectDo();

        productLst.Add(pro);

        XmlTools.SaveListToXMLSerializer(productLst, path);

        return pro.ID;
    }


    /// <summary>
    /// Implementation of iCrod functions for each entity within Excel files
    /// </summary>
    /// <param name="ID"></param>
    /// <exception cref="NonFoundObjectDo"></exception>
    public void Delete(int ID)
    {
        List<Product?> productLst = XmlTools.LoadListFromXMLSerializer<Product?>(path);

        if (!productLst.Exists(x => x?.ID == ID))
            throw new NonFoundObjectDo();

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
        List<Product?> productList = XmlTools.LoadListFromXMLSerializer<DO.Product?>(path);

        bool checkNull = predicate is null;
        return productList.Where(product => checkNull ? true : predicate!(product));
    }

    /// <summary>
    /// A helper method that returns a single object according to the requested filter
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    /// <exception cref="NonFoundObjectDo"></exception>
    public Product RequestByPredicate(Func<Product?, bool>? predicate)
    {
        List<Product?> productList = XmlTools.LoadListFromXMLSerializer<DO.Product?>(path);
        if (productList.FirstOrDefault(predicate!) is Product product)
        {
            return product;
        }
        throw new NonFoundObjectDo();
    }


    /// <summary>
    /// Implementation of iCrod functions for each entity within Excel files
    /// </summary>
    /// <param name="pro"></param>
    /// <exception cref="NonFoundObjectDo"></exception>
    public void Update(Product pro)
    {
        List<Product?> productList = XmlTools.LoadListFromXMLSerializer<Product?>(path);

        if (!productList.Exists(i => i?.ID == pro.ID))
            throw new NonFoundObjectDo();

        for (int i = 0; i < productList.Count; i++)
        {
            if (pro.ID == productList[i]?.ID)
                productList[i] = pro;
        }

        XmlTools.SaveListToXMLSerializer(productList, path);
    }
}

