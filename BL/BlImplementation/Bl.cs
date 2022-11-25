using BlApi;

namespace BlImplementation
{
    public class Bl : IBl
    {
        public Bl() { }
        public IProduct Product { get; set; } = new Product();
        public IOrder Order { get; set; } = new Order();
        public ICart Cart { get; set; } = new Cart();
    }
}
