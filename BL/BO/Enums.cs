
namespace BO
{
    public enum InStock { No, Yes }
    public enum Category { Clothing, Shoes }
    public enum OrderStatus { Confirmed, Shipped, Delivered }
    public enum Gender { Women, Men, Boys, Girls }
    public enum Clothing { Blazers, Hoodies, Sweatshirts, Shirts, Socks, Pants, Coats, Jackets, SportWear, Skirts, Dresses }
    public enum Shoes { Sneakers, Boots, Sport, Sandals, Heels }
    public enum Color { Black, White, Green, Yellow, Red, Blue, Beige, Brown, Jeans, Orange, Pink, Purple, Gray, Gold, Silver, Colored }
    public enum SizeClothing { XS, S, M, L, XL }
    public enum SizeShoes { xs = 36, s, m, l, xl, xxl, xxxl, xxxxl, xxxxxl, xxxxxxl }
    public enum Choice { Exit, Product, Order, Cart }
    public enum ProductChoice { Exit, Add, PrintDaetailsForCustomer, PrintDetailsForManagaer, PrintList, Updat, Delete }
    public enum OrderChoice { Exit, PrintOrderDetails, PrintTheList, UpdateShipDate, UpdateDeliveryDate, TrackingOrder }
    public enum CartChoice { Exit, AddProduct, DeleteAProduct, ProductsInCart, UpdateAmount, OrderMaking, EmptyCart}
}
