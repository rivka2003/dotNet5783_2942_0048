using System;
using System.Collections.Specialized;
using Dal;
using DO;
namespace DalTest
{
    class Program
    {
        /// <summary>
        /// Creating variables to get access to the CRUD
        /// </summary>
        private static DalOrder order = new DalOrder();
        private static DalOrderItem orderItem = new DalOrderItem();
        private static DalProduct product = new DalProduct();
        static readonly Random random = new Random(); /// for the random numbers

        static void Main(string[] args)
        {
            Choice choice = new Choice();
            ///A loop that ran as long as the requested value for exiting the main program was not received
            do
            {
                Program program = new Program();
                Console.WriteLine($@"Enter your Choice:
0: Exit
1: Product
2: Order
3: OrderItem");
                /// Conversion of the received value to the desired type
                Choice.TryParse(Console.ReadLine(), out choice);
                switch (choice)
                {
                    case Choice.Exit:
                        break;
                    case Choice.Product:
                        program.ProductSwitch();
                        break;
                    case Choice.Order:
                        program.OrderSwitch();
                        break;
                    case Choice.OrderItem:
                        program.OrderItemSwitch();
                        break;
                    default:
                        break;
                }
            } while (choice != 0);

        }
        /// <summary>
        /// A function within which all the user's choices regarding the product list are handled
        /// </summary>
        void ProductSwitch()
        {
            Product theProduct = new Product();
            Choice2 choice2 = new Choice2();
            do
            {
                Console.WriteLine($@"Enter your Choice:
0: Exit
1: Add
2: Print
3: Print the List
4: Updat
5: Delete");
                /// Conversion of the received value to the desired type
                Choice2.TryParse(Console.ReadLine(), out choice2);
                try
                {
                    switch (choice2)
                    {
                        case Choice2.Exit:
                            return;
                        case Choice2.Add: // Adding a product
                            creatProduct(ref theProduct);
                            Console.WriteLine("Enter the product ID:");
                            int id;
                            /// Conversion of the received value to the desired type
                            int.TryParse(Console.ReadLine(), out id);
                            theProduct.ID = id;
                            Console.WriteLine("The product ID is:");
                            Console.WriteLine(product.Create(theProduct));
                            break;
                        case Choice2.Print:
                            Console.WriteLine("Enter the product ID:");
                            int id1;
                            /// Conversion of the received value to the desired type
                            int.TryParse(Console.ReadLine(), out id1);
                            Console.WriteLine(product.RequestById(id1));
                            break;
                        case Choice2.PrintList:
                            foreach (var pro in product.RequestAll())
                                Console.WriteLine(pro);
                            break;
                        case Choice2.Update:
                            Console.WriteLine("Enter the ID of the product that you want to update");
                            int id2;
                            /// Conversion of the received value to the desired type
                            int.TryParse(Console.ReadLine(), out id2);
                            Console.WriteLine(product.RequestById(id2));
                            theProduct.ID = id2;
                            creatProduct(ref theProduct);
                            product.Update(theProduct);
                            break;
                        case Choice2.Delete:
                            Console.WriteLine("Enter the ID of the product that you wants to delete:");
                            int id3;
                            /// Conversion of the received value to the desired type
                            int.TryParse(Console.ReadLine(), out id3);
                            product.Delete(id3);
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            } while (choice2 != 0);

        }

        void creatProduct(ref Product TheProduct)
        {
            Console.WriteLine("Enter the product details:");
            Console.WriteLine("Enter the name of the product: ");
            TheProduct.Name = Console.ReadLine();
            Console.WriteLine("Enter the price of the product");
            double price;
            /// Conversion of the received value to the desired type
            double.TryParse(Console.ReadLine(), out price);
            TheProduct.Price = price;
            TheProduct.status = 0;
            Console.WriteLine("Enter the amount that in stock (1-4):");
            int inStock;
            /// Conversion of the received value to the desired type
            int.TryParse(Console.ReadLine(), out inStock);
            TheProduct.InStock = inStock;
            Console.WriteLine($@"Enter the gender type:
0: Women
1: Men
2: Boys
3: Girls");
            Gender choice3 = new Gender();
            /// Conversion of the received value to the desired type
            Gender.TryParse(Console.ReadLine(), out choice3);
            TheProduct.gender = choice3;
            Console.WriteLine($@"Enter the Category of the product:
0: Clothing
1: Shoes");
            Category choice4 = new Category();
            /// Conversion of the received value to the desired type
            Category.TryParse(Console.ReadLine(), out choice4);
            TheProduct.category = choice4;
            if (choice4 is (Category)0)
            {
                Clothing c = new Clothing();
                if (!(choice3 is (Gender)1 or (Gender)2))
                {
                    Console.WriteLine($@"Enter the clothing type:
0: Skirts
1: Dresses
2: Blazers
3: Hoodies
4: Sweatshirts
5: Shirts
6: Socks
7: Pants
8: Cauts
9: Jackets
10: SportWear");
                }
                else
                {
                    Console.WriteLine($@"Enter the clothing type:
2: Blazers
3: Hoodies
4: Sweatshirts
5: Shirts
6: Socks
7: Pants
8: Cauts
9: Jackets
10: SportWear");
                }
                /// Conversion of the received value to the desired type
                Clothing.TryParse(Console.ReadLine(), out c);
                TheProduct.clothing = c;
                Console.WriteLine($@"Enter the size of the clothing:
0: XS
1: S
2: M
3: L
4: XL");
                SizeClothing sc = new SizeClothing();
                /// Conversion of the received value to the desired type
                SizeClothing.TryParse(Console.ReadLine(), out sc);
                TheProduct.sizeClothing = sc;
            }
            else
            {
                Shoes s = new Shoes();
                if (choice3 is (Gender)0)
                {
                    Console.WriteLine($@"Enter the Shoe's type:
0: Heels
1: Sneakers
2: Boots
3: Sport
4: Sandals");
                }
                else
                {
                    Console.WriteLine($@"Enter the Shoe's type:
1: Sneakers
2: Boots
3: Sport
4: Sandals");
                }
                /// Conversion of the received value to the desired type
                Shoes.TryParse(Console.ReadLine(), out s);
                TheProduct.shoes = s;
                Console.WriteLine($@"Enter the shoes type:
36
37
38
39
40
41");
                SizeShoes ss;
                /// Conversion of the received value to the desired type
                SizeShoes.TryParse(Console.ReadLine(), out ss);
                TheProduct.sizeShoes = ss;
            }
            Console.WriteLine($@"Enter the color of the product:
0: Black
1: White
2: Green
3: Yellow
4: Red
5: Blue
6: Beige
7: Brown
8: Jeans
9: Orange
10: Pink
11: Purple
12: Gray
13: Gold
14: Silver
15: Colored");
            Color cl = new Color();
            /// Conversion of the received value to the desired type
            Color.TryParse(Console.ReadLine(), out cl);
            TheProduct.color = cl;
            Console.WriteLine("Enter the description of the product:");
            string str = Console.ReadLine();
            TheProduct.Description = str;
        }

        void OrderSwitch()
        {
            Order theOrder = new Order();
            Choice2 choice2 = new Choice2();
            do
            {
                Console.WriteLine($@"Enter your Choice:
0: Exit
1: Add
2: Print
3: Print the List
4: Updat
5: Delete");
                /// Conversion of the received value to the desired type
                Choice2.TryParse(Console.ReadLine(), out choice2);
                try
                {
                    switch (choice2)
                    {
                        case Choice2.Exit:
                            return;
                        case Choice2.Add:
                            creatOrder(ref theOrder);
                            Console.WriteLine("The order ID is:");
                            Console.WriteLine(order.Create(theOrder));
                            break;
                        case Choice2.Print:
                            Console.WriteLine("Enter the ordr ID:");
                            int id;
                            /// Conversion of the received value to the desired type
                            int.TryParse(Console.ReadLine(), out id);
                            Console.WriteLine(order.RequestById(id));
                            break;
                        case Choice2.PrintList:
                            foreach (var or in order.RequestAll())
                                Console.WriteLine(or);
                            break;
                        case Choice2.Update:
                            Console.WriteLine("Enter the ID of the order that you want to update");
                            int id1;
                            /// Conversion of the received value to the desired type
                            int.TryParse(Console.ReadLine(), out id1);
                            Console.WriteLine(order.RequestById(id1));
                            theOrder.ID = id1;
                            creatOrder(ref theOrder);
                            order.Update(theOrder);
                            break;
                        case Choice2.Delete:
                            Console.WriteLine("Enter the ID of the order that you wants to delete:");
                            int id2;
                            /// Conversion of the received value to the desired type
                            int.TryParse(Console.ReadLine(), out id2);
                            order.Delete(id2);
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            } while (choice2 != 0);
        }

        void creatOrder(ref Order TheOrder)
        {
            Console.WriteLine("Enter the order details:");
            Console.WriteLine("Enter the customer name:");
            string Name = Console.ReadLine();
            TheOrder.CustomerName = Name;
            Console.WriteLine("Enter the customer email:");
            string email = Console.ReadLine();
            TheOrder.CustomerEmail = email;
            Console.WriteLine("Enter the customer adress:");
            string adress = Console.ReadLine();
            TheOrder.CustomerAddress = adress;
            Console.WriteLine("Enter the order date:");
            DateTime orderDate = new DateTime();
            /// Conversion of the received value to the desired type
            DateTime.TryParse(Console.ReadLine(), out orderDate);
            TheOrder.OrderDate = orderDate;
            Console.WriteLine("Enter the shipping date:");
            DateTime shippingDate = new DateTime();
            /// Conversion of the received value to the desired type
            DateTime.TryParse(Console.ReadLine(), out shippingDate);
            TheOrder.ShipDate = shippingDate;
            Console.WriteLine("Enter the delivery date:");
            DateTime deliveryDate = new DateTime();
            /// Conversion of the received value to the desired type
            DateTime.TryParse(Console.ReadLine(), out deliveryDate);
            TheOrder.DeliveryDate = deliveryDate;
        }
        void OrderItemSwitch()
        {
            OrderItem theOrderItem = new OrderItem();
            Choice3 choice3 = new Choice3();
            do
            {
                Console.WriteLine($@"Enter your Choice:
0: Exit
1: Add
2: Print by id
3: Print by order and product ID
4: Print the List
5: Print the List by order ID
6: Updat
7: Delete");
                Choice3.TryParse(Console.ReadLine(), out choice3);
                try
                {
                    switch (choice3)
                    {
                        case Choice3.Exit:
                            return;
                        case Choice3.Add:
                            creatOrderItem(ref theOrderItem);
                            Console.WriteLine("The order item ID is:");
                            Console.WriteLine(orderItem.Create(theOrderItem));
                            break;
                        case Choice3.PrintByID:
                            Console.WriteLine("Enter the ordr item ID:");
                            int id;
                            /// Conversion of the received value to the desired type
                            int.TryParse(Console.ReadLine(), out id);
                            Console.WriteLine(orderItem.RequestById(id));
                            break;
                        case Choice3.PrintByOrderAndProductID:
                            Console.WriteLine("Enter the order ID:");
                            int orID;
                            /// Conversion of the received value to the desired type
                            int.TryParse(Console.ReadLine(), out orID);
                            Console.WriteLine("Enter the product ID:");
                            int proID;
                            /// Conversion of the received value to the desired type
                            int.TryParse(Console.ReadLine(), out proID);
                            Console.WriteLine(orderItem.RequestByOrderAndProductID(orID, proID));
                            break;
                        case Choice3.PrintList:
                            foreach (var orIt in orderItem.RequestAll())
                                Console.WriteLine(orIt);
                            break;
                        case Choice3.PrintByOrderID:
                            Console.WriteLine("Enter the order ID:");
                            int orderID;
                            /// Conversion of the received value to the desired type
                            int.TryParse(Console.ReadLine(), out orderID);
                            foreach (var orIt in orderItem.RequestAllByOrderID(orderID))
                                Console.WriteLine(orIt);
                            break;
                        case Choice3.Update:
                            Console.WriteLine("Enter the ID of the order item that you want to update");
                            int id1;
                            /// Conversion of the received value to the desired type
                            int.TryParse(Console.ReadLine(), out id1);
                            Console.WriteLine(orderItem.RequestById(id1));
                            theOrderItem.ID = id1;
                            creatOrderItem(ref theOrderItem);
                            orderItem.Update(theOrderItem);
                            break;
                        case Choice3.Delete:
                            Console.WriteLine("Enter the ID of the order item that you wants to delete:");
                            int id2;
                            /// Conversion of the received value to the desired type
                            int.TryParse(Console.ReadLine(), out id2);
                            orderItem.Delete(id2);
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            } while (choice3 != 0);
        }
        void creatOrderItem(ref OrderItem TheOrderItem)
        {
            Console.WriteLine("Enter the order item details:");
            Console.WriteLine("Enter the product ID:");
            int productID;
            /// Conversion of the received value to the desired type
            int.TryParse(Console.ReadLine(), out productID);
            TheOrderItem.ProductID = productID;
            Console.WriteLine("Enter the order ID:");
            int orderID;
            /// Conversion of the received value to the desired type
            int.TryParse(Console.ReadLine(), out orderID);
            TheOrderItem.OrderID = orderID;
            Console.WriteLine("Enter the price of the order item:");
            int price;
            /// Conversion of the received value to the desired type
            int.TryParse(Console.ReadLine(), out price);
            TheOrderItem.Price = price;
            Console.WriteLine("Enter the amount of the product:");
            int amount;
            /// Conversion of the received value to the desired type
            int.TryParse(Console.ReadLine(), out amount);
            TheOrderItem.Amount = amount;
        }
    }
}
