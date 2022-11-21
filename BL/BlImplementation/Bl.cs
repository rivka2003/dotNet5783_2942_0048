using BlApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation
{
    public class Bl : IBl
    {
        public Bl() { }
        public IProduct Product { get; } = new Product();
        public IOrder Order { get; } = new Order();
        public ICart Cart { get; } = new Cart();
    }
}
