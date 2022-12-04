
namespace DO;

public struct Order
{
    public int ID { get; set; }
    /// <summary>
    /// The ID number of the order
    /// </summary>
    public string? CustomerName { get; set; }
    /// <summary>
    /// The name of the customer
    /// </summary>
    public string? CustomerEmail { get; set; }
    /// <summary>
    /// The email of the customer
    /// </summary>
    public string? CustomerAddress { get; set; }
    /// <summary>
    /// The address of the customer
    /// </summary>
    public DateTime? OrderDate { get; set; }
    /// <summary>
    /// The date the order was placed 
    /// </summary>
    public DateTime? ShipDate { get; set; }
    /// <summary>
    /// The date of shipment
    /// </summary>
    public DateTime? DeliveryDate { get; set; }
    /// Delivery arrival date
    public override string ToString() => this.ToStringProperty();
    /// printing order details function
}
