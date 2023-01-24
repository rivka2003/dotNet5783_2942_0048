using DalApi;

namespace Dal
{
    /// <summary>
    /// The dalXml class is defined as a singleton class
    /// </summary>
    internal sealed class DalXml : IDal
    {
        private static readonly Lazy<DalXml> lazy = new Lazy<DalXml>(() => new DalXml());
        public static IDal Instance { get { return lazy.Value; } }

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
