using DalApi;

namespace Dal
{
    internal sealed class DalXml : IDal
    {
        public static IDal Instance { get; } = new DalXml();

        public IProduct Product { get; }
        public IOrder Order { get; }
        public IOrderItem OrderItem { get; }

        private DalXml()
        {
            Product = new dalProduct();
            Order = new dalOrder();
            OrderItem = new dalOrderItem();
        }
    }
}
