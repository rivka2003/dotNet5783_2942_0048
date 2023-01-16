
namespace DO;

public struct OrderItem
{
    public int ID { get; set; }
    /// <summary>
    /// The special number for each orderItem
    /// </summary>
    public int ProductID { get; set; }
    /// <summary>
    /// The ID number of the product
    /// </summary>
    public int OrderID { get; set; }
    /// <summary>
    /// The ID number of the order
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// The price of each product
    /// </summary>
    public int Amount { get; set; }
    /// <summary>
    /// The amount that I have ordered from this product
    /// </summary>
    /// <returns></returns>
    public string? Image { get; set; }
    /// <summary>
    /// The Imege of the product
    /// </summary>
    public override string ToString() => this.ToStringProperty();
    ///printing order item details function
}
