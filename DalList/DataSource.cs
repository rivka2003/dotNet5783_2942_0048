﻿using DO;
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
            /// <summary>
            /// The running number of the order item
            /// </summary>
            private static int orderItemSequenceID = 1;
            /// <summary>
            /// The running number of the order
            /// </summary>
            private static int orderSequenceID = 1;
            internal static int getOrderItemSequenceID() { return orderItemSequenceID++; }
            internal static int getOrderSequenceID() { return orderSequenceID++; }

        }
        /// <summary>
        /// Our three lists
        /// </summary>
        internal static List<Product> Products = new List<Product>();
        internal static List<Order> Orders = new List<Order>();
        internal static List<OrderItem> OrderItems = new List<OrderItem>();
        static readonly Random random = new Random(); /// for the random numbers
        static DataSource() /// constructor
        {
            s_Initialize();
        }
        /// <summary>
        /// A function that calls initialization functions
        /// </summary>
        private static void s_Initialize()
        {
            InitProduct();
            InitOrder();
            InitOrderItem();
        }
        /// <summary>
        /// A method to initialize the list of items
        /// </summary>
        private static void InitProduct()
        {
            ///Building arrays that receive the contents of all enums
            Array Categories = Enum.GetValues(typeof(Category)), Clothes = Enum.GetValues(typeof(Clothing)), Shoe = Enum.GetValues(typeof(Shoes));
            Array Colors = Enum.GetValues(typeof(Color)), Genders = Enum.GetValues(typeof(Gender));
            Array SizeShoe = Enum.GetValues(typeof(SizeShoes)), SizeCloth = Enum.GetValues(typeof(SizeClothing));
            ///Creating a variable from all the types of the enums to get a value drawn from each enum
            Category randomCategory = new Category(); Clothing randomClothes = new Clothing();
            Shoes randomShoes = new Shoes(); Color randomColor = new Color();
            Gender randomGender = new Gender(); SizeClothing randomSizeClothes = new SizeClothing();
            SizeShoes randomSizeShoes = new SizeShoes();
            ///A loop that goes through each element in the list
            for (int i = 1; i <= 10; i++)
            {
                Product value = new Product();
                randomCategory = (Category)Categories.GetValue(random.Next(Categories.Length));
                randomGender = (Gender)Genders.GetValue(random.Next(Genders.Length));
                randomColor = (Color)Colors.GetValue(random.Next(Colors.Length));
                value.category = randomCategory;
                value.ID = random.Next(100000, 999999);
                /// A loop that says that as long as the id exists in the list, it is necessary to generate a new id
                while (Products.Exists(i => i.ID == value.ID))
                    value.ID = random.Next(100000, 999999);
                /// 5% of the products will be empty and there will be no stock of them
                if (i <= 0.05 * 10)
                {
                    value.InStock = 0;
                    value.status = (Status)1;
                }
                else
                {
                    value.InStock = random.Next(1, 4);
                    value.status = 0;
                }
                value.Name = "Product" + i;
                value.Price = 100;
                value.color = randomColor;
                value.Description = "New!";
                ///If the drawn category is "Clothing"
                if (randomCategory is (Category)0)
                {
                    randomSizeClothes = (SizeClothing)SizeCloth.GetValue(random.Next(SizeCloth.Length));
                    value.sizeClothing = randomSizeClothes;
                    /// If the category drawn is "men" or "boys"
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
                ///If the category is "shoes"
                else
                {
                    randomSizeShoes = (SizeShoes)SizeShoe.GetValue(random.Next(SizeShoe.Length));
                    value.sizeShoes = randomSizeShoes;
                    /// If the category is "Men" or "Boys" or "Girls"
                    if (randomGender is (Gender)1 or (Gender)2 or (Gender)3)
                    {
                        randomShoes = (Shoes)Shoe.GetValue(random.Next(1, Shoe.Length));
                        value.gender = randomGender;
                        value.shoes = randomShoes;
                    }
                    else
                    {
                        randomShoes = (Shoes)Shoe.GetValue(random.Next(Shoe.Length));
                        value.gender = randomGender;
                        value.shoes = randomShoes;
                    }
                }
                Products.Add(value);
            }
        }
        /// <summary>
        /// A function to initialize the list of orders
        /// </summary>
        private static void InitOrder()
        {
            ///A loop that goes through all the places in the list
            for (int i = 1; i <= 20; i++)
            {
                Order value = new Order();
                value.ID = Config.getOrderSequenceID();
                value.CustomerName = "Order" + i;
                value.CustomerAddress = "Menachem Begin" + i;
                value.CustomerEmail = "Customer" + i + "@gmail.com";
                value.OrderDate = DateTime.Now + new TimeSpan(random.Next(1, 24), random.Next(1, 60), random.Next(0, 60));
                /// If the order is within the 80% that were shipped
                if (i <= 0.8 * 20)
                {
                    value.ShipDate = value.OrderDate - TimeSpan.FromDays(random.Next(2, 4));
                    /// If the order is within the 60% that reached the orderer
                    if (i <= 0.6 * 20)
                    {
                        value.DeliveryDate = value.ShipDate - TimeSpan.FromDays(random.Next(2, 4));
                    }
                    else
                    {
                        value.DeliveryDate = DateTime.MinValue;
                    }
                }
                else
                {
                    value.ShipDate = DateTime.MinValue;
                    value.DeliveryDate = DateTime.MinValue;
                }
                Orders.Add(value);
            }
        }
        /// <summary>
        /// Function to initialize the list of order items
        /// </summary>
        private static void InitOrderItem()
        {
            /// A loop that goes through all the places in the list
            for (int i = 0; i < 40;)
            {
                OrderItem orderItem = new OrderItem();

                orderItem.OrderID = Orders[random.Next(0,20)].ID;
                /// If this order number exists within the list of order items then generate a new order number
                while (OrderItems.Exists(i => i.OrderID == orderItem.OrderID))
                {
                    orderItem.OrderID = Orders[random.Next(0, 20)].ID;
                }
                /// The amount of products in each order
                int amountOfProducts = random.Next(1, 4);
                /// A loop that creates in each iteration a new product for the same order number
                for (int j = 0; j < amountOfProducts; j++)
                {
                    Product product = new Product();
                    product = Products[random.Next(0, 10)];// take some product
                    orderItem.ID = Config.getOrderItemSequenceID();
                    orderItem.ProductID = product.ID;
                    orderItem.Amount = random.Next(1, 6);
                    orderItem.Price = orderItem.Amount * product.Price;
                    OrderItems.Add(orderItem);
                }
                /// Subtracting the amount of products added to the list of order items from i
                i += amountOfProducts;
            }
        }
    }
}