using CopyPropertisTo;
using Dal;

namespace BlImplementation
{
    internal class Product : BlApi.IProduct
    {
        public DalApi.IDal Dal = new Dal.DalList();
        public IEnumerable<BO.ProductForList> GetAll()
        {
            return Dal.Product.GetAll().CopyPropToList<DO.Product, BO.ProductForList> ();
        }

        public BO.Product ProductDetailsForManager(int ID)
        {
            BO.Product productBo = new BO.Product();
            DO.Product productDo = new DO.Product();
            try
            { productDo = Dal.Product.Get(ID); }
            catch(DO.NonFoundObjectDo) 
            { throw new BO.NonFoundObjectBo(); }
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
            { proDo = Dal.Product.Get(ID); }
            catch (DO.NonFoundObjectDo)
            { throw new BO.NonFoundObjectBo(); }
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

        public void AddProduct(BO.Product productBo)
        {
            DO.Product productDo = new DO.Product();
            try
            {
                productDo = Dal.Product.Get(productBo.ID);
            }
            catch (DO.NonFoundObjectDo)
            { throw new BO.NonFoundObjectBo(); }
            if (productBo.ID > 0 && productBo.Name != " " && productBo.Price > 0 && productBo.InStock >= 0)
            {
                productBo.CopyPropToStruct(productDo);
                productBo.CopyPropTo(productDo);
                productDo.Status = DO.Status.Exist;
                try
                { Dal.Product.Add(productDo); }
                catch (DO.ExistingObjectDo)
                { throw new BO.ExistingObjectBo(); }
            }
            else
                throw new BO.NotValid();
        }

        public void DeleteProduct(int ID) 
        {
           if (!Dal.OrderItem.RequestAllByPredicate(orderItem => orderItem.ProductID == ID).Any())
           {
                try
                {
                    Dal.Product.Delete(ID);
                }
                catch(DO.NonFoundObjectDo)
                { throw new BO.NonFoundObjectBo(); }
           }
           else
           {
                throw new BO.ExistingObjectBo();
           }
        }

        public BO.Product UpdateProduct(BO.Product updateProduct)
        {
            DO.Product productDo = new DO.Product();
            try
            {
               productDo =  Dal.Product.Get(updateProduct.ID);
            }
            catch (DO.NonFoundObjectDo)
            { throw new BO.NonFoundObjectBo(); }
            if (updateProduct.ID > 0 && updateProduct.Name != " " && updateProduct.Price > 0 && updateProduct.InStock >= 0)
            {
                updateProduct.CopyPropToStruct(productDo);
                updateProduct.CopyPropTo(productDo);
                productDo.Status = DO.Status.Exist;
                try
                { Dal.Product.Update(productDo); }
                catch (DO.NonFoundObjectDo)
                { throw new BO.NonFoundObjectBo(); }
            }
            else
            {
                throw new BO.NotValid();
            }
            return updateProduct;
        }
    }
}
