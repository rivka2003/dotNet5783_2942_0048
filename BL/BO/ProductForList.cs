
namespace BO
{
    public class ProductForList
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
        public string? Image { get; set; }
        /// <summary>
        /// The Imege of the product
        /// </summary>
        public override string ToString()
        {
            string str = Category is BO.Category.Clothing ?
@$"Clothing : {Clothing}
Size of Clothing: {SizeClothing}" :
@$"Shoes: {Shoes} 
Size of Shoes: {(SizeShoes.HasValue ? (int)SizeShoes.Value : 0)}";

            return $@"
ID: {ID}
Name: {Name}
Price: {Price}
Category: {Category}
Description: {Description}
Gender: {Gender}
Color: {Color}
{str}
{Image}
";
        }
    }
}
