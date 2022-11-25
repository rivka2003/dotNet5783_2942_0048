

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
    public Status Status { get; set; } /// <summary>
                                       /// check if to do with enum
                                       /// True - there is a product in stock
                                       /// False - there is no products at all
                                       /// </summary>
    public Category Category { get; set; }
    /// <summary>
    /// The Category of the product
    /// </summary>
    /// <returns></returns>
    public Gender Gender { get; set; }
    /// <summary>
    /// The Gender tip of the product
    /// </summary>
    /// <returns></returns>
    public Clothing Clothing { get; set; }
    /// <summary>
    /// Cloth tip 
    /// </summary>
    /// <returns></returns>
    public Shoes Shoes { get; set; }
    /// <summary>
    /// Shoe tip
    /// </summary>
    /// <returns></returns>
    public Color Color { get; set; }
    /// <summary>
    /// The Color of the product
    /// </summary>
    /// <returns></returns>
    public SizeClothing SizeClothing { get; set; }
    /// <summary>
    /// The size of the Clothing
    /// </summary>
    public SizeShoes SizeShoes { get; set; }
    /// <summary>
    /// The size of the Shoes
    /// </summary>
    /// <returns></returns>
    public string Description { get; set; }
    /// <summary>
    /// The Description of the product
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        string str = Category is Category.Clothing ?
      @$"Clothing : {Clothing}
         Size of Clothing: {SizeClothing}" :
      @$"Shoes: {Shoes} 
         Size of Shoes: {(int)SizeShoes}";
        return $@"
         ID: {ID}
         Name: {Name}
         Price: {Price}
         Amount in stock: {InStock}
         Description: {Description}
         Gender: {Gender}
         Color: {Color}
         Category: {Category}
         ";
    }
}
