/*using System;
using System.Collections.Generic;
using System.Text;*/

namespace DO
{
    public enum Status { Exist, NotExist}
    public enum Category { Clothing, Shoes }
    public enum Gender { Women, Men, Boys, Girls}
    public enum Clothing { Blazers, Hoodies, Sweatshirts, Shirts, Socks, Pants, Coats, Jackets, SportWear, Skirts, Dresses }
    public enum Shoes { Sneakers, Boots, Sport, Sandals, Heels }
    public enum Color { Black, White, Green, Yellow, Red, Blue, Beige, Brown, Jeans, Orange, Pink, Purple, Gray, Gold, Silver, Colored }
    public enum SizeClothing { XS, S, M, L, XL}
    public enum SizeShoes { xs = 36, s, m, l, xl, xxl}
    public enum Choice { Exit, Product, Order, OrderItem }
    public enum OrderChoice { Exit, Add, Print, PrintList, Update, Delete}
    public enum Choice3 { Exit, Add, PrintByID, PrintByOrderAndProductID, PrintList, PrintByOrderID, Update, Delete }
}
