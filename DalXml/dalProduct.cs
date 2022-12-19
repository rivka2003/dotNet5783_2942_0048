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

    public int Add(Product pro)
    {
        List<Product?> productLst = XmlTools.LoadListFromXMLSerializer<Product?>(path);

        if (productLst.Exists(x => x?.ID == pro.ID))
            throw new ExistingObjectDo();

        productLst.Add(pro);

        XmlTools.SaveListToXMLSerializer(productLst, path);

        return pro.ID;
    }

    public void Delete(int ID)
    {
        List<Product?> productLst = XmlTools.LoadListFromXMLSerializer<Product?>(path);

        if (!productLst.Exists(x => x?.ID == ID))
            throw new NonFoundObjectDo();

        productLst.Remove(RequestByPredicate(product => product?.ID == ID));

        XmlTools.SaveListToXMLSerializer(productLst, path);
    }

    public IEnumerable<Product?> RequestAllByPredicate(Func<Product?, bool>? predicate = null)///list i enumerable.
    {
        List<Product?> productList = XmlTools.LoadListFromXMLSerializer<DO.Product?>(path);

        bool checkNull = predicate is null;
        return productList.Where(product => checkNull ? true : predicate!(product));
    }

    public Product RequestByPredicate(Func<Product?, bool>? predicate)
    {
        List<Product?> productList = XmlTools.LoadListFromXMLSerializer<DO.Product?>(path);
        if (productList.FirstOrDefault(predicate!) is Product product)
        {
            return product;
        }
        throw new NonFoundObjectDo();
    }

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

