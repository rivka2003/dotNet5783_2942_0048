using System.ComponentModel.DataAnnotations;
using BlApi;
using CopyPropertisTo;

namespace BlImplementation
{
    internal class Cart : ICart
    {
        public DalApi.IDal Dal = new Dal.DalList();
        public IBl Ibl = new Bl();
        /// <summary>
        /// adding a product with the given id to the cart
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="productID"></param>
        /// <returns></returns>
        /// <exception cref="BO.NonFoundObjectBo"></exception>
        /// <exception cref="BO.NotInStock"></exception>
        public BO.Cart AddProductToCart(BO.Cart cart, int productID)
        {
            DO.Product productDo;

            try///making sure the oroduct exists in the products list
            { productDo = Dal.Product.RequestByPredicate(product => product!.Value.ID == productID); }
            catch (DO.NonFoundObjectDo ex)
            { throw new BO.NonFoundObjectBo("", ex); }

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
                        Amount = 1,
                        TotalPrice = productDo.Price
                    });
                    cart.TotalPrice += productDo.Price;
                }
                else /// if the cart is not empty
                {
                    orderItemBo.Amount++;
                    orderItemBo.TotalPrice += orderItemBo.Price;
                    cart.TotalPrice += orderItemBo.Price;
                }
            }
            else /// if the stock is empty
                throw new BO.NotInStock();

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
                BO.Product productBo = new BO.Product();

                for (int i = 0; i < cart.Items!.Count(); i++)///for each item
                {
                    productBo = Ibl.Product.ProductDetailsForManager(cart.Items![i]!.ProductID);

                    if (cart.Items[i]!.Amount > 0 && productBo.InStock > 0 && cart.CustomerEmail != " " &&
                        cart.CustomerName != " " && cart.CustomerAddress != " " && new EmailAddressAttribute().IsValid(cart.CustomerEmail) == true)///if all the items details were given and are correct and valid
                    {
                        BO.Order orderBo = new BO.Order()///initialize with basic values
                        {
                            Status = BO.OrderStatus.Confirmed,
                            OrderDate = DateTime.Now,
                            DeliveryDate = null,
                            PaymentDate = null,
                            ShipDate = null,
                            Items = cart.Items
                        };

                        orderBo.CopyPropTo(cart);/// copy the datails from the otder to the cart (the same values)
                        DO.Order orderDo = new DO.Order();
                        orderDo = orderBo.CopyPropToStruct(orderDo); /// using the function that copy froBO to DO(from class to struct)

                        int ID;
                        ID = Dal.Order.Add(orderDo);

                        BO.OrderItem orderItemBo = new BO.OrderItem()
                        {
                            ID = ID,
                            Name = cart.CustomerName,
                            ProductID = cart.Items[i]!.ProductID,
                            Amount = cart.Items[i]!.Amount,
                            Price = cart.Items[i]!.Price,
                            TotalPrice = cart.Items[i]!.TotalPrice
                        };
                        DO.OrderItem orderItemDo = new DO.OrderItem();
                        orderItemDo = orderItemBo.CopyPropToStruct(orderItemDo);
                        Dal.OrderItem.Add(orderItemDo);


                        DO.Product productDo = new DO.Product();

                        productDo = Dal.Product.RequestByPredicate(product => product!.Value.ID == cart.Items[i]!.ProductID);

                        productDo.InStock -= cart.Items[i]!.Amount;

                        Dal.Product.Update(productDo);
                    }
                    else
                        throw new BO.NotValid();
                }
            }
            catch (DO.NonFoundObjectDo ex)///if run into issue throw error message
            { throw new BO.NonFoundObjectBo("", ex); }
            catch (DO.ExistingObjectDo ex)
            { throw new BO.ExistingObjectBo("", ex); }

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
            if (cart.Items!.Exists(i => i!.ProductID == productId))///if this product is actually excisting in the given cart
            {
                for (int i = 0; i < cart.Items.Count(); i++)///go over all the items list in the cart
                {
                    if (cart.Items[i]!.ProductID == productId)///search the product
                    {
                        int diffrence = Amount - cart.Items[i]!.Amount;///saving the difference between the old and new amount
                        if (diffrence != 0)///if its just the same skip the process and no changes needed
                        {
                            if (Amount == 0)///new amount empty the products
                            {
                                cart.TotalPrice -= cart.Items[i]!.TotalPrice;
                                cart.Items.Remove(cart.Items[i]);
                            }
                            else if (diffrence < 0)///new amount is smaller
                            {
                                cart.Items[i]!.Amount += diffrence; // edding a negitiv number
                                cart.Items[i]!.TotalPrice += cart.Items[i]!.Price * diffrence;
                                cart.TotalPrice += cart.Items[i]!.Price * diffrence;
                            }
                            else///new amount is larger
                            {
                                cart.Items[i]!.Amount += diffrence;
                                cart.Items[i]!.TotalPrice = cart.Items[i]!.Price * cart.Items[i]!.Amount;
                                cart.TotalPrice += cart.Items[i]!.ProductID * diffrence;
                            }
                        }
                        break;
                    }
                }
            }
            else///the product isnt in the cart
                throw new BO.NonFoundObjectBo();

            return cart;
        }
    }
}
