﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using CopyPropertisTo;
using Dal;
using DalApi;
using DocumentFormat.OpenXml.ExtendedProperties;

namespace BlImplementation
{
    internal class Product : BlApi.IProduct
    {
        public IDal Dal = new DalList();
        public IEnumerable<BO.ProductForList> AllList(List<DO.Product> listOfProducts)
        {
            List<BO.ProductForList> newListOfProducts = new List<BO.ProductForList>();
            for(int i = 0; i < listOfProducts.Count();i++)
            {
                listOfProducts[i].CopyPropTo(newListOfProducts[i]);
            }
            return newListOfProducts;
        }

        public BO.Product ProductDetailsForManager(int ID)
        {
            BO.Product productBo = new BO.Product();
            DO.Product productDo = new DO.Product();
            try
            { productDo = Dal.Product.RequestById(ID); }
            catch(DO.NonFoundObject) 
            { throw new Exception("not found"); }
            if(ID > 0)
            {
                productDo.CopyPropTo(productBo);
            }
            return productBo;
        }

        public BO.ProductItem ProductDetailsForCustomer(int ID, BO.Cart product)
        {
            BO.ProductItem proItm = new BO.ProductItem();
            DO.Product proDo = new DO.Product();
            try
            { proDo = Dal.Product.RequestById(ID); }
            catch (DO.NonFoundObject)
            { throw new Exception("not found"); }
            if (ID > 0)
            {
                proDo.CopyPropTo(proItm);
                BO.OrderItem orderItem = product.Items.First(i => i.ID == ID);
                if (orderItem is not null)
                {
                    proItm.Amount = orderItem.Amount;
                }
                if (proDo.InStock != 0)
                {
                    proItm.InStock = true;
                }
            }
            return proItm;
        }

        public void AddProduct(BO.Product product)
        {
            DO.Product productDo = new DO.Product();
            if (product.ID > 0 && product.Name != " " && product.Price > 0 && product.InStock >= 0)
            {
                product.CopyPropTo(productDo);
                try
                { Dal.Product.Add(productDo); }
                catch (DO.ExistingObject)
                { throw new Exception("Already Exist"); }
            }
            else
                throw new Exception("not valid");
        }

        public void DeleteProduct(int ID)
        {
           if(!Dal.OrderItem.Get().Any())
           {
                try
                {
                    Dal.Product.Delete(ID);
                }
                catch(DO.NonFoundObject)
                { throw new Exception("not exist"); }
           }
           else
           {
                throw new Exception("is exist");
           }
        }

        public void UpdateProduct(BO.Product updateProduct)
        {
            DO.Product productDo = new DO.Product();
            if (updateProduct.ID > 0 && updateProduct.Name != " " && updateProduct.Price > 0 && updateProduct.InStock >= 0)
            {
                updateProduct.CopyPropTo(productDo);
                try
                { Dal.Product.Update(productDo); }
                catch (DO.NonFoundObject)
                { throw new Exception("Not Exist"); }

            }
            else
            {
                throw new Exception("not valid");
            }
        }
    }
}
