using System.Text.RegularExpressions;
using BlApi;
using CopyPropertisTo;

namespace BlImplementation
{
    internal class Cart : ICart
    {
        public DalApi.IDal Dal = new Dal.DalList();
        public IBl Ibl = new Bl();
        public BO.Cart AddProductToCart(BO.Cart cart, int productID)
        {
            DO.Product productDo;

            try
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

        public void OrderMaking(BO.Cart Item)
        {
            BO.Product productBo = new BO.Product();

            for(int i = 0; i < Item.Items.Count(); i++)
            {
                try
                {
                    productBo = Ibl.Product.ProductDetailsForManager(Item.Items[i].ProductID);
                }
                catch(DO.NonFoundObjectDo ex)
                { throw new BO.NonFoundObjectBo("", ex); }

                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                MatchCollection matchCollection = regex.Matches(Item.CustomerEmail);

                if (Item.Items[i].Amount > 0 && productBo.InStock > 0 &&  Item.CustomerEmail != " " && 
                    Item.CustomerName != " " && Item.CustomerAddress != " " && matchCollection.Count < 1)
                {
                    BO.Order orderBo = new BO.Order()
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

                    try
                    { ID = Dal.Order.Add(orderDo); }
                    catch(DO.ExistingObjectDo ex)
                    { throw new BO.ExistingObjectBo("", ex); }

                    BO.OrderItem orderItemBo = new BO.OrderItem() { ID = ID, Name = Item.CustomerName,
                    ProductID = Item.Items[i].ProductID, Amount = Item.Items[i].Amount, Price = Item.Items[i].Price,
                    TotalPrice = Item.Items[i].TotalPrice};
                    DO.OrderItem orderItemDo = new DO.OrderItem();
                    orderItemDo = orderItemBo.CopyPropToStruct(orderItemDo);

                    try
                    { Dal.OrderItem.Add(orderItemDo); }
                    catch (DO.ExistingObjectDo ex)
                    { throw new BO.ExistingObjectBo("", ex); }

                    DO.Product productDo = new DO.Product();

                    try
                    { productDo = Dal.Product.Get(Item.Items[i].ProductID); }
                    catch(DO.NonFoundObjectDo ex)
                    { throw new BO.NonFoundObjectBo("", ex); }

                    productDo.InStock -= Item.Items[i].Amount;

                    try
                    { Dal.Product.Update(productDo); }
                    catch(DO.NonFoundObjectDo ex)
                    { throw new BO.NonFoundObjectBo("", ex); }
                }
            }
        }

        public BO.Cart UpdateAmountProduct(BO.Cart cart, int productId, int Amount)
        {
            if (cart.Items.Exists(i => i.ProductID == productId))
            {
                for (int i = 0; i < cart.Items.Count(); i++)
                {
                    if (cart.Items[i].ProductID == productId)
                    {
                        int diffrence = Amount - cart.Items[i].Amount;
                        if (diffrence != 0)
                        {
                            if (Amount == 0)
                            {
                                cart.TotalPrice -= cart.Items[i].TotalPrice;
                                cart.Items.Remove(cart.Items[i]);
                            }
                            else if (diffrence < 0)
                            {
                                cart.Items[i].Amount += diffrence; // edding a negitiv number
                                cart.Items[i].TotalPrice += cart.Items[i].Price * diffrence;
                                cart.TotalPrice += cart.Items[i].Price * diffrence;
                            }
                            else
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
            else
                throw new BO.NonFoundObjectBo();

            return cart;
        }
    }
}
