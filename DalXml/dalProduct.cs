using Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal;

//short implementation with XMLTools functions
internal class dalProduct : IProduct
    {
        string path = "products.xml";

        public int Create(Product Or)
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

        public Product GetByCondition(Func<Product?, bool>? cond)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product?> RequestAll(Func<Product?, bool>? cond = null)
        {
        List<DO.Product?> prodList = XmlTools.LoadListFromXMLSerializer<DO.Product?>(path);

        if (cond == null)
            return prodList.AsEnumerable().OrderByDescending(p=>p?.Id);

        return prodList.Where(cond).OrderByDescending(p => p?.Id);
             
        }

        public Product RequestById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Product Or)
        {
            throw new NotImplementedException();
        }
    }

