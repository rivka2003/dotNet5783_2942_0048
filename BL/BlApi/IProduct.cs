using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface IProduct
    {
        /// <summary>
        /// Returns all the list of products       
        /// </summary>
        /// <param name="listOfProducts"></param>
        /// <returns></returns>
        public IEnumerable<BO.ProductForList> AllList();
        /// <summary>
        /// Return the product details by the ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public BO.Product ProductDetailsForManager(int ID);
        /// <summary>
        /// The function gets a product from the cart and presenting the details of the product
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        public BO.ProductItem ProductDetailsForCustomer(int ID, BO.Cart product);
        /// <summary>
        /// A function that adds the product to the data layer
        /// </summary>
        /// <param name="product"></param>
        public void AddProduct(BO.Product product);
        /// <summary>
        /// Deleting a product by the ID that resived
        /// </summary>
        /// <param name="ID"></param>
        public void DeleteProduct(int ID);
        /// <summary>
        /// The function updates the product to the product that she have resived
        /// </summary>
        /// <param name="UpdeteProduct"></param>
        public void UpdateProduct(BO.Product UpdeteProduct);
    }
}
