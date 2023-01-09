using System.ComponentModel.DataAnnotations;
using BlApi;
using CopyPropertisTo;

namespace BlImplementation
{
    internal class Cart : ICart
    {
        public DalApi.IDal? Dal = DalApi.Factory.Get();
        private readonly IProduct product = new Product();
        public Cart(IProduct Product)
        {
            product = Product;
        }
        /// <summary>
        /// adding a product with the given id to the cart
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="productID"></param>
        /// <returns></returns>
        /// <exception cref="BO.NonFoundObjectBo"></exception>
        /// <exception cref="BO.NotInStock"></exception>
        public BO.Cart AddProductToCart(BO.Cart cart, int productID, int productAmount)
        {
            DO.Product productDo;

            try///making sure the oroduct exists in the products list
            { productDo = Dal!.Product.RequestByPredicate(product => product?.ID == productID); }
            catch (DO.NonFoundObjectDo ex)
            { throw new BO.NonFoundObjectBo(ex.Message, ex); }

            if (productDo.InStock > 0) /// making sure that ther is any product in stock
            {
                BO.OrderItem orderItemBo = cart.Items!.FirstOrDefault(i => i!.ProductID == productID)!;

                if (orderItemBo is null) /// if there is no items in the cart
                {
                    cart.Items!.Add(new BO.OrderItem /// insert the order item in to the cart whith basics
                    {
                        ProductID = productID,
                        Name = productDo.Name,
                        Price = productDo.Price,
                        Amount = productAmount,
                        TotalPrice = productDo.Price
                    });
                    cart.TotalPrice += productDo.Price;
                }
                else /// if the cart is not empty
                {
                    orderItemBo.Amount += productAmount;
                    orderItemBo.TotalPrice += orderItemBo.Price * productAmount;
                    cart.TotalPrice += orderItemBo.Price * productAmount;
                }
            }
            else /// if the stock is empty
                throw new BO.NotInStock("Error - The product is out of stock!");

            return cart;
        }
        /// <summary>
        /// making an order
        /// </summary>
        /// <param name="cart"></param>
        /// <exception cref="BO.NonFoundObjectBo"></exception>
        /// <exception cref="BO.ExistingObjectBo"></exception>
        public void OrderMaking(BO.Cart cart)
        {
            try
            {
                BO.Product productBo = new ();

                for (int i = 0; i < cart.Items!.Count; i++)///for each item
                {
                    productBo = product.ProductDetailsForManager(cart.Items![i]!.ProductID);

                    if(cart.Items[i]!.Amount < 0)
                        throw new BO.NotValid("Error - Amount can't be a negative number!");
                    if(productBo.InStock <= 0)
                        throw new BO.NotValid("Error - The product is out of stock!");
                    if(cart.CustomerEmail != " ")
                        throw new BO.NotValid("Error - Customer email box can't be empty!");
                    if(cart.CustomerName != " ")
                        throw new BO.NotValid("Error - Customer name box can't be empty!");
                    if(cart.CustomerAddress != " ")
                        throw new BO.NotValid("Error - Customer address box can't be empty!");
                    if (!new EmailAddressAttribute().IsValid(cart.CustomerEmail))
                        throw new BO.NotValid("Error - The email address is not valid!");
                    //if (cart.Imege == " ")
                    //    throw new BO.NotValid("Error - Imege box can't be empty!");

                    BO.Order orderBo = new ()///initialize with basic values
                    {
                        Status = BO.OrderStatus.Confirmed,
                        OrderDate = DateTime.Now,
                        DeliveryDate = null,
                        PaymentDate = null,
                        ShipDate = null,
                        Items = cart.Items
                    };

                    orderBo.CopyPropTo(cart);/// copy the datails from the otder to the cart (the same values)
                    DO.Order orderDo = new ();
                    orderDo = orderBo.CopyPropToStruct(orderDo); /// using the function that copy froBO to DO(from class to struct)

                    int ID;
                    ID = Dal!.Order.Add(orderDo);

                    BO.OrderItem orderItemBo = new ()
                    {
                        ID = ID,
                        Name = cart.CustomerName,
                        ProductID = cart.Items[i]!.ProductID,
                        Amount = cart.Items[i]!.Amount,
                        Price = cart.Items[i]!.Price,
                        TotalPrice = cart.Items[i]!.TotalPrice
                    };
                    DO.OrderItem orderItemDo = new ();
                    orderItemDo = orderItemBo.CopyPropToStruct(orderItemDo);
                    Dal.OrderItem.Add(orderItemDo);

                    DO.Product productDo = new ();

                    productDo = Dal.Product.RequestByPredicate(product => product?.ID == cart.Items[i]!.ProductID);

                    productDo.InStock -= cart.Items[i]!.Amount;

                    Dal.Product.Update(productDo);
                }
            }
            catch (DO.NonFoundObjectDo ex)///if run into issue throw error message
            { throw new BO.NonFoundObjectBo(ex.Message, ex); }
            catch (DO.ExistingObjectDo ex)
            { throw new BO.ExistingObjectBo(ex.Message, ex); }

        }

        /// <summary>
        /// updating the amount in the recieved cart with the given id to the new amount
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="productId"></param>
        /// <param name="Amount"></param>
        /// <returns></returns>
        /// <exception cref="BO.NonFoundObjectBo"></exception>
        public BO.Cart UpdateAmountProduct(BO.Cart cart, int productId, int Amount)
        {
            if (!cart.Items!.Exists(i => i!.ProductID == productId))///if this product is actually excisting in the given cart
                throw new BO.NonFoundObjectBo("Error - The product does not exist");

            int index = cart.Items.FindIndex(cart => cart!.ProductID == productId);

            int diffrence = Amount - cart.Items[index]!.Amount;///saving the difference between the old and new amount
            if (diffrence != 0)///if its just the same skip the process and no changes needed
            {
                if (Amount == 0)///new amount empty the products
                {
                    cart.TotalPrice -= cart.Items[index]!.TotalPrice;
                    cart.Items.Remove(cart.Items[index]);
                }
                else if (diffrence < 0)///new amount is smaller
                {
                    cart.Items[index]!.Amount += diffrence; // edding a negitiv number
                    cart.Items[index]!.TotalPrice += cart.Items[index]!.Price * diffrence;
                    cart.TotalPrice += cart.Items[index]!.Price * diffrence;
                }
                else///new amount is larger
                {
                    cart.Items[index]!.Amount += diffrence;
                    cart.Items[index]!.TotalPrice = cart.Items[index]!.Price * cart.Items[index]!.Amount;
                    cart.TotalPrice += cart.Items[index]!.ProductID * diffrence;
                }
            }

            return cart;
        }
    }
}
