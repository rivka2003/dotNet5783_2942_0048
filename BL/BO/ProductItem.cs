
namespace BO
{
    public class ProductItem
    {
        /// <summary>
        /// The ID of the product
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// The name of the product
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// The price of the product
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// The Category of the product
        /// </summary>
        public BO.Category? Category { get; set; }
        /// <summary>
        /// The amount of the product that in the cart
        /// </summary>
        public int Amount { get; set; }
        /// <summary>
        /// If the product is in stock or not
        /// </summary>
        public BO.InStock? InStock { get; set; }
        public BO.Gender? Gender { get; set; }
        /// <summary>
        /// The Gender tip of the product
        /// </summary>
        /// <returns></returns>
        public BO.Clothing? Clothing { get; set; }
        /// <summary>
        /// Cloth tipe 
        /// </summary>
        /// <returns></returns>
        public BO.Shoes? Shoes { get; set; }
        /// <summary>
        /// Shoe tipe
        /// </summary>
        /// <returns></returns>
        public BO.Color? Color { get; set; }
        /// <summary>
        /// The Color of the product
        /// </summary>
        /// <returns></returns>
        public BO.SizeClothing? SizeClothing { get; set; }
        /// <summary>
        /// The size of the Clothing
        /// </summary>
        public BO.SizeShoes? SizeShoes { get; set; }
        /// <summary>
        /// The size of the Shoes
        /// </summary>
        /// <returns></returns>
        public string? Description { get; set; }
        /// <summary>
        /// The Description of the product
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string str = Category is BO.Category.Clothing ?
@$"Clothing : {Clothing}
Size of Clothing: {SizeClothing}" :
@$"Shoes: {Shoes} 
Size of Shoes: {(SizeShoes.HasValue ? (int)SizeShoes : 0)}";

            return $@"
ID: {ID}
Name: {Name}
Price: {Price}
Category: {Category}
Amount in cart: {Amount}
Gender: {Gender}
Color: {Color}
Description: {Description}
In Stock: {InStock}
{str}
";
        }
    }
}
