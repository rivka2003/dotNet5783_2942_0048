using System.Text.RegularExpressions;
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
            { productDo = Dal.Product.Get(productID); }
            catch (DO.NonFoundObjectDo ex)
            { throw new BO.NonFoundObjectBo("", ex); }
            
            if (productDo.InStock > 0)
            {
                BO.OrderItem orderItemBo = cart.Items.FirstOrDefault(i => i.ProductID == productID)!;

                if (orderItemBo is null)
                {
                    cart.Items.Add(new BO.OrderItem
                    {
                        ProductID = productID,
                        Name = productDo.Name,
                        Price = productDo.Price,
                        Amount = 1,
                        TotalPrice = productDo.Price
                    });
                    cart.TotalPrice += productDo.Price;
                }
                else
                {
                    orderItemBo.Amount++;
                    orderItemBo.TotalPrice += orderItemBo.Price;
                    cart.TotalPrice += orderItemBo.Price;
                }
            }
            else
                throw new BO.NotInStock();

            return cart;
        }
        /// <summary>
        /// making an order
        /// </summary>
        /// <param name="Item"></param>
        /// <exception cref="BO.NonFoundObjectBo"></exception>
        /// <exception cref="BO.ExistingObjectBo"></exception>
        public void OrderMaking(BO.Cart Item)
        {
            BO.Product productBo = new BO.Product();

            for(int i = 0; i < Item.Items.Count(); i++)///for each item
            {
                try///try to pu the data in product bo
                {
                    productBo = Ibl.Product.ProductDetailsForManager(Item.Items[i].ProductID);
                }
                catch(DO.NonFoundObjectDo ex)///if run into issue throw error message
                { throw new BO.NonFoundObjectBo("", ex); }

                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");///checking if email address is valid
                MatchCollection matchCollection = regex.Matches(Item.CustomerEmail);

                if (Item.Items[i].Amount > 0 && productBo.InStock > 0 &&  Item.CustomerEmail != " " && 
                    Item.CustomerName != " " && Item.CustomerAddress != " " && matchCollection.Count < 1)///if all the items details were given and are correct
                {
                    BO.Order orderBo = new BO.Order()///initialize with basic values
                    {
                        Status = BO.OrderStatus.Confirmed,
                        OrderDate = DateTime.Now,
                        DeliveryDate = DateTime.MinValue,
                        PaymentDate = DateTime.MinValue,
                        ShipDate = DateTime.MinValue,
                        Items = Item.Items
                    };

                    orderBo.CopyPropTo(Item);
                    DO.Order orderDo = new DO.Order();
                    orderDo = orderBo.CopyPropToStruct(orderDo);

                    int ID;

                    try///try to add the made order
                    { ID = Dal.Order.Add(orderDo); }
                    catch(DO.ExistingObjectDo ex)
                    { throw new BO.ExistingObjectBo("", ex); }

                    BO.OrderItem orderItemBo = new BO.OrderItem() { ID = ID, Name = Item.CustomerName,
                    ProductID = Item.Items[i].ProductID, Amount = Item.Items[i].Amount, Price = Item.Items[i].Price,
                    TotalPrice = Item.Items[i].TotalPrice};
                    DO.OrderItem orderItemDo = new DO.OrderItem();
                    orderItemDo = orderItemBo.CopyPropToStruct(orderItemDo);

                    try///try to add the made order item
                    { Dal.OrderItem.Add(orderItemDo); }
                    catch (DO.ExistingObjectDo ex)
                    { throw new BO.ExistingObjectBo("", ex); }

                    DO.Product productDo = new DO.Product();

                    try///try to receive the product according to its id
                    { productDo = Dal.Product.Get(Item.Items[i].ProductID); }
                    catch(DO.NonFoundObjectDo ex)
                    { throw new BO.NonFoundObjectBo("", ex); }

                    productDo.InStock -= Item.Items[i].Amount;

                    try///try to update the product
                    { Dal.Product.Update(productDo); }
                    catch(DO.NonFoundObjectDo ex)
                    { throw new BO.NonFoundObjectBo("", ex); }
                }
            }
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
            if (cart.Items.Exists(i => i.ProductID == productId))///if this product is actually excisting in the given cart
            {
                for (int i = 0; i < cart.Items.Count(); i++)///go over all the items list in the cart
                {
                    if (cart.Items[i].ProductID == productId)///search the product
                    {
                        int diffrence = Amount - cart.Items[i].Amount;///saving the difference between the old and new amount
                        if (diffrence != 0)///if its just the same skip the process and no changes needed
                        {
                            if (Amount == 0)///new amount empty the products
                            {
                                cart.TotalPrice -= cart.Items[i].TotalPrice;
                                cart.Items.Remove(cart.Items[i]);
                            }
                            else if (diffrence < 0)///new amount is smaller
                            {
                                cart.Items[i].Amount += diffrence; // edding a negitiv number
                                cart.Items[i].TotalPrice += cart.Items[i].Price * diffrence;
                                cart.TotalPrice += cart.Items[i].Price * diffrence;
                            }
                            else///new amount is larger
                            {
                                cart.Items[i].Amount += diffrence;
                                cart.Items[i].TotalPrice = cart.Items[i].Price * cart.Items[i].Amount;
                                cart.TotalPrice += cart.Items[i].ProductID * diffrence;
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
