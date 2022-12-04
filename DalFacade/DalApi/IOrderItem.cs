using DO;
namespace DalApi
{
    public interface IOrderItem : ICrude<OrderItem>
    {
        OrderItem RequestByOrderAndProductID(int value1, int value2);
    }
}
