

using System.Xml.Linq;

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
    /// The amount of items that have ordered
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $@"
    ID: {ID}
    Product ID: {ProductID}
    Order ID: {OrderID}
    Price: {Price}
    Amount: {Amount}
    ";
    ///printing product details function
}
