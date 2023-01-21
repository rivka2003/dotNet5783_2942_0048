using DalApi;

namespace Dal
{
    /// <summary>
    /// The dalXml class is defined as a singleton class
    /// </summary>
    internal sealed class DalXml : IDal
    {
        public static IDal Instance { get; } = new DalXml();

        public IProduct Product { get; }
        public IOrder Order { get; }
        public IOrderItem OrderItem { get; }

        private DalXml()
        {
            Product = new DalProduct();
            Order = new DalOrder();
            OrderItem = new DalOrderItem();
        }
    }
}
