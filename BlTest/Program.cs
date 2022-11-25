using BlApi;
using BlImplementation;
using BO;
using CopyPropertisTo;
using Dal;
using DO;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace BlTest
{
    internal class Program
    {
        private static IBl blApi = new Bl();
        private static BO.Cart cart = new BO.Cart();
        static void Main(string[] args)
        {
            BO.Choice choice = new BO.Choice();
            ///A loop that run as long as the requested value for exiting the main program was not received
            do
            {
                Program program = new Program();
                Console.WriteLine($@"Enter your Choice:
0: Exit
1: Product
2: Order
3: Cart");
                /// Conversion of the received value to the desired type
                BO.Choice.TryParse(Console.ReadLine(), out choice);
                switch (choice)///choose option from the main menu:
                {
                    case BO.Choice.Exit:
                        break;
                    case BO.Choice.Product:
                        program.ProductSwitch();///jump to inner menu of product's actions
                        break;
                    case BO.Choice.Order:
                        program.OrderSwitch();///jump to inner menu of order's actions
                        break;
                    case BO.Choice.Cart:
                        program.CartSwitch();///jump to inner menu of cart's actions
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
            BO.Product theProduct = new BO.Product();
            BO.ProductChoice productChoice = new BO.ProductChoice();
            do
            {
                Console.WriteLine($@"Enter your Choice:
0: Exit
1: Add
2: Print details for customer
3: Print details for manager
3: Print the List
4: Updat
5: Delete");
                /// Conversion of the received value to the desired type
                BO.ProductChoice.TryParse(Console.ReadLine(), out productChoice);
                switch (productChoice)
                {
                    case BO.ProductChoice.Exit: /// A case to exit the order back to the main
                        return;
                    case BO.ProductChoice.Add: // Adding a product
                        creatProduct(ref theProduct);
                        Console.WriteLine("Enter the product ID:");
                        int id;
                        /// Conversion of the received value to the desired type
                        int.TryParse(Console.ReadLine(), out id);
                        theProduct.ID = id;
                        Console.WriteLine("The product ID is:");
                        try
                        { blApi.Product.AddProduct(theProduct); }
                        catch (BO.NonFoundObjectBo)
                        { }
                        catch (BO.ExistingObjectBo)
                        { }
                        finally { Console.WriteLine("Problem found!"); }
                        Console.WriteLine(theProduct.ID);
                        break;
                    case BO.ProductChoice.PrintDaetailsForCustomer: ///print the product details for the customer  with the recieved id and cart object
                        Console.WriteLine("Enter the product ID:");
                        int id1;
                        /// Conversion of the received value to the desired type
                        int.TryParse(Console.ReadLine(), out id1);
                        try
                        { Console.WriteLine(blApi.Product.ProductDetailsForCustomer(id1, cart)); }
                        catch (BO.NonFoundObjectBo)
                        { }
                        finally { Console.WriteLine("Problem found!"); }
                        break;
                    case BO.ProductChoice.PrintDetailsForManagaer: // print the product details for manager with the recieved id
                        Console.WriteLine("Enter the product ID:");
                        int id2;
                        /// Conversion of the received value to the desired type
                        int.TryParse(Console.ReadLine(), out id2);
                        try
                        { Console.WriteLine(blApi.Product.ProductDetailsForManager(id2)); }
                        catch (BO.NonFoundObjectBo)
                        { }
                        finally { Console.WriteLine("Problem found!"); }
                        break;
                    case BO.ProductChoice.PrintList: ///printing the full product list
                        foreach (var pro in blApi.Product.GetAll())
                            Console.WriteLine(pro);
                        break;
                    case BO.ProductChoice.Updat: ///updating the product
                        Console.WriteLine("Enter the ID of the product that you want to update");
                        int id3;
                        /// Conversion of the received value to the desired type
                        int.TryParse(Console.ReadLine(), out id3);
                        try
                        { Console.WriteLine(blApi.Product.ProductDetailsForManager(id3)); }
                        catch (BO.NotInStock)
                        { }
                        finally { Console.WriteLine("Problem found!"); }
                        theProduct.ID = id3;
                        creatProduct(ref theProduct);
                        try
                        {
                            Console.WriteLine("The updated product is:");
                            Console.WriteLine(blApi.Product.UpdateProduct(theProduct));
                        }
                        catch (BO.NonFoundObjectBo)
                        { }
                        catch (BO.NotValid)
                        { }
                        finally { Console.WriteLine("Problem found!"); }
                        break;
                    case BO.ProductChoice.Delete: ///deleting the product according to the recieved id
                        Console.WriteLine("Enter the ID of the product that you wants to delete:");
                        int id4;
                        /// Conversion of the received value to the desired type
                        int.TryParse(Console.ReadLine(), out id4);
                        try
                        { blApi.Product.DeleteProduct(id4); }
                        catch (BO.NonFoundObjectBo)
                        { }
                        finally { Console.WriteLine("Problem found!"); }
                        break;
                    default:
                        break;
                }
            } while (productChoice != 0);

        }

        void creatProduct(ref BO.Product TheProduct)
        {
            Console.WriteLine("Enter the product details:");
            Console.WriteLine("Enter the name of the product: ");
            TheProduct.Name = Console.ReadLine();
            Console.WriteLine("Enter the price of the product");
            double price;
            /// Conversion of the received value to the desired type
            double.TryParse(Console.ReadLine(), out price);
            TheProduct.Price = price;
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
            BO.Gender g = new BO.Gender();
            /// Conversion of the received value to the desired type
            BO.Gender.TryParse(Console.ReadLine(), out g);
            TheProduct.Gender = g;
            Console.WriteLine($@"Enter the Category of the product:
0: Clothing
1: Shoes");
            BO.Category cat = new BO.Category();
            /// Conversion of the received value to the desired type
            BO.Category.TryParse(Console.ReadLine(), out cat);
            TheProduct.Category = cat;
            if (cat is BO.Category.Clothing)
            {
                BO.Clothing c = new BO.Clothing();
                if (g is BO.Gender.Women or BO.Gender.Girls)
                {
                    Console.WriteLine($@"Enter the Clothing type:
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
                    Console.WriteLine($@"Enter the Clothing type:
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
                BO.Clothing.TryParse(Console.ReadLine(), out c);
                TheProduct.Clothing = c;
                Console.WriteLine($@"Enter the size of the Clothing:
0: XS
1: S
2: M
3: L
4: XL");
                BO.SizeClothing sc = new BO.SizeClothing();
                /// Conversion of the received value to the desired type
                BO.SizeClothing.TryParse(Console.ReadLine(), out sc);
                TheProduct.SizeClothing = sc;
            }
            else
            {
                BO.Shoes s = new BO.Shoes();
                if (g is BO.Gender.Women)
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
                BO.Shoes.TryParse(Console.ReadLine(), out s);
                TheProduct.Shoes = s;
                Console.WriteLine($@"Enter the Shoes type:
36
37
38
39
40
41");
                BO.SizeShoes ss;
                /// Conversion of the received value to the desired type
                BO.SizeShoes.TryParse(Console.ReadLine(), out ss);
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
            BO.Color cl = new BO.Color();
            /// Conversion of the received value to the desired type
            BO.Color.TryParse(Console.ReadLine(), out cl);
            TheProduct.Color = cl;
            Console.WriteLine("Enter the description of the product:");
            string str = Console.ReadLine();
            TheProduct.Description = str;
        }

        void OrderSwitch()
        {
            BO.Order theOrder = new BO.Order();
            BO.OrderChoice orderChoice = new BO.OrderChoice();
            do
            {
                Console.WriteLine($@"Enter your Choice:
0: Exit
1: Print order datails
2: Print the List
3: Update ship date
4: Update delivery date
5: Tracking order");
                /// Conversion of the received value to the desired type
                BO.OrderChoice.TryParse(Console.ReadLine(), out orderChoice);
                switch (orderChoice)
                {
                    case BO.OrderChoice.Exit:/// A case to exit the order back to the main
                        return;
                    case BO.OrderChoice.PrintOrderDetails: ///print the order details with the recieved id
                        Console.WriteLine("Enter the ordr ID:");
                        int id;
                        /// Conversion of the received value to the desired type
                        int.TryParse(Console.ReadLine(), out id);
                        try
                        { Console.WriteLine(blApi.Order.OrderDetails(id)); }
                        catch (BO.NonFoundObjectBo)
                        { }
                        finally { Console.WriteLine("Problem found!"); }
                        break;
                    case BO.OrderChoice.PrintTheList: ///printing the full order list
                        foreach (var or in blApi.Order.GetAll())
                            Console.WriteLine(or);
                        break;
                    case BO.OrderChoice.UpdateShipDate: // update the ship date
                        Console.WriteLine("Enter the ID of the order that you want to update the ship date:");
                        int id1;
                        /// Conversion of the received value to the desired type
                        int.TryParse(Console.ReadLine(), out id1);
                        Console.WriteLine("The updeted order is:");
                        try
                        { Console.WriteLine(blApi.Order.UpdeteShipDate(id1)); }
                        catch (BO.NonFoundObjectBo)
                        { }
                        catch (BO.AlreadyUpdated)
                        { }
                        finally { Console.WriteLine("Problem found!"); }
                        break;
                    case BO.OrderChoice.UpdateDeliveryDate: ///update the delivery date
                        Console.WriteLine("Enter the ID of the order that you want to update the ship date:");
                        int id2;
                        /// Conversion of the received value to the desired type
                        int.TryParse(Console.ReadLine(), out id2);
                        Console.WriteLine("The updeted order is:");
                        try
                        { Console.WriteLine(blApi.Order.UpdateDeliveryDate(id2)); }
                        catch (BO.NonFoundObjectBo)
                        { }
                        catch (BO.AlreadyUpdated)
                        { }
                        finally { Console.WriteLine("Problem found!"); }
                        break;
                    case BO.OrderChoice.TrackingOrder: ///prints the tracking status
                        Console.WriteLine("Enter the ID of the order that you wants to get the tracking status:");
                        int id3;
                        /// Conversion of the received value to the desired type
                        int.TryParse(Console.ReadLine(), out id3);
                        try
                        { Console.WriteLine(blApi.Order.TrackingOrder(id3)); }
                        catch (BO.NotInStock)
                        { }
                        finally { Console.WriteLine("Problem found!"); }
                        break;
                    default:
                        break;
                }
            } while (orderChoice != 0);
        }

        void CartSwitch()
        {
            BO.Cart theCart = new BO.Cart();
            BO.CartChoice cartChoice = new BO.CartChoice();
            do
            {
                Console.WriteLine($@"Enter your Choice:
0: Exit
1: Add a product
2: Update the amount of the product
3: Make a new order");
                /// Conversion of the received value to the desired type
                BO.CartChoice.TryParse(Console.ReadLine(), out cartChoice);
                switch (cartChoice)
                {
                    case CartChoice.Exit:
                        return;
                    case CartChoice.AddProduct:
                        Console.WriteLine("Enter the product ID that you want to add:");
                        int id;
                        /// Conversion of the received value to the desired type
                        int.TryParse(Console.ReadLine(), out id);
                        try
                        { cart = blApi.Cart.AddProductToCart(cart, id); }
                        catch (BO.NonFoundObjectBo)
                        {}
                        catch (BO.NotInStock)
                        {}
                        finally { Console.WriteLine("Problem found!"); }
                        Console.WriteLine("The cart after the addition:");
                        Console.WriteLine(cart);
                        break;
                    case CartChoice.UpdateAmount:
                        Console.WriteLine("Enter the product ID that you want to update it's amount");
                        int id1;
                        /// Conversion of the received value to the desired type
                        int.TryParse(Console.ReadLine(), out id1);
                        Console.WriteLine("Enter the new amount:");
                        int amount;
                        /// Conversion of the received value to the desired type
                        int.TryParse(Console.ReadLine(), out amount);
                        try
                        { cart = blApi.Cart.UpdateAmountProduct(cart, id1, amount); }
                        catch(BO.NonFoundObjectBo)
                        { }
                        catch(BO.SameAmount)
                        { }
                        finally { Console.WriteLine("Problem found!"); }
                        Console.WriteLine("The cart after the update:");
                        Console.WriteLine(cart);
                        break;
                    case CartChoice.OrderMaking:
                        try
                        { blApi.Cart.OrderMaking(cart); }
                        catch(BO.NonFoundObjectBo)
                        { }
                        catch(BO.ExistingObjectBo)
                        { }
                        finally { Console.WriteLine("Problem found!"); }
                        Console.WriteLine("Succesfuly made!");
                        break;
                    default:
                        break;
                }
            } while (cartChoice != 0);
        }
    }
}