using DO;
namespace DalApi
{
    public interface IOrderItem : ICrude<OrderItem>
    {
        IEnumerable<OrderItem> RequestAllByOrderID(int value);
        OrderItem RequestByOrderAndProductID(int value1, int value2);
    }
}
