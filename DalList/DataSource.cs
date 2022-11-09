using DO;
using System.Collections.Generic;

namespace Dal
{
    internal static class DataSource
    {
        /// <summary>
        /// Making the Config class to make a run number
        /// </summary>
        internal static class Config
        {
            internal static int ProductsIndex = 0;
            internal static int OrdersIndex = 0;
            internal static int OrderItemIndex = 0;

            internal static int orderItemSequenceID = 0;
            internal static int orderSequenceID = 0;
            public static int OrderItemSequenceID => ++orderItemSequenceID;
            public static int OrderSequenceID => ++orderSequenceID;
        }
        internal static List<Product> Products = new List<Product>();
        internal static List<Order> Orders = new List<Order>();
        internal static List<OrderItem> OrderItems = new List<OrderItem>();
        static readonly Random random = new Random(); /// for the random numbers

        const int initialNumOfProducts = 50;
        const int initialNumOfOrders = 100;
        const int initialNumOfOrderItems = 200;
        /// <summary>
        /// constructor
        /// </summary>
        static DataSource()
        {
            s_Initialize();
        }
        private static void s_Initialize()
        {
            InitProduct();
            InitOrder();
            InitOrderItem();
        }

        private static void InitProduct()
        {
            Array Categories = Enum.GetValues(typeof(Category)), Clothes = Enum.GetValues(typeof(Clothing)), Shoe = Enum.GetValues(typeof(Shoes));
            Array Colors = Enum.GetValues(typeof(Color)), Genders = Enum.GetValues(typeof(Gender));
            Array SizeShoe = Enum.GetValues(typeof(SizeShoes)), SizeCloth = Enum.GetValues(typeof(SizeClothes));
            Category randomCategory; Clothing randomClothes; Shoes randomShoes; Color randomColor;
            Gender randomGender; SizeClothes randomSizeClothes; SizeShoes randomSizeShoes;
            for (int i = 0, j =1; i < 50; i++, j++)
            {
                randomCategory = (Category)Categories.GetValue(random.Next(Categories.Length));

                randomGender = (Gender)Genders.GetValue(random.Next(Genders.Length));
                randomColor = (Color)Colors.GetValue(random.Next(Colors.Length));
                if (randomCategory == 0)
                {
                    randomClothes = (Clothing)Clothes.GetValue(random.Next(Clothes.Length));
                    randomSizeClothes = (SizeClothes)SizeCloth.GetValue(random.Next(SizeCloth.Length));
                    Product value = new Product()
                    {
                        ID = random.Next(1000000000),
                        InStock = random.Next(1, 4),
                        Name = "Product" + j,
                        Price = 100,
                        category = randomCategory,
                        gender = randomGender,
                        color = randomColor,
                        Status = true,
                        clothing = randomClothes,
                        sizeClothing = randomSizeClothes
                    };
                    Products.Add(value);
                }
                else
                {
                    randomShoes = (Shoes)Shoe.GetValue(random.Next(Shoe.Length));
                    randomSizeShoes = (SizeShoes)SizeShoe.GetValue(random.Next(SizeShoe.Length));
                    Product value = new Product()
                    {
                        ID = random.Next(100000, 1000000000),
                        InStock = random.Next(1,4),
                        Name = "Product" + j,
                        Price = 100,
                        category = randomCategory,
                        gender = randomGender,
                        color = randomColor,
                        Status = true,
                        shoes = randomShoes,
                        sizeShoes = randomSizeShoes
                    };
                    Products.Add(value);
                }
            }
            Config.ProductsIndex = initialNumOfProducts;
        }

        private static void InitOrder()
        {
            for(int i = 0, j = 1; i < 100; i++, j++)
            {
                Order value = new Order() 
                { ID = Config.OrderSequenceID, CustomerName = "Order" + j, 
                CustomerAddress = "Menachem Begin" + j, CustomerEmail = "Customer" + j + "@gmail.com",
                DeliveryDate = DateTime.MinValue, OrderDate = DateTime.MinValue, ShipDate = new TimeSpan( };
                Orders.Add(value);
            }
            Config.OrdersIndex = initialNumOfOrders;
        }
        private static void InitOrderItem()
        {
            for (int i = 0, j = 1; i < 200; j++)
            {
                int amount = random.Next(1, 4);
                OrderItem value = new OrderItem() { ID = Config.orderItemSequenceID, Amount = amount,
                OrderID = random.Next(1, 100), ProductID = random.Next(100000, 1000000000), Price = 500 };
                i += amount;
            }
            Config.ProductsIndex = initialNumOfOrderItems;
        }
    }
}