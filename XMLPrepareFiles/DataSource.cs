using DO;

namespace XMLPrepareFiles
{
    public static class DataSource
    {
        /// <summary>
        /// Making the Config class to make a run number
        /// </summary>
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

        /// <summary>
        /// Our three lists
        /// </summary>
        internal static List<Product?> Products { set; get; } = new List<Product?>();
        internal static List<Order?> Orders { set; get; } = new List<Order?>();
        internal static List<OrderItem?> OrderItems { set; get; } = new List<OrderItem?>();

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
            ///
            for (int i = 1; i <= 10; i++)
            {
                Product value = new Product();
                randomCategory = (Category)Categories.GetValue(random.Next(Categories.Length))!;
                randomGender = (Gender)Genders.GetValue(random.Next(Genders.Length))!;
                randomColor = (Color)Colors.GetValue(random.Next(Colors.Length))!;
                value.Category = randomCategory;
                value.ID = random.Next(100000, 999999);

                /// A loop that says that as long as the id exists in the list, it is necessary to generate a new id
                while (Products.Exists(i => i?.ID == value.ID))
                    value.ID = random.Next(100000, 999999);

                /// 5% of the products will be empty and there will be no stock of them
                if (i <= 0.05 * 10)
                {
                    value.InStock = 0;
                    value.Status = Status.NotExist;
                }
                else
                {
                    value.InStock = random.Next(1, 4);
                    value.Status = Status.Exist;
                }

                value.Name = "Product" + i;
                value.Price = 100;
                value.Color = randomColor;
                value.Description = "New!";
                value.Imege = "/Image2.png";

                ///If the drawn Category is "Clothing"
                if (randomCategory is Category.Clothing)
                {
                    randomSizeClothes = (SizeClothing)SizeCloth.GetValue(random.Next(SizeCloth.Length))!;
                    value.SizeClothing = randomSizeClothes;
                    /// If the Category drawn is "men" or "boys"
                    if (randomGender is Gender.Men or Gender.Boys)
                    {
                        randomClothes = (Clothing)Clothes.GetValue(random.Next(Clothes.Length - 2))!;
                        value.Gender = randomGender;
                        value.Clothing = randomClothes;
                    }
                    else
                    {
                        randomClothes = (Clothing)Clothes.GetValue(random.Next(Clothes.Length))!;
                        value.Gender = randomGender;
                        value.Clothing = randomClothes;
                    }
                }
                ///If the Category is "Shoes"
                else
                {
                    randomSizeShoes = (SizeShoes)SizeShoe.GetValue(random.Next(SizeShoe.Length))!;
                    value.SizeShoes = randomSizeShoes;
                    /// If the Category is "Men" or "Boys" or "Girls"
                    if (randomGender is not Gender.Women)
                    {
                        randomShoes = (Shoes)Shoe.GetValue(random.Next(Shoe.Length - 1))!;
                        value.Gender = randomGender;
                        value.Shoes = randomShoes;
                    }
                    else
                    {
                        randomShoes = (Shoes)Shoe.GetValue(random.Next(Shoe.Length))!;
                        value.Gender = randomGender;
                        value.Shoes = randomShoes;
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
                Order value = new Order()
                {
                    ID = getOrderSequenceID(),
                    CustomerName = "Order" + i,
                    CustomerAddress = "Menachem Begin" + i,
                    CustomerEmail = "Customer" + i + "@gmail.com",
                    OrderDate = DateTime.Now + new TimeSpan(random.Next(1, 24), random.Next(1, 60), random.Next(0, 60))
                };

                /// If the order is within the 80% that were shipped
                if (i <= 0.8 * 20)
                {
                    value.ShipDate = value.OrderDate + TimeSpan.FromDays(random.Next(2, 4));

                    /// If the order is within the 60% that reached the orderer
                    if (i <= 0.6 * 20)
                    {
                        value.DeliveryDate = value.ShipDate + TimeSpan.FromDays(random.Next(2, 4));
                    }
                    else
                    {
                        value.DeliveryDate = null;
                    }
                }
                else
                {
                    value.ShipDate = null;
                    value.DeliveryDate = null;
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
            for (int i = 0; i < 40; i++)
            {
                OrderItem orderItem = new OrderItem();

                // all the first 20 orders have at list one product
                if (i < 20)
                    orderItem.OrderID = Orders[i]?.ID ?? 0;
                else /// If this order number exists within the list of order items then generate a new order number
                    orderItem.OrderID = Orders[random.Next(0, 20)]?.ID ?? 0;

                // every order that has less than 5 products
                if (OrderItems.FindAll(item => item?.OrderID == orderItem.OrderID).Count() < 5)
                {
                    Product product = new Product();
                    product = (Product)Products[random.Next(0, 10)]!;// take some product
                    orderItem.ID = getOrderItemSequenceID();
                    orderItem.ProductID = product.ID;
                    orderItem.Amount = random.Next(1, 6);
                    orderItem.Price = orderItem.Amount * product.Price;
                    OrderItems.Add(orderItem);
                }
                else
                    i--;
            }
        }
    }
}
