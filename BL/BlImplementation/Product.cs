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
        public IEnumerable<BO.ProductForList?> GetAll()
        {
            return Dal.Product.RequestAllByPredicate().CopyPropToList<DO.Product?, BO.ProductForList>();
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
            try /// trying to get the product from the Dal
            { productDo = Dal.Product.RequestByPredicate(product => product!.Value.ID == ID);
            }
            catch(DO.NonFoundObjectDo ex) 
            { throw new BO.NonFoundObjectBo("", ex); }

            if (ID > 0) /// checking that the ID is valid
            {
                productDo.CopyPropTo(productBo); ///copyng product from DO to BO
            }
            else
                throw new BO.NotValid();
            return productBo;
        }
        /// <summary>
        /// The function gets a product from the cart and presenting the details of the product to the customer
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        public BO.ProductItem ProductDetailsForCustomer(int ID, BO.Cart cart)
        {
            BO.ProductItem proItm = new BO.ProductItem();
            DO.Product proDo;

            try /// tryng to get the product from the Dal
            { proDo = Dal.Product.RequestByPredicate(product => product!.Value.ID == ID); }
            catch (DO.NonFoundObjectDo ex)
            { throw new BO.NonFoundObjectBo("", ex); }

            if (ID > 0) /// checking that the ID is valid
            {
                proDo.CopyPropTo(proItm);
                BO.OrderItem orderItem = cart.Items!.FirstOrDefault(i => i!.ID == ID)!;
                if (orderItem is not null) ///checking if there are any items in the cart
                {
                    proItm.Amount = orderItem.Amount;
                }
                if (proDo.InStock != 0) ///checking that the stock is not empty
                {
                    proItm.InStock = BO.InStock.Yes;
                }
            }
            else /// if the ID is not valid..
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
            /// checking if all the product details are valid
            if (productBo.ID > 0 && productBo.Name != " " && productBo.Price > 0 && productBo.InStock >= 0)
            {
                productDo = productBo.CopyPropToStruct(productDo);
                productDo.Status = DO.Status.Exist;

                try /// tryng to add the product in to the list in the DO
                { Dal.Product.Add(productDo); }
                catch (DO.ExistingObjectDo ex)
                { throw new BO.ExistingObjectBo("", ex); }
            }
            else /// if one of the details is not valid
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
            /// checking by predicat if there are any orders that contains this product right now
           if (Dal.OrderItem.RequestAllByPredicate(orderItem => orderItem?.ProductID == ID).Any())
           {
                try /// trying to delete the product from the DO
                {
                    Dal.Product.Delete(ID);
                }
                catch(DO.NonFoundObjectDo ex)
                { throw new BO.NonFoundObjectBo("", ex); }
           }
           else /// if there is any order with this product
              throw new BO.InExistingOrder();
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
            try /// trying to get the product from the Dal
            {
               productDo =  Dal.Product.RequestByPredicate(product => product!.Value.ID == updateProduct.ID);
            }
            catch (DO.NonFoundObjectDo ex)
            { throw new BO.NonFoundObjectBo("", ex); }
            /// checking if all the fields in the product are valid
            if (updateProduct.ID > 0 && updateProduct.Name != " " && updateProduct.Price > 0 && updateProduct.InStock >= 0)
            {
                productDo = updateProduct.CopyPropToStruct(productDo);
                productDo.Status = DO.Status.Exist;

                try /// trying to update the product in the DO
                { Dal.Product.Update(productDo); }
                catch (DO.NonFoundObjectDo ex)
                { throw new BO.NonFoundObjectBo("", ex); }
            }
            else /// if even one of the fields is not valid
                throw new BO.NotValid();
            return updateProduct;
        }
    }
}
