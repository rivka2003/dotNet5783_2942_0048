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
            private static int orderItemSequenceID = 1;
            private static int orderSequenceID = 1;
            internal static int getOrderItemSequenceID() { return orderItemSequenceID++; }
            internal static int getOrderSequenceID() { return orderSequenceID++; }

        }
        internal static List<Product> Products = new List<Product>();
        internal static List<Order> Orders = new List<Order>();
        internal static List<OrderItem> OrderItems = new List<OrderItem>();
        static readonly Random random = new Random(); /// for the random numbers
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
            Array SizeShoe = Enum.GetValues(typeof(SizeShoes)), SizeCloth = Enum.GetValues(typeof(SizeClothing));
            Category randomCategory = new Category(); Clothing randomClothes = new Clothing();
            Shoes randomShoes = new Shoes(); Color randomColor = new Color();
            Gender randomGender = new Gender(); SizeClothing randomSizeClothes = new SizeClothing();
            SizeShoes randomSizeShoes = new SizeShoes();
            for (int i = 1; i <= 10; i++)
            {
                Product value = new Product();
                randomCategory = (Category)Categories.GetValue(random.Next(Categories.Length));
                randomGender = (Gender)Genders.GetValue(random.Next(Genders.Length));
                randomColor = (Color)Colors.GetValue(random.Next(Colors.Length));
                value.category = randomCategory;
                value.ID = random.Next(100000, 999999);
                while (Products.Exists(i => i.ID == value.ID))
                    value.ID = random.Next(100000, 1000000000);
                if (i <= 0.05 * 10)
                    value.InStock = 0;
                else
                    value.InStock = random.Next(1, 4);
                value.Name = "Product" + i;
                value.Price = 100;
                value.color = randomColor;
                value.status = 0;
                value.Description = "New!";
                if (randomCategory is (Category)0)
                {
                    randomSizeClothes = (SizeClothing)SizeCloth.GetValue(random.Next(SizeCloth.Length));
                    value.sizeClothing = randomSizeClothes;
                    if (randomGender is (Gender)1 or (Gender)2)
                    {
                        randomClothes = (Clothing)Clothes.GetValue(random.Next(2, Clothes.Length));
                        value.gender = randomGender;
                        value.clothing = randomClothes;
                    }
                    else
                    {
                        randomClothes = (Clothing)Clothes.GetValue(random.Next(Clothes.Length));
                        value.gender = randomGender;
                        value.clothing = randomClothes;
                    }
                }
                else
                {
                    randomSizeShoes = (SizeShoes)SizeShoe.GetValue(random.Next(SizeShoe.Length));
                    value.sizeShoes = randomSizeShoes;
                    if (randomGender is (Gender)1 or (Gender)2 or (Gender)3)
                    {
                        randomShoes = (Shoes)Shoe.GetValue(random.Next(1, Shoe.Length));
                        value.gender = randomGender;
                        value.shoes = randomShoes;
                    }
                }
                Products.Add(value);
            }
        }

        private static void InitOrder()
        {
            for (int i = 1; i <= 20; i++)
            {
                Order value = new Order();
                value.ID = Config.getOrderSequenceID();
                value.CustomerName = "Order" + i;
                value.CustomerAddress = "Menachem Begin" + i;
                value.CustomerEmail = "Customer" + i + "@gmail.com";
                value.OrderDate = DateTime.MinValue;
                if (i <= 0.8 * 20)
                {
                    value.ShipDate = value.OrderDate + TimeSpan.FromDays((double)random.Next(4));
                    if (i <= 0.6 * 20)
                    {
                        value.DeliveryDate = value.ShipDate + TimeSpan.FromDays((double)random.Next(3));
                    }
                }
                Orders.Add(value);
            }
        }
        private static void InitOrderItem()
        {
            // ********************************אין לי שמץ של מושג מה הולך פה בכלל עשית לולאה חדשה******************************
            //for (int i = 1; i <= 40; )
            //{
            //    OrderItem value = new OrderItem();
            //    value.OrderID = random.Next(1, 100);
            //    while (!Orders.Exists(i => i.ID == value.OrderID) || OrderItems.Exists(i => i.OrderID == value.OrderID))
            //        value.OrderID = random.Next(1, 100);
            //    int numOfProducts = random.Next(1, 4);
            //    int j = numOfProducts;
            //    while (numOfProducts > 0)
            //    {
            //        value.ID = Config.orderItemSequenceID;
            //        value.Amount = random.Next(1, 6);
            //        value.ProductID = random.Next(100000, 1000000000);
            //        while (!Products.Exists(i => i.ID == value.ProductID))
            //            value.ProductID = random.Next(100000, 1000000000);
            //        value.Price = 100;
            //        OrderItems.Add(value);
            //        j++;
            //    }
            //    i += numOfProducts;
            //}

            for (int i = 0; i < 40; i++)
            {
                OrderItem orderItem = new OrderItem();
                Product product = new Product();

                product = Products[random.Next(0, 10)];// take some product
                orderItem.ID = Config.getOrderItemSequenceID();
                orderItem.ProductID = product.ID;
                orderItem.OrderID = Orders[random.Next(0, 10)].ID;
                orderItem.Amount = random.Next(1, 5);
                orderItem.Price = orderItem.Amount * product.Price;
                OrderItems.Add(orderItem);
            }
        }
    }
}