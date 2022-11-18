namespace DalApi
{
    public interface IDal
    {
        public IProduct Product { get; }
        public IOrder Order { get; }
        public IOrderItem OrderItem { get; }
    }
}
