using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface IOrderManager
    {
        public void AddingProduct(BO.Cart Item);
        public void DeletingProduct(int ID);
        public void UpdateAmountOfProduct(BO.Cart Item);
    }
}
