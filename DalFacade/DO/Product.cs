

namespace DO;

public struct Product
{
    public int ID { get; set; }
    /// <summary>
    /// The ID number of the product
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// The Name of the product
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    ///The price of each product
    /// </summary>
    public int InStock { get; set; }
    /// <summary>
    /// The amount of products available from the product
    /// </summary>
    public bool Status { get; set; } /// <summary>
    /// check if to do with enum
    /// True - there is a product in stock
    /// False - there is no products at all
    /// </summary>
    public Category category { get; set; }
    /// <summary>
    /// The category of the product
    /// </summary>
    /// <returns></returns>
    public Gender gender { get; set; }
    /// <summary>
    /// The Gender tip of the product
    /// </summary>
    /// <returns></returns>
    public Clothing clothing { get; set; }
    /// <summary>
    /// Cloth tip 
    /// </summary>
    /// <returns></returns>
    public Shoes shoes { get; set; }
    /// <summary>
    /// Shoe tip
    /// </summary>
    /// <returns></returns>
    public Color color { get; set; }
    /// <summary>
    /// The color of the product
    /// </summary>
    /// <returns></returns>
    public SizeClothes sizeClothing { get; set; }
    /// <summary>
    /// The size of the Clothing
    /// </summary>
    public SizeShoes sizeShoes { get; set; }
    /// <summary>
    /// The size of the shoes
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $@"
    ID: {ID}
    Name: {Name}
    Price: {Price}
    Amount in stock: {InStock}
    Status: {Status}
    category: {category}
    Gender: {gender}
    ";
    ///printing product details function
}
