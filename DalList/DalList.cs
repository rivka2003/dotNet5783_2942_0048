using DalApi;

namespace Dal
{
    internal sealed class DalList : IDal
    {
        public static IDal Instance { get; } = new DalList();

        public IProduct Product { get; }
        public IOrder Order { get; }
        public IOrderItem OrderItem { get; }

        private DalList()
        {
            Product = new DalProduct();
            Order = new DalOrder();
            OrderItem = new DalOrderItem();
        }
    }
}
