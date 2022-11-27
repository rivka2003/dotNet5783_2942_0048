using BlApi;

namespace BlImplementation
{
    public class Bl : IBl
    {
        public Bl() { }
        public IProduct Product  => new Product();
        public IOrder Order => new Order();
        public ICart Cart => new Cart();
    }
}
