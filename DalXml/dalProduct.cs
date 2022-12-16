using DalApi;
using DO;

namespace Dal;

//short implementation with XMLTools functions
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
            throw new NotImplementedException();
        }

        public Product RequestByPredicate(Func<Product?, bool>? cond)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product?> RequestAllByPredicate(Func<Product?, bool>? cond = null)
        {
        List<DO.Product?> prodList = XmlTools.LoadListFromXMLSerializer<DO.Product?>(path);

        if (cond == null)
            return prodList.AsEnumerable().OrderByDescending(p=>p?.ID);

        return prodList.Where(cond).OrderByDescending(p => p?.ID);
             
        }

        public void Update(Product Or)
        {
            throw new NotImplementedException();
        }
    }

