﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public enum Category { Clothing, Shoes }
    public enum OrderStatus { Confirmed, Shipped, Delivered }
    public enum Gender { Women, Men, Boys, Girls }
    public enum Clothing { Skirts, Dresses, Blazers, Hoodies, Sweatshirts, Shirts, Socks, Pants, Coats, Jackets, SportWear }
    public enum Shoes { Heels, Sneakers, Boots, Sport, Sandals }
    public enum Color { Black, White, Green, Yellow, Red, Blue, Beige, Brown, Jeans, Orange, Pink, Purple, Gray, Gold, Silver, Colored }
    public enum SizeClothing { XS, S, M, L, XL }
    public enum SizeShoes { xs = 36, s, m, l, xl, xxl }
}
