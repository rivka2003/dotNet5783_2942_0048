using DalApi;

namespace Dal
{
    internal sealed class DalXml : IDal
    {
        private static readonly Lazy<DalXml> lazy = new Lazy<DalXml>(() => new DalXml());
        public static DalXml Instance { get { return lazy.Value; } }

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
