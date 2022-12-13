
namespace BlApi
{
    public interface IOrderManager
    {
        public void AddingProduct(BO.Cart Item);
        public void DeletingProduct(int ID);
        public void UpdateAmountOfProduct(BO.Cart Item);
    }
}
