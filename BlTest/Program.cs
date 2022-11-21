using CopyPropertisTo;

namespace BlTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BO.Product product = new BO.Product();
            DO.Product productDo = new DO.Product();
            object obj = productDo;
            product.category = BO.Category.Shoes;
            product.shoes = BO.Shoes.Boots;
            product.InStock = 700;
            product.ID = 8000;
            product.Name = "fnvhud";
            product.CopyPropTo(obj);
            productDo = (DO.Product)obj;
            Console.WriteLine(productDo);
        }
    }
}