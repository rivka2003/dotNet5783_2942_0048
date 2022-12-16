using DalApi;

namespace Dal
{
    internal sealed class DalList : IDal
    {
        private static readonly Lazy<DalList> lazy = new Lazy<DalList>(() => new DalList());
        public static DalList Instance { get { return lazy.Value; } }

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
