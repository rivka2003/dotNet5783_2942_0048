using BO;
using CopyPropertisTo;

namespace BlImplementation
{
    internal class Product : BlApi.IProduct
    {
        public DalApi.IDal? Dal = DalApi.Factory.Get();

        /// <summary>
        /// returning all the products as a collection
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProductForList?> GetAll()
        {
            return Dal!.Product.RequestAllByPredicate().CopyPropToList<DO.Product?, ProductForList>();
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
            /// if the ID is not valid..
            if (ID > 100000000)
                throw new BO.NotValid("Error - ID can't be more then 8 digits!");
            if (ID < 100000)
                throw new BO.NotValid("Error - ID can't be less then 6 digits!");

            BO.Product productBo = new();
            DO.Product productDo;

            try /// trying to get the product from the Dal
            {
                productDo = Dal!.Product.RequestByPredicate(product => product?.ID == ID);
            }
            catch (DO.NonFoundObjectDo ex)
            { throw new BO.NonFoundObjectBo(ex.Message, ex); }

            productDo.CopyPropTo(productBo); ///copyng product from DO to BO

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
            /// if the ID is not valid..
            if (ID > 100000000)
                throw new BO.NotValid("Error - ID can't be more then 8 digits!");
            if (ID < 100000)
                throw new BO.NotValid("Error - ID can't be less then 6 digits!");

            BO.ProductItem proItm = new();
            DO.Product proDo;

            try /// tryng to get the product from the Dal
            { proDo = Dal!.Product.RequestByPredicate(product => product?.ID == ID); }
            catch (DO.NonFoundObjectDo ex)
            { throw new BO.NonFoundObjectBo(ex.Message, ex); }

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
            /// checking if all the product details are valid
            if (productBo.Description == " ")
                throw new BO.NotValid("Error - Description box can't be empty!");
            if (productBo.ID > 100000000)
                throw new BO.NotValid("Error - ID can't be more then 8 digits!");
            if (productBo.ID < 100000)
                throw new BO.NotValid("Error - ID can't be less then 6 digits!");
            if (productBo.Name == " ")
                throw new BO.NotValid("Error - Name box can't be empty!");
            if (productBo.Price <= 0)
                throw new BO.NotValid("Error - Price can't be a negative number!");
            if (productBo.InStock < 0)
                throw new BO.NotValid("Error - Amount in stock can't be a negative number!");
            if (productBo.Image == " ")
                throw new BO.NotValid("Error - Imege box can't be empty!");

            DO.Product productDo = new();

            productDo = productBo.CopyPropToStruct(productDo);
            productDo.Status = DO.Status.Exist;

            try /// tryng to add the product in to the list in the DO
            { Dal!.Product.Add(productDo); }
            catch (DO.ExistingObjectDo ex)
            { throw new BO.ExistingObjectBo(ex.Message, ex); }
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
            if (Dal!.OrderItem.RequestAllByPredicate(orderItem => orderItem?.ProductID == ID).Any())
            {
                try /// trying to delete the product from the DO
                {
                    Dal.Product.Delete(ID);
                }
                catch (DO.NonFoundObjectDo ex)
                { throw new BO.NonFoundObjectBo(ex.Message, ex); }
            }
            else /// if there is any order with this product
                throw new BO.InExistingOrder("The product exists in another order - can't delete");
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
            /// checking if all the product details are valid
            if (updateProduct.Description == " ")
                throw new BO.NotValid("Error - Description box can't be empty!");
            if (updateProduct.ID > 100000000)
                throw new BO.NotValid("Error - ID can't be more then 8 digits!");
            if (updateProduct.ID < 100000)
                throw new BO.NotValid("Error - ID can't be less then 6 digits!");
            if (updateProduct.Name == " ")
                throw new BO.NotValid("Error - Name box can't be empty!");
            if (updateProduct.Price <= 0)
                throw new BO.NotValid("Error - Price can't be a negative number!");
            if (updateProduct.InStock < 0)
                throw new BO.NotValid("Error - Amount in stock can't be a negative number!");
            if (updateProduct.Image == " ")
                throw new BO.NotValid("Error - Imege box can't be empty!");

            DO.Product productDo;
            try /// trying to get the product from the Dal
            {
                productDo = Dal!.Product.RequestByPredicate(product => product?.ID == updateProduct.ID);
            }
            catch (DO.NonFoundObjectDo ex)
            { throw new BO.NonFoundObjectBo(ex.Message, ex); }

            /// checking if all the fields in the product are valid
            if (updateProduct.Description != " " && updateProduct.ID >= 100000 && updateProduct.Name != " " && updateProduct.Price > 0 && updateProduct.InStock >= 0)
            {
                productDo = updateProduct.CopyPropToStruct(productDo);
                productDo.Status = DO.Status.Exist;

                try /// trying to update the product in the DO
                { Dal.Product.Update(productDo); }
                catch (DO.NonFoundObjectDo ex)
                { throw new BO.NonFoundObjectBo(ex.Message, ex); }
            }

            return updateProduct;
        }

        public IEnumerable<ProductItem?> GrupingByChoos(BO.Cart cart)
        {
          
            IEnumerable<ProductItem?> productItems = from DO.Product item in Dal!.Product.RequestAllByPredicate()
                                                     select new ProductItem
                                                     {
                                                         ID = item.ID,
                                                         Name = item.Name,
                                                         Price = item.Price,
                                                         InStock = item.InStock > 0 ? InStock.Yes : InStock.No,
                                                         Amount = cart?.Items!.FirstOrDefault(x => x?.ID == item.ID)?.Amount ?? 0,
                                                         Category = (BO.Category?)item.Category,
                                                         Gender = (BO.Gender?)item.Gender,
                                                         Clothing = (BO.Clothing?)item.Clothing,
                                                         Shoes = (BO.Shoes?)item.Shoes ?? null,
                                                         Color = (BO.Color?)item.Color,
                                                         SizeClothing = (BO.SizeClothing?)item.SizeClothing ?? null,
                                                         SizeShoes = (BO.SizeShoes?)item.SizeShoes ?? null,
                                                         Description = item.Description,
                                                         Image = item.Image
                                                     };


            //IEnumerable<IGrouping<Gender, ProductItem?>> result = from g in productItems
            //                                                         group g by BO.Gender.Women into genderGroup
            //                                                         select genderGroup;
            return productItems;
        }
    }
}
