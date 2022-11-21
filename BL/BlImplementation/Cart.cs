using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using CopyPropertisTo;
using Dal;
using DalApi;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace BlImplementation
{
    public class Cart : ICart
    {
        public IDal Dal = new DalList();
        public BO.Cart AddProductToCart(BO.Cart Item, int productID)
        {
            DO.Product pro = new DO.Product();
            try
            { pro = Dal.Product.RequestById(productID); }
            catch (DO.NonFoundObject)
            { throw new Exception("not found"); }
            if (Item.Items.First(i => i.ID == productID) is null)
            {
                if(pro.InStock > 0)
                {
                    Item.Items.
                }
                Item.Items
            }
            else
            {
                if(pro.InStock > 0)
                {
                    
                }
            }
            return Item;
        }

        public void OrderMaking(BO.Cart Item, string Name, string Email, string Address)
        {
            
        }

        public BO.Cart UpdateAmountProduct(BO.Cart Item, int ID, int Amount)
        {

            return null;
        }
    }
}
