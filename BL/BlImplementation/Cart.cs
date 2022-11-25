using System.Text.RegularExpressions;
using BlApi;
using CopyPropertisTo;

namespace BlImplementation
{
    internal class Cart : ICart
    {
        public DalApi.IDal Dal = new Dal.DalList();
        public IBl Ibl = new Bl();
        public BO.Cart AddProductToCart(BO.Cart Item, int productID)
        {
            BO.Product productBo = new BO.Product();
            BO.OrderItem orderItemBo = new BO.OrderItem();
            try
            { productBo = Ibl.Product.ProductDetailsForManager(productID); }
            catch (DO.NonFoundObjectDo)
            { throw new BO.NonFoundObjectBo(); }
            if (productBo.InStock > 0)
            {
                if (!Item.Items.Exists(i => i.ProductID == productID))
                {
                    orderItemBo.ProductID = productID;
                    orderItemBo.Name = productBo.Name;
                    orderItemBo.Price = productBo.Price;
                    orderItemBo.Amount = 1;
                    orderItemBo.TotalPrice = orderItemBo.Price;
                    Item.Items.Add(orderItemBo);
                    Item.TotalPrice += orderItemBo.Price;
                }
                else
                {
                    orderItemBo.Amount++;
                    orderItemBo.TotalPrice = orderItemBo.Price * orderItemBo.Amount;
                    Item.TotalPrice += orderItemBo.Price;
                }
            }
            else
                throw new BO.NotInStock();
            return Item;
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
                catch(DO.NonFoundObjectDo)
                { throw new BO.NonFoundObjectBo(); }
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
                        ShipDate = DateTime.Now,
                        Items = Item.Items
                    };
                    orderBo.CopyPropTo(Item);
                    DO.Order orderDo = new DO.Order();
                    orderBo.CopyPropToStruct(orderDo);
                    orderBo.CopyPropTo(orderDo);
                    int ID;
                    try
                    { ID = Dal.Order.Add(orderDo); }
                    catch(DO.ExistingObjectDo)
                    { throw new BO.ExistingObjectBo(); }
                    BO.OrderItem orderItemBo = new BO.OrderItem() { ID = ID, Name = Item.CustomerName,
                    ProductID = Item.Items[i].ProductID, Amount = Item.Items[i].Amount, Price = Item.Items[i].Price,
                    TotalPrice = Item.Items[i].TotalPrice};
                    DO.OrderItem orderItemDo = new DO.OrderItem();
                    orderItemBo.CopyPropToStruct(orderItemDo);
                    orderItemBo.CopyPropTo(orderItemDo);
                    try
                    { Dal.OrderItem.Add(orderItemDo); }
                    catch (DO.ExistingObjectDo)
                    { throw new BO.ExistingObjectBo(); }
                    DO.Product productDo = new DO.Product();
                    try
                    { productDo = Dal.Product.Get(Item.Items[i].ProductID); }
                    catch
                    { throw new BO.NonFoundObjectBo(); }
                    productDo.InStock -= Item.Items[i].Amount;
                    try
                    { Dal.Product.Update(productDo); }
                    catch(DO.NonFoundObjectDo)
                    { throw new BO.NonFoundObjectBo(); }
                }
            }
        }

        public BO.Cart UpdateAmountProduct(BO.Cart Item, int productId, int Amount)
        {
            if (Item.Items.Exists(i => i.ProductID == productId))
            {
                for (int i = 0; i < Item.Items.Count(); i++)
                {
                    if (Item.Items[i].ProductID == productId)
                    {
                        int diffrence = Amount - Item.Items[i].Amount;
                        if (diffrence != 0)
                        {
                            if (Amount == 0)
                            {
                                Item.TotalPrice -= Item.Items[i].TotalPrice;
                                Item.Items.Remove(Item.Items[i]);
                            }
                            else if (diffrence < 0)
                            {
                                Item.Items[i].Amount += diffrence; // edding a negitiv number
                                Item.Items[i].TotalPrice += Item.Items[i].Price * diffrence;
                                Item.TotalPrice += Item.Items[i].Price * diffrence;
                            }
                            else
                            {
                                Item.Items[i].Amount += diffrence;
                                Item.Items[i].TotalPrice = Item.Items[i].Price * Item.Items[i].Amount;
                                Item.TotalPrice += Item.Items[i].ProductID * diffrence;
                            }
                        }
                        else
                        { throw new BO.SameAmount(); }
                        break;
                    }
                }
            }
            else
                throw new BO.NonFoundObjectBo();
            return Item;
        }
    }
}
