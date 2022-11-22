using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using CopyPropertisTo;
using Dal;
using DalApi;
using DocumentFormat.OpenXml.Office.CustomUI;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Office2016.Drawing.Charts;

namespace BlImplementation
{
    internal class Cart : ICart
    {
        public IDal Dal = new DalList();
        public IBl Ibl = new Bl();
        public BO.Cart AddProductToCart(BO.Cart Item, int productID)
        {
            BO.Product productBo = new BO.Product();
            BO.OrderItem orderItemBo = new BO.OrderItem();
            try
            { productBo = Ibl.Product.ProductDetailsForManager(productID); }
            catch (DO.NonFoundObject)
            { throw new Exception("not found"); }
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
                throw new Exception("Not int stock");
            return Item;
        }

        public void OrderMaking(BO.Cart Item, string Name, string Email, string Address)
        {
            BO.Product productBo = new BO.Product();
            for(int i = 0; i < Item.Items.Count(); i++)
            {
                try
                {
                    productBo = Ibl.Product.ProductDetailsForManager(Item.Items[i].ProductID);
                }
                catch(DO.NonFoundObject)
                { throw new Exception("not found"); }
                if (Item.Items[i].Amount > 0 && productBo.InStock > 0 &&  Item.CustomerEmail != " " && 
                    Item.CustomerName != " " && Item.CustomerAddress != " ")
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
                    catch(DO.ExistingObject)
                    { throw new Exception("already exists"); }
                    BO.OrderItem orderItemBo = new BO.OrderItem() { ID = ID, Name = Item.CustomerName,
                    ProductID = Item.Items[i].ProductID, Amount = Item.Items[i].Amount, Price = Item.Items[i].Price,
                    TotalPrice = Item.Items[i].TotalPrice};
                    DO.OrderItem orderItemDo = new DO.OrderItem();
                    orderItemBo.CopyPropToStruct(orderItemDo);
                    orderItemBo.CopyPropTo(orderItemDo);
                    try
                    { Dal.OrderItem.Add(orderItemDo); }
                    catch (DO.ExistingObject)
                    { throw new Exception("already exists"); }
                    DO.Product productDo = new DO.Product();
                    productDo = Dal.Product.RequestById(Item.Items[i].ProductID);
                    productDo.InStock -= Item.Items[i].Amount;
                    try
                    { Dal.Product.Update(productDo); }
                    catch(DO.NonFoundObject)
                    { throw new Exception("not exist"); }
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
                        break;
                    }
                    else
                        throw new Exception("not chang");
                }
            }
            else
                throw new Exception("not Exist");
            return Item;
        }
    }
}
