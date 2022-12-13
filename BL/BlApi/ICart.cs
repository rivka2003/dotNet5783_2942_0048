
namespace BlApi
{
    public interface ICart
    {
        /// <summary>
        /// Adding an item to the cart
        /// </summary>
        /// <param name="Item"></param>
        /// <returns></returns>
        public BO.Cart AddProductToCart(BO.Cart cart, int productID);
        /// <summary>
        /// Updetes the amount of the item in the cart
        /// </summary>
        /// <param name="Item"></param>
        /// <param name="ID"></param>
        /// <param name="Amount"></param>
        /// <returns></returns>
        public BO.Cart UpdateAmountProduct(BO.Cart cartt, int ID, int Amount);
        /// <summary>
        /// Making a new order by the details that it got
        /// </summary>
        /// <param name="Item"></param>
        /// <param name="Name"></param>
        /// <param name="Email"></param>
        /// <param name="Address"></param>
        public void OrderMaking(BO.Cart cart);
    }
}
