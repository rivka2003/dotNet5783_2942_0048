using DalApi;

namespace Dal
{
    /// <summary>
    /// The dalList class is defined as a singleton class
    /// We created it as Thread Safe so that when there is parallel use between two different devices then the first device will be handled first and then the second.
    /// In addition, we made it lazy so that the object is initialized only when it is called and not when the program starts running,
    /// We used Sixth - version - using .NET 4's Lazy<T> type
    /// </summary>
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
