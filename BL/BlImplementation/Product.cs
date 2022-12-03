using CopyPropertisTo;

namespace BlImplementation
{
    internal class Product : BlApi.IProduct
    {
        public DalApi.IDal Dal = new Dal.DalList();
        /// <summary>
        /// returning all the products as a collection
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BO.ProductForList> GetAll()
        {
            return Dal.Product.GetAll().CopyPropToList<DO.Product, BO.ProductForList>();
        }
        /// <summary>
        /// returning the details of an order needed for a manager view
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        /// <exception cref="BO.NonFoundObjectBo"></exception>
        /// <exception cref="BO.NotValid"></exception>
        public BO.Product ProductDetailsForManager(int ID)
        {
            BO.Product productBo = new BO.Product();
            DO.Product productDo;
            try
            { productDo = Dal.Product.Get(ID);
            }
            catch(DO.NonFoundObjectDo ex) 
            { throw new BO.NonFoundObjectBo("", ex); }

            if (ID > 0)
            {
                productDo.CopyPropTo(productBo);
            }
            else
                throw new BO.NotValid();
            return productBo;
        }
        /// <summary>
        /// returning the details of an order needed for a customer view
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="cart"></param>
        /// <returns></returns>
        /// <exception cref="BO.NonFoundObjectBo"></exception>
        /// <exception cref="BO.NotValid"></exception>
        public BO.ProductItem ProductDetailsForCustomer(int ID, BO.Cart cart)
        {
            BO.ProductItem proItm = new BO.ProductItem();
            DO.Product proDo;

            try
            { proDo = Dal.Product.Get(ID); }
            catch (DO.NonFoundObjectDo ex)
            { throw new BO.NonFoundObjectBo("", ex); }

            if (ID > 0)
            {
                proDo.CopyPropTo(proItm);
                BO.OrderItem orderItem = cart.Items.FirstOrDefault(i => i.ID == ID)!;

                if (orderItem is not null)
                {
                    proItm.Amount = orderItem.Amount;
                }

                if (proDo.InStock != 0)
                {
                    proItm.InStock = true;
                }
            }
            else
                throw new BO.NotValid();
            return proItm;
        }
        /// <summary>
        /// adding a new gived product to the products list
        /// </summary>
        /// <param name="productBo"></param>
        /// <exception cref="BO.ExistingObjectBo"></exception>
        /// <exception cref="BO.NotValid"></exception>
        public void AddProduct(BO.Product productBo)
        {
            DO.Product productDo = new DO.Product();
            if (productBo.ID > 0 && productBo.Name != " " && productBo.Price > 0 && productBo.InStock >= 0)
            {
                productDo = productBo.CopyPropToStruct(productDo);
                productDo.Status = DO.Status.Exist;

                try
                { Dal.Product.Add(productDo); }
                catch (DO.ExistingObjectDo ex)
                { throw new BO.ExistingObjectBo("", ex); }
            }
            else
                throw new BO.NotValid();
        }
        /// <summary>
        /// deleting a product that its id was given, from the products list
        /// </summary>
        /// <param name="ID"></param>
        /// <exception cref="BO.NonFoundObjectBo"></exception>
        /// <exception cref="BO.ExistingObjectBo"></exception>
        public void DeleteProduct(int ID) 
        {
           if (!Dal.OrderItem.RequestAllByPredicate(orderItem => orderItem.ProductID == ID).Any())
           {
                try
                {
                    Dal.Product.Delete(ID);
                }
                catch(DO.NonFoundObjectDo ex)
                { throw new BO.NonFoundObjectBo("", ex); }
           }
           else
              throw new BO.ExistingObjectBo();
        }
        /// <summary>
        /// updating a product according to the new product with same id that was recieved
        /// </summary>
        /// <param name="updateProduct"></param>
        /// <returns></returns>
        /// <exception cref="BO.NonFoundObjectBo"></exception>
        /// <exception cref="BO.NotValid"></exception>
        public BO.Product UpdateProduct(BO.Product updateProduct)
        {
            DO.Product productDo;
            try
            {
               productDo =  Dal.Product.Get(updateProduct.ID);
            }
            catch (DO.NonFoundObjectDo ex)
            { throw new BO.NonFoundObjectBo("", ex); }

            if (updateProduct.ID > 0 && updateProduct.Name != " " && updateProduct.Price > 0 && updateProduct.InStock >= 0)
            {
                productDo = updateProduct.CopyPropToStruct(productDo);
                productDo.Status = DO.Status.Exist;

                try
                { Dal.Product.Update(productDo); }
                catch (DO.NonFoundObjectDo ex)
                { throw new BO.NonFoundObjectBo("", ex); }
            }
            else
                throw new BO.NotValid();
            return updateProduct;
        }
    }
}
