using Dal;
using DalApi;
using DO;
namespace DalTest
{
    class Program
    {
        /// <summary>
        /// Creating variables to get access to the CRUD
        /// </summary>
        private static IDal dalList = new DalList();
        static readonly Random random = new Random(); /// for the random numbers
        static void Main(string[] args)
        {
            Choice choice = new Choice();
            ///A loop that run as long as the requested value for exiting the main program was not received
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
                switch (choice)///choose option from the main menu:
                {
                    case Choice.Exit:
                        break;
                    case Choice.Product:
                        program.ProductSwitch();///jump to inner menu of product's actions
                        break;
                    case Choice.Order:
                        program.OrderSwitch();///jump to inner menu of order's actions
                        break;
                    case Choice.OrderItem:
                        program.OrderItemSwitch();///jump to inner menu of order item's actions
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
            OrderChoice choice2 = new OrderChoice();
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
                OrderChoice.TryParse(Console.ReadLine(), out choice2);
                switch (choice2)
                {
                    case OrderChoice.Exit: /// A case to exit the order back to the main
                        return;
                    case OrderChoice.Add: // Adding a product
                        creatProduct(ref theProduct);
                        Console.WriteLine("Enter the product ID:");
                        int id;
                        /// Conversion of the received value to the desired type
                        int.TryParse(Console.ReadLine(), out id);
                        theProduct.ID = id;
                        Console.WriteLine("The product ID is:");
                        try
                        {
                            Console.WriteLine(dalList.Product.Add(theProduct));
                        }
                        catch (Exception)
                        {
                            throw new ExistingObjectDo();
                        }
                        break;
                    case OrderChoice.Print: ///print the product with the recieved id
                        Console.WriteLine("Enter the product ID:");
                        int id1;
                        /// Conversion of the received value to the desired type
                        int.TryParse(Console.ReadLine(), out id1);
                        try
                        {
                            Console.WriteLine(dalList.Product.RequestByPredicate(product => product!.Value.ID == id1));
                        }
                        catch (Exception)
                        {
                            throw new NonFoundObjectDo();
                        }
                        break;
                    case OrderChoice.PrintList: ///printing the full product list
                        foreach (var pro in dalList.Product.RequestAllByPredicate())
                            Console.WriteLine(pro);
                        break;
                    case OrderChoice.Update: ///updating the product
                        Console.WriteLine("Enter the ID of the product that you want to update");
                        int id2;
                        /// Conversion of the received value to the desired type
                        int.TryParse(Console.ReadLine(), out id2);
                        try
                        {
                            Console.WriteLine(dalList.Product.RequestByPredicate(product => product!.Value.ID == id2));
                        }
                        catch (Exception)
                        {
                            throw new NonFoundObjectDo();
                        }
                        theProduct.ID = id2;
                        creatProduct(ref theProduct);
                        try
                        {
                            dalList.Product.Update(theProduct);
                        }
                        catch (Exception)
                        {
                            throw new NonFoundObjectDo();
                        }
                        break;
                    case OrderChoice.Delete: ///deleting the product according to the recieved id
                        Console.WriteLine("Enter the ID of the product that you wants to delete:");
                        int id3;
                        /// Conversion of the received value to the desired type
                        int.TryParse(Console.ReadLine(), out id3);
                        dalList.Product.Delete(id3);
                        break;
                    default:
                        break;
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
            TheProduct.Status = 0;
            Console.WriteLine("Enter the amount that in stock (1-4):");
            int inStock;
            /// Conversion of the received value to the desired type
            int.TryParse(Console.ReadLine(), out inStock);
            TheProduct.InStock = inStock;
            Console.WriteLine($@"Enter the Gender type:
0: Women
1: Men
2: Boys
3: Girls");
            Gender choice3 = new Gender();
            /// Conversion of the received value to the desired type
            Gender.TryParse(Console.ReadLine(), out choice3);
            TheProduct.Gender = choice3;
            Console.WriteLine($@"Enter the Category of the product:
0: Clothing
1: Shoes");
            Category choice4 = new Category();
            /// Conversion of the received value to the desired type
            Category.TryParse(Console.ReadLine(), out choice4);
            TheProduct.Category = choice4;
            if (choice4 is (Category)0)
            {
                Clothing c = new Clothing();
                string SkirtsAndDress = choice3 is Gender.Women or Gender.Girls ? @"
9: Skirts
10: Dresses" :
"";
                Console.WriteLine($@"Enter the Clothing type:
0: Blazers
1: Hoodies
2: Sweatshirts
3: Shirts
4: Socks
5: Pants
6: Cauts
7: Jackets
8: SportWear
{SkirtsAndDress}");
                /// Conversion of the received value to the desired type
                Clothing.TryParse(Console.ReadLine(), out c);
                TheProduct.Clothing = c;
                Console.WriteLine($@"Enter the size of the Clothing:
0: XS
1: S
2: M
3: L
4: XL");
                SizeClothing sc = new SizeClothing();
                /// Conversion of the received value to the desired type
                SizeClothing.TryParse(Console.ReadLine(), out sc);
                TheProduct.SizeClothing = sc;
            }
            else
            {
                Shoes s = new Shoes();
                string Heels = choice3 is Gender.Women ? @"
4: Heels" :
"";
                Console.WriteLine($@"Enter the Shoe's type:
0: Sneakers
1: Boots
2: Sport
3: Sandals
{Heels}");
                /// Conversion of the received value to the desired type
                Shoes.TryParse(Console.ReadLine(), out s);
                TheProduct.Shoes = s;
                Console.WriteLine($@"Enter the Shoes type:
36
37
38
39
40
41");
                SizeShoes ss;
                /// Conversion of the received value to the desired type
                SizeShoes.TryParse(Console.ReadLine(), out ss);
                TheProduct.SizeShoes = ss;
            }
            Console.WriteLine($@"Enter the Color of the product:
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
            TheProduct.Color = cl;
            Console.WriteLine("Enter the description of the product:");
            string str = Console.ReadLine()!;
            TheProduct.Description = str;
        }

        void OrderSwitch()
        {
            Order theOrder = new Order();
            OrderChoice choice2 = new OrderChoice();
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
                OrderChoice.TryParse(Console.ReadLine(), out choice2);
                switch (choice2)
                {
                    case OrderChoice.Exit:/// A case to exit the order back to the main
                        return;
                    case OrderChoice.Add: ///add a new order item
                        creatOrder(ref theOrder);
                        Console.WriteLine("The order ID is:");
                        Console.WriteLine(dalList.Order.Add(theOrder));
                        break;
                    case OrderChoice.Print: ///print the order with the recieved id
                        Console.WriteLine("Enter the ordr ID:");
                        int id;
                        /// Conversion of the received value to the desired type
                        int.TryParse(Console.ReadLine(), out id);
                        try
                        {
                            Console.WriteLine(dalList.Order.RequestByPredicate(order => order!.Value.ID == id));
                        }
                        catch (Exception)
                        {
                            throw new NonFoundObjectDo();
                        }
                        break;
                    case OrderChoice.PrintList: ///printing the full order list
                        foreach (var or in dalList.Order.RequestAllByPredicate())
                            Console.WriteLine(or);
                        break;
                    case OrderChoice.Update: ///updating the order
                        Console.WriteLine("Enter the ID of the order that you want to update");
                        int id1;
                        /// Conversion of the received value to the desired type
                        int.TryParse(Console.ReadLine(), out id1);
                        try
                        {
                            Console.WriteLine(dalList.Order.RequestByPredicate(order => order!.Value.ID == id1));
                        }
                        catch (Exception)
                        {
                            throw new NonFoundObjectDo();
                        }
                        theOrder.ID = id1;
                        creatOrder(ref theOrder);
                        try
                        {
                            dalList.Order.Update(theOrder);
                        }
                        catch (Exception)
                        {
                            throw new NonFoundObjectDo();
                        }
                        break;
                    case OrderChoice.Delete: ///deleting the order according to the recieved id
                        Console.WriteLine("Enter the ID of the order that you wants to delete:");
                        int id2;
                        /// Conversion of the received value to the desired type
                        int.TryParse(Console.ReadLine(), out id2);
                        dalList.Order.Delete(id2);
                        break;
                    default:
                        break;
                }
            } while (choice2 != 0);
        }

        void creatOrder(ref Order TheOrder)
        {
            Console.WriteLine("Enter the order details:");
            Console.WriteLine("Enter the customer name:");
            string Name = Console.ReadLine()!;
            TheOrder.CustomerName = Name;
            Console.WriteLine("Enter the customer email:");
            string email = Console.ReadLine()!;
            TheOrder.CustomerEmail = email;
            Console.WriteLine("Enter the customer adress:");
            string adress = Console.ReadLine()!;
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
        /// <summary>
        /// A function within which all the user's choices regarding the order items list are handled
        /// </summary>
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
                switch (choice3)
                {
                    case Choice3.Exit:///exiting from the order item function
                        return;
                    case Choice3.Add:///add a new order item
                        creatOrderItem(ref theOrderItem);
                        Console.WriteLine("The order item ID is:");
                        Console.WriteLine(dalList.OrderItem.Add(theOrderItem));
                        break;
                    case Choice3.PrintByID:///print the order item with the recieved id
                        Console.WriteLine("Enter the ordr item ID:");
                        int id;
                        /// Conversion of the received value to the desired type
                        int.TryParse(Console.ReadLine(), out id);
                        try
                        {
                            Console.WriteLine(dalList.OrderItem.RequestByPredicate(orderItem => orderItem!.Value.ID == id));
                        }
                        catch (Exception)
                        {
                            throw new NonFoundObjectDo();
                        }
                        break;
                    case Choice3.PrintByOrderAndProductID:///print the order item according to the recieved two ids
                        Console.WriteLine("Enter the order ID:");
                        int orID;
                        /// Conversion of the received value to the desired type
                        int.TryParse(Console.ReadLine(), out orID);
                        Console.WriteLine("Enter the product ID:");
                        int proID;
                        /// Conversion of the received value to the desired type
                        int.TryParse(Console.ReadLine(), out proID);
                        try
                        {
                            Console.WriteLine(dalList.OrderItem.RequestByOrderAndProductID(orID, proID));
                        }
                        catch (Exception)
                        {
                            throw new NonFoundObjectDo();
                        }
                        break;
                    case Choice3.PrintList:///printing the full order item list
                        foreach (var orIt in dalList.OrderItem.RequestAllByPredicate())
                            Console.WriteLine(orIt);
                        break;
                    case Choice3.PrintByOrderID:///printing according to the order items id
                        Console.WriteLine("Enter the order ID:");
                        int orderID;
                        /// Conversion of the received value to the desired type
                        int.TryParse(Console.ReadLine(), out orderID);
                        try
                        {
                            var orderItems = dalList.OrderItem.RequestAllByPredicate(orderItem => orderItem?.OrderID == orderID);
                            foreach (var orIt in orderItems)
                                Console.WriteLine(orIt);
                        }
                        catch (Exception)
                        {
                            throw new NonFoundObjectDo();
                        }
                        break;
                    case Choice3.Update:///updating the order item
                        Console.WriteLine("Enter the ID of the order item that you want to update");
                        int id1;
                        /// Conversion of the received value to the desired type
                        int.TryParse(Console.ReadLine(), out id1);
                        try
                        {
                            Console.WriteLine(dalList.OrderItem.RequestByPredicate(orderItem => orderItem!.Value.ID == id1));
                        }
                        catch (Exception)
                        {
                            throw new NonFoundObjectDo();
                        }
                        theOrderItem.ID = id1;
                        creatOrderItem(ref theOrderItem);
                        try
                        {
                            dalList.OrderItem.Update(theOrderItem);
                        }
                        catch (Exception)
                        {
                            throw new NonFoundObjectDo();
                        }
                        break;
                    case Choice3.Delete:///deleting the order item according to the recieved id
                        Console.WriteLine("Enter the ID of the order item that you wants to delete:");
                        int id2;
                        /// Conversion of the received value to the desired type
                        int.TryParse(Console.ReadLine(), out id2);
                        dalList.OrderItem.Delete(id2);
                        break;
                    default:
                        break;
                }
            } while (choice3 != 0);
        }
        /// <summary>
        /// A function that creats an orderItem
        /// </summary>
        /// <param name="TheOrderItem"></param>
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
