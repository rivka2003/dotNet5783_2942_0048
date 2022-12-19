using DalApi;
using DO;

namespace Dal;

//short implementation with XMLTools functions.
internal class dalProduct : IProduct
    {
        string path = "products.xml";

        public int Add(Product Or)
        {
            List<Product> prodLst = XmlTools.LoadListFromXMLSerializer<Product>(path);

            if (prodLst.Exists(x => x.ID == Or.ID))
                throw new DalAlreadyExistsException("Product");

            prodLst.Add(Or);

            XmlTools.SaveListToXMLSerializer(prodLst, path);

            return Or.ID;
        }

        public void Delete(int id)
        {
        List<Product> prodLst = XmlTools.LoadListFromXMLSerializer<Product>(path);

        if (prodLst.Exists(x => x.ID == id))
            throw new DalAlreadyExistsException("Product");
        prodLst.Remove(RequestByPredicate(product => product?.ID == id));

        XmlTools.SaveListToXMLSerializer(prodLst, path);

        
        }

    //public Product GetByCondition(Func<Product?, bool>? cond)
    //{
    //    throw new NotImplementedException();
    //}
    public IEnumerable<Product?> RequestByPredicate(Func<Product?, bool>? predicate = null)///list i enumerable.
    {
        IEnumerable<Product?> prodLst = XmlTools.LoadListFromXMLSerializer<Product>(path);

        bool checkNull = predicate is null;
        return prodLst.Where(product => checkNull ? true : predicate!(product));
    }

    public IEnumerable<Product?> RequestAll(Func<Product?, bool>? cond = null)
        {
        List<DO.Product?> prodList = XmlTools.LoadListFromXMLSerializer<DO.Product?>(path);

        if (cond == null)
            return prodList.AsEnumerable().OrderByDescending(p=>p?.ID);

        return prodList.Where(cond).OrderByDescending(p => p?.ID);

             
        }


        public Product RequestById(int id)
        {
            throw new NotImplementedException();
        }



        public void Update(Product Or)
    {
        List<Product> prodLst = XmlTools.LoadListFromXMLSerializer<Product>(path);
        if (!prodLst.Exists(i => i?.ID == Or.ID))
            throw new NonFoundObjectDo();
        for (int i = 0; i < prodLst.Count; i++)
        {
            if (Or.ID == prodLst[i]?.ID)
                prodLst[i] = Or;
        }
        XmlTools.SaveListToXMLSerializer(prodLst, path);

    }
}

